using FastMail.Core.Domain;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace FastMail.Service
{
    public class AdminService : IAdminService
    {
        private readonly IConfiguration _configuration;

        public AdminService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
                /// Gets all senders
                /// </summary>
                /// <returns>A task that represents the asynchronous operation</returns>
        public List<Sender> GetAllSenders()
        {
            var list = new List<Sender>();

            string cnnStr = _configuration.GetConnectionString("SendSuiteConnection");
            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT * FROM SENDERS ORDER BY SNDNAME";

                cnn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var sender = new Sender {
                        DateCreatedUtc = DateTime.UtcNow,
                        Name = dr["SNDNAME"].ToString(),
                        Address = dr["SNDADDR"].ToString(),
                        City = dr["SNDCITY"].ToString(),
                        State = dr["SNDSTAT"].ToString(),
                        Zip = dr["SNDZIP"].ToString(),
                        Remark = dr["SNDEMAIL"].ToString()
                    };
                    list.Add(sender);
                }
            }

           return list;
        }
    }
}