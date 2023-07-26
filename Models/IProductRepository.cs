using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models
{
	public interface IProductRepository
	{
		Task Add(ProductModel product);
		Task Update(ProductModel product);
		Task Delete(long id);
		Task<DataTable> ConsultProducts();
		Task<DataTable> ConsultByCategory(string categoryName);
		Task<List<ProductModel>> GetAllAsync();
		Task<List<ProductModel>> GetAllByCategoryAsync(string categoryName);


	}
}
