using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models
{
    public interface IUserRepository
    {
        //Method for check user if is valid 
        Task<bool> AuthenticateUser(NetworkCredential credential);
        Task Add(UserModel userModel);
        void Edit(UserModel userModel);
        Task EditByEmail(UserModel userModel);
        void Delete(int _id);
        UserModel GetById(int _id);
        UserModel GetByUsername(string username);
        Task<List<UserModel>> GetByEmail(string email);
        Task<bool> ConsultByEmail(string email);
        Task<DataTable> ConsultUsername(string username);
        Task<DataTable> GetInfoByEmail(string email);
        Task<List<UserModel>> GetDataAll(string username);
    }
}
