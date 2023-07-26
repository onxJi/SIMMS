using SIMMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SIMMS.Repositories
{
	public class ProductRepository : RepositoryBase, IProductRepository
	{
		public async Task Add(ProductModel product)
		{
			using (var con = GetSqlConnection())
			using (var command = new SqlCommand())
			{
				await con.OpenAsync();
				command.Connection = con;
				command.CommandText = "insert into [Products] (Name," +
					"Category,Brand,Pieces,Version,Serie,Size,Measure," +
					"EntryDate,EgressDate,Image) values (@name,@category," +
					"@brand,@pieces,@version,@serie,@size,@measure," +
					"@entryDate,@egressDate,@img)";
				command.CommandType = CommandType.Text;
				command.Parameters.Add("@name", SqlDbType.NVarChar).Value = product.Name;
				command.Parameters.Add("@category", SqlDbType.NVarChar).Value = product.Category;
				command.Parameters.Add("@brand", SqlDbType.NVarChar).Value = product.Brand;
				command.Parameters.Add("@pieces", SqlDbType.BigInt).Value = product.Pieces;
				command.Parameters.Add("@version", SqlDbType.NVarChar).Value = product.Version;
				command.Parameters.Add("@serie", SqlDbType.NVarChar).Value = product.Serie;
				command.Parameters.Add("@size", SqlDbType.NVarChar).Value = product.Size;
				command.Parameters.Add("@measure", SqlDbType.NVarChar).Value = product.Measure;
				command.Parameters.Add("@entryDate", SqlDbType.DateTime).Value = product.EntryDate;
				command.Parameters.Add("@egressDate", SqlDbType.DateTime).Value = product.EgressDate;
				command.Parameters.Add("@img", SqlDbType.Image).Value = product.Image;
				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task<DataTable> ConsultByCategory(string categoryName)
		{
			throw new NotImplementedException();
		}

		public async Task<DataTable> ConsultProducts()
		{
			using (var con = GetSqlConnection())
			{
				using (var command = new SqlCommand())
				{
					await con.OpenAsync();
					command.Connection = con;
					command.CommandText = "select * from Products";
					command.CommandType = CommandType.Text;
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

		public Task Delete(long id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ProductModel>> GetAllAsync()
		{
			var tabla = await ConsultProducts();
			var list = new List<ProductModel>();
			foreach (DataRow dr in tabla.Rows)
			{
				try
				{
					list.Add(new ProductModel()
					{
						IDProduct = Convert.ToInt32(dr[0]),
						Name = Convert.ToString(dr[1]),
						Category = Convert.ToString(dr[2]),
						Brand = Convert.ToString(dr[3]),
						Pieces = Convert.ToInt32(dr[4]),
						Version = Convert.ToString(dr[5]),
						Serie = Convert.ToString(dr[6]),
						Size = Convert.ToString(dr[7]),
						Measure = Convert.ToString(dr[8]),
						EntryDate = Convert.ToDateTime(dr[9]),
						EgressDate = Convert.ToDateTime(dr[10]),
						Image = (byte[])dr[11]

					});

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}


			}
			return list;
		}

		public Task<List<ProductModel>> GetAllByCategoryAsync(string categoryName)
		{
			throw new NotImplementedException();
		}

		public Task Update(ProductModel product)
		{
			throw new NotImplementedException();
		}
	}
}
