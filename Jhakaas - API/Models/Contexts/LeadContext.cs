using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Jhakaas___API.Models.Contexts
{
    internal class LeadContext
    {
        internal Lead GetLeadById(int leadId)
        {
            var lead = new Lead();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetLeadById";
                    command.Parameters.AddWithValue("@leadId", leadId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lead.Id = reader.GetInt32(0);
                            lead.FirstName = reader.GetString(1);
                            lead.MiddleName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            lead.LastName = reader.GetString(3);
                            lead.Email = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            lead.Phone = reader.IsDBNull(5) ? -1 : reader.GetInt64(5);
                            lead.Avatar = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        }
                    }
                    connection.Close();
                }
            }
            return lead;
        }

        internal List<Lead> GetLeadsByUserId(int userId)
        {
            var leads = new List<Lead>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetLeadsByUserId";
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var lead = new Lead();
                            lead.Id = reader.GetInt32(0);
                            lead.FirstName = reader.GetString(1);
                            lead.MiddleName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            lead.LastName = reader.GetString(3);
                            lead.Email = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            lead.Phone = reader.IsDBNull(5) ? -1 : reader.GetInt64(5);
                            lead.Avatar = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);

                            leads.Add(lead);
                        }
                    }
                    connection.Close();
                }
            }
            return leads;
        }
    }
}