using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Jhakaas___API.Models.Contexts
{
    internal class UserContext
    {
        internal User GetUserById(int userId)
        {
            var user = new User();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetUserById";
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = reader.GetInt32(0);
                            user.FirstName = reader.GetString(1);
                            user.MiddleName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            user.LastName = reader.GetString(3);
                            user.Email = reader.GetString(4);
                            user.Avatar = reader.GetString(5);
                            user.Role = reader.GetString(6);
                        }
                    }
                    connection.Close();
                }
            }
            return user;
        }

        internal string GetUserDeviceId(int userId)
        {
            string deviceId;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetUserDeviceId";
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    deviceId = command.ExecuteScalar() != DBNull.Value ? command.ExecuteScalar().ToString() : string.Empty;
                    connection.Close();
                }
            }
            return deviceId;
        }

        internal Dictionary<string, string> GetDeviceIdsOfAllUsers()
        {
            var deviceIds = new Dictionary<string, string>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetDeviceIdsOfAllUsers";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var userId = reader.GetString(0);
                            var deviceId = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            deviceIds.Add(userId, deviceId);
                        }
                    }
                    connection.Close();
                }
            }
            return deviceIds;
        }

        internal int UpdateDeviceIdForUser(string uniquePhoneId, string deviceId)
        {
            int result;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateDeviceIdForUser";
                    command.Parameters.AddWithValue("@uniquePhoneId", uniquePhoneId);
                    command.Parameters.AddWithValue("@deviceId", deviceId);
                    connection.Open();

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            return result;
        }

        internal User GetUserByFirstName(string firstName)
        {
            var user = new User();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetUserByFirstName";
                    command.Parameters.AddWithValue("@firstName", firstName);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = reader.GetInt32(0);
                            user.FirstName = reader.GetString(1);
                            user.MiddleName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            user.LastName = reader.GetString(3);
                            user.Email = reader.GetString(4);
                            user.Avatar = reader.GetString(5);
                            user.Role = reader.GetString(6);
                        }
                    }
                    connection.Close();
                }
            }
            return user;
        }
    }
}