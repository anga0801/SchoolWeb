namespace SchoolWeb_DAL
{
    using SchoolWeb_DAL.Logging;
    using SchoolWeb_DAL.Mapping;
    using SchoolWeb_DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class UserDAO
    {
        private readonly string _ConnectionString;

        public UserDAO(string connectionString, string logPath)
        {
            _ConnectionString = connectionString;
            Logger.SetLogPath(logPath);
        }


        public List<UserDO> ObtainAllUsers()
        {
            List<UserDO> Users = new List<UserDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getAllUsers = new SqlCommand("USERS_VIEW_ALL", sqlConnection))
                {
                    getAllUsers.CommandType = CommandType.StoredProcedure;
                    getAllUsers.CommandTimeout = 60;

                    sqlConnection.Open();

                    SqlDataReader reader = getAllUsers.ExecuteReader();

                    while (reader.Read())
                    {
                        UserDO User = UserMapper.ReaderToDOForPublic(reader);

                        Users.Add(User);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }

            return Users;
        }

        public List<UserDO> ObtainUsersByRole(int role)
        {
            List<UserDO> Users = new List<UserDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getUsers = new SqlCommand("USERS_VIEW_BY_ROLE", sqlConnection))
                {
                    getUsers.CommandType = CommandType.StoredProcedure;
                    getUsers.CommandTimeout = 60;

                    getUsers.Parameters.AddWithValue("Role", role);

                    sqlConnection.Open();

                    SqlDataReader reader = getUsers.ExecuteReader();

                    while (reader.Read())
                    {
                        UserDO User = UserMapper.ReaderToDO(reader);

                        Users.Add(User);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }

            return Users;
        }

        public UserDO ObtainUserByUsername(string username)
        {
            UserDO user = new UserDO();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getUser = new SqlCommand("USER_VIEW_BY_USERNAME", sqlConnection))
                {
                    getUser.CommandType = CommandType.StoredProcedure;
                    getUser.CommandTimeout = 60;

                    sqlConnection.Open();

                    getUser.Parameters.AddWithValue("Username", username);

                    SqlDataReader reader = getUser.ExecuteReader();

                    if (reader.Read())
                    {
                        user = UserMapper.ReaderToDO(reader);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }

            return user;
        }

        public UserDO ObtainUserByID(int userID)
        {
            UserDO user = new UserDO();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getUser = new SqlCommand("USER_VIEW_BY_ID", sqlConnection))
                {
                    getUser.CommandType = CommandType.StoredProcedure;
                    getUser.CommandTimeout = 60;

                    sqlConnection.Open();

                    getUser.Parameters.AddWithValue("UserID", userID);

                    SqlDataReader reader = getUser.ExecuteReader();

                    if (reader.Read())
                    {
                        user = UserMapper.ReaderToDO(reader);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }

            return user;
        }

        public List<string> ObtainAllUsernames()
        {
            List<string> usernames = new List<string>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getUsernames = new SqlCommand("USERS_VIEW_ALL_USERNAMES", sqlConnection))
                {
                    getUsernames.CommandType = CommandType.StoredProcedure;
                    getUsernames.CommandTimeout = 60;

                    sqlConnection.Open();

                    SqlDataReader reader = getUsernames.ExecuteReader();

                    while (reader.Read())
                    {
                        string username = (string)reader["Username"];
                        usernames.Add(username);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }

            return usernames;
        }

        public List<UserDO> ObtainUsersByClassID(int userID)
        {
            List<UserDO> Users = new List<UserDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getUsers = new SqlCommand("USERS_VIEW_BY_CLASSID", sqlConnection))
                {
                    getUsers.CommandType = CommandType.StoredProcedure;
                    getUsers.CommandTimeout = 60;

                    getUsers.Parameters.AddWithValue("ClassID", userID);

                    sqlConnection.Open();

                    SqlDataReader reader = getUsers.ExecuteReader();

                    while (reader.Read())
                    {
                        UserDO User = UserMapper.ReaderToDOForPublic(reader);

                        Users.Add(User);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }

            return Users;
        }

        public void CreateUser(UserDO user)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand createUser = new SqlCommand("USER_CREATE", sqlConnection))
                {
                    createUser.CommandType = CommandType.StoredProcedure;
                    createUser.CommandTimeout = 60;

                    createUser.Parameters.AddWithValue("FirstName", user.FirstName);
                    createUser.Parameters.AddWithValue("LastName", user.LastName);
                    createUser.Parameters.AddWithValue("Nickname", user.Nickname);
                    createUser.Parameters.AddWithValue("Title", user.Title);
                    createUser.Parameters.AddWithValue("DOB", user.DateOfBirth);
                    createUser.Parameters.AddWithValue("HiredDate", user.HiredDate);
                    createUser.Parameters.AddWithValue("EMail", user.Email);
                    createUser.Parameters.AddWithValue("Phone", user.Phone);
                    createUser.Parameters.AddWithValue("HomeAddress", user.HomeAddress);
                    createUser.Parameters.AddWithValue("PhotoPath", user.PhotoPath);
                    createUser.Parameters.AddWithValue("About", user.About);
                    createUser.Parameters.AddWithValue("Username", user.Username);
                    createUser.Parameters.AddWithValue("Password", user.Password);
                    createUser.Parameters.AddWithValue("ChildID", user.ChildID);
                    createUser.Parameters.AddWithValue("RoleID", user.RoleID);
                    createUser.Parameters.AddWithValue("ClassID", user.ClassID);

                    sqlConnection.Open();

                    createUser.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }
        }

        public void UpdateUser(UserDO user)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand updateUser = new SqlCommand("USER_UPDATE_BY_ID", sqlConnection))
                {
                    updateUser.CommandType = CommandType.StoredProcedure;
                    updateUser.CommandTimeout = 60;

                    updateUser.Parameters.AddWithValue("UserID", user.UserID);
                    updateUser.Parameters.AddWithValue("FirstName", user.FirstName);
                    updateUser.Parameters.AddWithValue("LastName", user.LastName);
                    updateUser.Parameters.AddWithValue("Nickname", user.Nickname);
                    updateUser.Parameters.AddWithValue("Title", user.Title);
                    updateUser.Parameters.AddWithValue("DOB", user.DateOfBirth);
                    updateUser.Parameters.AddWithValue("HiredDate", user.HiredDate);
                    updateUser.Parameters.AddWithValue("EMail", user.Email);
                    updateUser.Parameters.AddWithValue("Phone", user.Phone);
                    updateUser.Parameters.AddWithValue("HomeAddress", user.HomeAddress);
                    updateUser.Parameters.AddWithValue("PhotoPath", user.PhotoPath);
                    updateUser.Parameters.AddWithValue("About", user.About);
                    updateUser.Parameters.AddWithValue("Username", user.Username);
                    updateUser.Parameters.AddWithValue("Password", user.Password);
                    updateUser.Parameters.AddWithValue("ChildID", user.ChildID);
                    updateUser.Parameters.AddWithValue("RoleID", user.RoleID);
                    updateUser.Parameters.AddWithValue("ClassID", user.ClassID);

                    sqlConnection.Open();

                    updateUser.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }
        }

        public void DeleteUser(int userID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand deleteUser = new SqlCommand("USER_DELETE_BY_ID", sqlConnection))
                {
                    deleteUser.CommandType = CommandType.StoredProcedure;
                    deleteUser.CommandTimeout = 60;

                    deleteUser.Parameters.AddWithValue("UserID", userID);

                    sqlConnection.Open();

                    deleteUser.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.SqlExceptionLog(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex);
                throw ex;
            }
        }

    }
}
