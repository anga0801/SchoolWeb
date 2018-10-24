namespace SchoolWeb_DAL
{
    using SchoolWeb_DAL.Logging;
    using SchoolWeb_DAL.Mapping;
    using SchoolWeb_DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SessionDAO
    {
        private readonly string _ConnectionString;

        public SessionDAO(string connectionString, string logPath)
        {
            _ConnectionString = connectionString;
            Logger.SetLogPath(logPath);
        }

        public SessionDO ObtainSessionByID(int sessionID)
        {
            SessionDO session = new SessionDO();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getSession = new SqlCommand("SESSION_VIEW_BY_ID", sqlConnection))
                {
                    getSession.CommandType = CommandType.StoredProcedure;
                    getSession.Parameters.AddWithValue("SessionID", sessionID);
                    getSession.CommandTimeout = 60;

                    sqlConnection.Open();

                    SqlDataReader reader = getSession.ExecuteReader();

                    if (reader.Read())
                    {
                        session = SessionMapper.ReaderToDO(reader);
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

            return session;
        }

        public List<SessionDO> ObtainSessionsByDate(DateTime date)
        {
            List<SessionDO> sessions = new List<SessionDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getSessions = new SqlCommand("SESSIONS_VIEW_BY_DATE", sqlConnection))
                {
                    getSessions.CommandType = CommandType.StoredProcedure;
                    getSessions.Parameters.AddWithValue("Date", date);
                    getSessions.CommandTimeout = 60;

                    sqlConnection.Open();

                    SqlDataReader reader = getSessions.ExecuteReader();

                    while (reader.Read())
                    {
                        SessionDO session = SessionMapper.ReaderToDO(reader);

                        sessions.Add(session);
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

            return sessions;
        }

        public void UpdateSession(SessionDO sessionInfo)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand updateClass = new SqlCommand("SESSION_UPDATE_BY_ID", sqlConnection))
                {
                    updateClass.CommandType = CommandType.StoredProcedure;
                    updateClass.CommandTimeout = 60;

                    updateClass.Parameters.AddWithValue("SessionID", sessionInfo.SessionID);
                    updateClass.Parameters.AddWithValue("Date", sessionInfo.Date);
                    updateClass.Parameters.AddWithValue("StartTime", sessionInfo.StartTime);
                    updateClass.Parameters.AddWithValue("EndTime", sessionInfo.EndTime);
                    updateClass.Parameters.AddWithValue("ClassID", sessionInfo.ClassID);
                    updateClass.Parameters.AddWithValue("SubjectID", sessionInfo.SubjectID);
                    updateClass.Parameters.AddWithValue("Notes", sessionInfo.Notes);
                    updateClass.Parameters.AddWithValue("Homework", sessionInfo.Homework);

                    sqlConnection.Open();

                    updateClass.ExecuteNonQuery();
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

        public void DeleteSession(int sessionID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand deleteSession = new SqlCommand("SESSION_DELETE_BY_ID", sqlConnection))
                {
                    deleteSession.CommandType = CommandType.StoredProcedure;
                    deleteSession.CommandTimeout = 60;

                    deleteSession.Parameters.AddWithValue("SessionID", sessionID);

                    sqlConnection.Open();

                    deleteSession.ExecuteNonQuery();
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

        public void CreateSession(SessionDO sessionInfo)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand createClass = new SqlCommand("SESSION_CREATE", sqlConnection))
                {
                    createClass.CommandType = CommandType.StoredProcedure;
                    createClass.CommandTimeout = 60;

                    createClass.Parameters.AddWithValue("Date", sessionInfo.Date);
                    createClass.Parameters.AddWithValue("StartTime", sessionInfo.StartTime);
                    createClass.Parameters.AddWithValue("EndTime", sessionInfo.EndTime);
                    createClass.Parameters.AddWithValue("ClassID", sessionInfo.ClassID);
                    createClass.Parameters.AddWithValue("SubjectID", sessionInfo.SubjectID);
                    createClass.Parameters.AddWithValue("Notes", sessionInfo.Notes);
                    createClass.Parameters.AddWithValue("Homework", sessionInfo.Homework);

                    sqlConnection.Open();

                    createClass.ExecuteNonQuery();
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
