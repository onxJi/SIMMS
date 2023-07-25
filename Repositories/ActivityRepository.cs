using SIMMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMMS.Repositories
{
    public class ActivityRepository : RepositoryBase, IActivityRepository
    {
        public async Task Add(ActivityModel activity)
        {
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "insert into [Activities] (UserActivity," +
                    "ActivityTitle,ActivityDescription," +
                    "DateFrom,DateTo,Priority,IsCompleted) values (@user,@title," +
                    "@description,@dateFrom,@dateTo,@priority,@isCompleted)";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@user", SqlDbType.NVarChar).Value = activity.User;
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = activity.Title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = activity.Description;
                command.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = activity.DateFrom;
                command.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = activity.DateTo;
                command.Parameters.Add("@priority", SqlDbType.NVarChar).Value = activity.Priority;
                command.Parameters.Add("@isCompleted", SqlDbType.Bit).Value = activity.IsCompleted;

                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<DataTable> ConsultActivities(string username)
        {
            using (var con = GetSqlConnection())
            {
                using (var command = new SqlCommand())
                {
                    await con.OpenAsync();
                    command.Connection = con;
                    command.CommandText = "select * from Activities where UserActivity = @user";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;
                    SqlDataReader lector = await command.ExecuteReaderAsync();
                    using (var dt = new DataTable())
                    {
                        dt.Load(lector);
                        lector.Dispose();
                        return dt;
                    }
                }

            }
        }

        public async Task<DataTable> ConsultActivitiesByDateAsc(string username)
        {
            using (var con = GetSqlConnection())
            {
                using (var command = new SqlCommand())
                {
                    await con.OpenAsync();
                    command.Connection = con;
                    command.CommandText = "SELECT * FROM [Activities] WHERE " +
                        "DateTo >= ( SELECT CONVERT (DATE,GETDATE()))  " +
                        "AND UserActivity = @user ORDER BY DateTo ASC";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;
                    SqlDataReader lector = await command.ExecuteReaderAsync();
                    using (var dt = new DataTable())
                    {
                        dt.Load(lector);
                        lector.Dispose();
                        return dt;
                    }
                }

            }
        }

        public async Task<DataTable> ConsultActivitiesByDateDesc(string username)
        {
            using (var con = GetSqlConnection())
            {
                using (var command = new SqlCommand())
                {
                    await con.OpenAsync();
                    command.Connection = con;
                    command.CommandText = "SELECT * FROM [Activities] WHERE " +
                        "DateTo >= ( SELECT CONVERT (DATE,GETDATE()))  " +
                        "AND UserActivity = @user ORDER BY DateTo DESC";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;
                    SqlDataReader lector = await command.ExecuteReaderAsync();
                    using (var dt = new DataTable())
                    {
                        dt.Load(lector);
                        lector.Dispose();
                        return dt;
                    }
                }

            }
        }
        public async Task<DataTable> ConsultActivitiesByPriority(string username, string priority)
        {
            using (var con = GetSqlConnection())
            {
                using (var command = new SqlCommand())
                {
                    await con.OpenAsync();
                    command.Connection = con;
                    command.CommandText = "SELECT * FROM [Activities] WHERE Priority" +
                        " = @priority  AND UserActivity = @user";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@priority", SqlDbType.NVarChar).Value = priority;
                    SqlDataReader lector = await command.ExecuteReaderAsync();
                    using (var dt = new DataTable())
                    {
                        dt.Load(lector);
                        lector.Dispose();
                        return dt;
                    }
                }

            }
        }
        public async Task<DataTable> ConsultActivitiesByTitle(string username, string title)
        {
            using (var con = GetSqlConnection())
            {
                using (var command = new SqlCommand())
                {
                    await con.OpenAsync();
                    command.Connection = con;
                    command.CommandText = "select * from Activities WHERE ActivityTitle LIKE" +
                        " '%' + @title + '%' AND UserActivity = @user";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
                    SqlDataReader lector = await command.ExecuteReaderAsync();
                    using (var dt = new DataTable())
                    {
                        dt.Load(lector);
                        lector.Dispose();
                        return dt;
                    }
                }

            }
        }

        public async Task Delete(long id)
        {
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "DELETE FROM Activities WHERE IDActivity = @id";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<List<ActivityModel>> GetActivitiesAsync(string username, Task<DataTable> consultType)
        {
            var tabla = await consultType;
            var list = new List<ActivityModel>();
            foreach (DataRow dr in tabla.Rows)
            {
                try
                {
                    list.Add(new ActivityModel()
                    {
                        Id = Convert.ToInt32(dr[0]),
                        User = Convert.ToString(dr[1]),
                        Title = Convert.ToString(dr[2]),
                        Description = Convert.ToString(dr[3]),
                        DateFrom = Convert.ToDateTime(dr[4]),
                        DateTo = Convert.ToDateTime(dr[5]),
                        Priority = Convert.ToString(dr[6]),
                        IsCompleted = Convert.ToBoolean(dr[7])

                    });

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
            return list;
        }

        public async Task<List<ActivityModel>> GetActivitiesByTitleAsync(string username, string title)
        {
            var tabla = await ConsultActivitiesByTitle(username, title);
            var list = new List<ActivityModel>();
            foreach (DataRow dr in tabla.Rows)
            {
                try
                {
                    list.Add(new ActivityModel()
                    {
                        Id = Convert.ToInt32(dr[0]),
                        User = Convert.ToString(dr[1]),
                        Title = Convert.ToString(dr[2]),
                        Description = Convert.ToString(dr[3]),
                        DateFrom = Convert.ToDateTime(dr[4]),
                        DateTo = Convert.ToDateTime(dr[5]),
                        Priority = Convert.ToString(dr[6]),
                        IsCompleted = Convert.ToBoolean(dr[7])

                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return list;
        }

        public async Task Update(ActivityModel activity)
        {
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "UPDATE Activities set ActivityTitle = @activity," +
                    "ActivityDescription = @description," +
                    "DateFrom = @dateFrom, DateTo = @dateTo," +
                    "Priority = @priority, IsCompleted = @isCompleted " +
                    "WHERE IDActivity = @ID";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@activity", SqlDbType.NVarChar).Value = activity.Title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = activity.Description;
                command.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = activity.DateFrom;
                command.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = activity.DateTo;
                command.Parameters.Add("@priority", SqlDbType.NVarChar).Value = activity.Priority;
                command.Parameters.Add("@isCompleted", SqlDbType.Bit).Value = activity.IsCompleted;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = activity.Id;
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
