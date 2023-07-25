using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Repositories
{
    public abstract class RepositoryBase
    {
        //C GetC = new C();
        private readonly string connection;
        private string cn;
        public RepositoryBase()
        {
            //connection = "Server=tcp:DESKTOP-QGU951C\\ONXCORP,7680;DataBase=sySIMMS; User Id=sa; Password=OnxJi090701";
            //connection = "8D39SUiHTqv3qc/qr0uGH20f1O8o7jG9DAMHWSxTdnFBctuq2Ntc2xP" +
            //    "0JmRnfonBHKv+PASSZTJ2flSkOj5U6AWwUhFP7HnucGiG1JtYblA5bwP3cIvn6UEAPknBb0Wr";
            connection = "Server=ONX;Database=sySIMMS;Integrated Security=true";
            //connection = "MzfSduawXEzilGBEyWkM4y3jlMDw5e+EzUrZL5RVp/3a9U5mptDzFsjnXCz1RFRkx3AhpQJfNm5Aj74TAkEp++yKYZ/I0f977nt3wryk8BI=";
            //connection = Convert.ToString(Descrypt.checkServer());

        }
        protected SqlConnection GetSqlConnection()
        {
            //cn = GetC.Des(connection);
            return new SqlConnection(connection);
        }

    }
}
