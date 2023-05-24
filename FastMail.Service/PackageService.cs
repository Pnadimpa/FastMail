using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace FastMail.Service
{
    public class PackageService : IPackageService
    {
        private readonly IConfiguration _configuration;

        public PackageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the packages by tracking no
        /// </summary>
        /// <param name="trackingNo"></param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public List<string> SearchPackage(string trackingNo)
        {
            var list = new List<string>();

            int cnt = 0;
            string cnnStr = _configuration.GetConnectionString("SendSuiteConnection");
            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT TRACKNO FROM mails2 WHERE TRACKNO LIKE '%{trackingNo}%' ORDER BY TRACKNO";

                cnn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (cnt >= 40)
                        break;

                    string no = dr[0].ToString();
                    list.Add(no);
                    cnt++;
                }
            }

           return list;
        }
    }
}











