using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models
{
    public interface IActivityRepository
    {
        Task Add(ActivityModel activity);
        Task Update(ActivityModel activity);
        Task Delete(long id);
        Task<DataTable> ConsultActivities(string username);
        Task<DataTable> ConsultActivitiesByPriority(string username, string priority);
        Task<DataTable> ConsultActivitiesByDateAsc(string username);
        Task<DataTable> ConsultActivitiesByDateDesc(string username);
        Task<List<ActivityModel>> GetActivitiesAsync(string username, Task<DataTable> consultType);
        Task<DataTable> ConsultActivitiesByTitle(string username, string title);
        Task<List<ActivityModel>> GetActivitiesByTitleAsync(string username, string title);
    }
}
