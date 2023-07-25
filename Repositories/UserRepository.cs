using SIMMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMMS.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        //C c = new C();
        bool _validUser = false;
        public async Task Add(UserModel userModel)
        {
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "insert into [Users] (Name,ApPaterno,ApMaterno," +
                    "Username,Password,Email,Position,Profile) values (@nombre,@apPaterno," +
                    "@apMaterno,@user,@password,@email,@position,@profile)";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@apPaterno", SqlDbType.NVarChar).Value = userModel.ApellidoP;
                command.Parameters.Add("@apMaterno", SqlDbType.NVarChar).Value = userModel.ApellidoM;
                command.Parameters.Add("@user", SqlDbType.NVarChar).Value = userModel.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userModel.Email;
                command.Parameters.Add("@position", SqlDbType.NVarChar).Value = userModel.Position;
                command.Parameters.Add("@profile", SqlDbType.Image).Value = userModel.Profile;

                await command.ExecuteNonQueryAsync();
            }

        }

        public async Task<bool> AuthenticateUser(NetworkCredential credential)
        {
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "select * from [Users] where Username = @username";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@username",
                                        SqlDbType.NVarChar).Value = credential.UserName;

                SqlDataReader lector = await command.ExecuteReaderAsync();

                using (var dt = new DataTable())
                {
                    dt.Load(lector);
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            _validUser = BCrypt.Net.BCrypt.Verify(
                                credential.Password,
                                Convert.ToString(dr[5]));

                        }
                        catch
                        {

                        }
                    }
                }
            }
            return _validUser;
        }

        public async Task<bool> ConsultByEmail(string email)
        {
            bool validEmail;
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "select * from [Users] where [Email] = @email";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                validEmail = await command.ExecuteScalarAsync() == null ? false : true;
            }
            return validEmail;
        }

        public async Task<DataTable> ConsultUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            using (var con = GetSqlConnection())
            {
                using (var command = new SqlCommand())
                {
                    await con.OpenAsync();
                    command.Connection = con;
                    command.CommandText = "select * from Users where Username = @user";
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
        public async Task<DataTable> GetInfoByEmail(string email)
        {
            using (var con = GetSqlConnection())
            {
                using (var command = new SqlCommand())
                {
                    await con.OpenAsync();
                    command.Connection = con;
                    command.CommandText = "select * from Users where Email = @email";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
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
        public void Delete(int _id)
        {
            throw new NotImplementedException();
        }

        public async void Edit(UserModel userModel)
        {
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "update from Users set Password = @pass where Email = @email";
                command.CommandType = CommandType.Text;

            }
        }

        //This method returned userModel by gmail-> is all user info 
        public async Task<List<UserModel>> GetByEmail(string email)
        {
            var tabla = await GetInfoByEmail(email);
            var userAccount = new List<UserModel>();
            foreach (DataRow row in tabla.Rows)
            {
                try
                {
                    userAccount.Add(new UserModel()
                    {
                        Id = Convert.ToInt32(row[0]),
                        Name = Convert.ToString(row[1]),
                        ApellidoP = Convert.ToString(row[2]),
                        ApellidoM = Convert.ToString(row[3]),
                        UserName = Convert.ToString(row[4]),
                        Email = Convert.ToString(row[6]),
                        Position = Convert.ToString(row[7]),
                        Profile = (byte[])row[8]
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            return userAccount;
        }

        public UserModel GetById(int _id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserModel>> GetDataAll(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            var tabla = await ConsultUsername(username);
            var list = new List<UserModel>();
            foreach (DataRow dr in tabla.Rows)
            {
                try
                {
                    list.Add(new UserModel()
                    {
                        Id = Convert.ToInt32(dr[0]),
                        Name = Convert.ToString(dr[1]),
                        ApellidoP = Convert.ToString(dr[2]),
                        ApellidoM = Convert.ToString(dr[3]),
                        UserName = Convert.ToString(dr[4]),
                        Email = Convert.ToString(dr[6]),
                        Position = Convert.ToString(dr[7]),
                        Profile = (byte[])dr[8]

                    });

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
            return list;
        }

        public async Task EditByEmail(UserModel userModel)
        {
            using (var con = GetSqlConnection())
            using (var command = new SqlCommand())
            {
                await con.OpenAsync();
                command.Connection = con;
                command.CommandText = "update Users set Password = @pass where Email = @email";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@pass", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userModel.Email;
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
