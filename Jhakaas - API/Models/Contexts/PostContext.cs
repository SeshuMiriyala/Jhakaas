using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Jhakaas___API.Models.Contexts
{
    internal class PostContext
    {
        internal int GetNewPostsCount(DateTime lastPostCreatedOn)
        {
            int count;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetNewPostsCount";
                    command.Parameters.AddWithValue("@lastPostCreatedOn", lastPostCreatedOn);
                    connection.Open();
                    count = Convert.ToInt16(command.ExecuteScalar());
                    connection.Close();
                }
            }

            return count;
        }

        internal List<Post> GetNewPosts(DateTime? lastPostCreatedOn)
        {
            var postsList = new List<Post>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetNewPosts";
                    command.Parameters.AddWithValue("@lastPostCreatedOn", lastPostCreatedOn);
                    command.Parameters.AddWithValue("@maximumPostsCount", Convert.ToInt16(ConfigurationManager.AppSettings["MaximumPostsCount"]));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            #region Build Post Object
                            var post = new Post
                            {
                                Id = reader.GetInt32(0),
                                ActivityId = reader.GetInt32(1),
                                ActivityName = reader.GetString(2),
                                ActivityImageUrl = reader.GetString(3),
                                IsNotificationEnabled = reader.GetBoolean(4),
                                UserId = reader.GetInt32(5),
                                UserFirstName = reader.GetString(6),
                                UserMiddleName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                UserLastName = reader.GetString(8),
                                UserRole = reader.GetString(9),
                                UserAvatarUrl = reader.GetString(10),
                                LeadId = reader.GetInt32(11),
                                LeadFirstName = reader.GetString(12),
                                LeadMiddleName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                                LeadLastName = reader.GetString(14),
                                LeadAvatarUrl = reader.IsDBNull(15) ? string.Empty : reader.GetString(15),
                                LeadEmail = reader.IsDBNull(16) ? string.Empty : reader.GetString(16),
                                LeadPhoneNumber = reader.IsDBNull(17) ? 0 : reader.GetInt64(17),
                                IsPostRead = reader.GetBoolean(18),
                                CreatedOn = reader.GetDateTime(19)
                            };
                            #endregion Build Post Object

                            postsList.Add(post);
                        }
                    }
                    connection.Close();
                }
            }
            return postsList;
        }

        internal int AddNewPost(int userId, int leadId, string activityCode)
        {
            int count;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddNewPost";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@leadId", leadId);
                    command.Parameters.AddWithValue("@activityCode", activityCode);
                    connection.Open();
                    count = Convert.ToInt16(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            return count;
        }

        internal int UpdatePostsStatus(string postIds)
        {
            int count;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdatePostsStatus";
                    command.Parameters.AddWithValue("@postIds", string.Join(",", postIds));
                    connection.Open();
                    count = Convert.ToInt16(command.ExecuteNonQuery());
                    connection.Close();
                }
            }

            return count;
        }

        public string GetActivityDetails(string activityCode)
        {
            string activityName;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JhakaasSqlCon"].ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetActivityDetails";
                    command.Parameters.AddWithValue("@activityCode", activityCode);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    activityName = result != null ? result.ToString() : string.Empty;
                    connection.Close();
                }
            }

            return activityName;
        }
    }
}