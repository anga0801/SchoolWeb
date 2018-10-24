namespace SchoolWeb_DAL
{
    using SchoolWeb_DAL.Logging;
    using SchoolWeb_DAL.Mapping;
    using SchoolWeb_DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SubjectDAO
    {
        private readonly string _ConnectionString;

        public SubjectDAO(string connectionString, string logPath)
        {
            _ConnectionString = connectionString;
            Logger.SetLogPath(logPath);
        }

        public List<SubjectDO> ObtainAllSubjects()
        {
            List<SubjectDO> subjects = new List<SubjectDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getAllSubjects = new SqlCommand("SUBJECTS_VIEW_ALL", sqlConnection))
                {
                    getAllSubjects.CommandType = CommandType.StoredProcedure;
                    getAllSubjects.CommandTimeout = 60;

                    sqlConnection.Open();

                    SqlDataReader reader = getAllSubjects.ExecuteReader();

                    while (reader.Read())
                    {
                        SubjectDO subject = SubjectMapper.ReaderToDO(reader);

                        subjects.Add(subject);
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

            return subjects;
        }

        public SubjectDO ObtainSubjectByID(int subjectID)
        {
            SubjectDO subject = new SubjectDO();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getSubject = new SqlCommand("SUBJECT_VIEW_BY_ID", sqlConnection))
                {
                    getSubject.CommandType = CommandType.StoredProcedure;
                    getSubject.CommandTimeout = 60;

                    getSubject.Parameters.AddWithValue("SubjectID", subjectID);

                    sqlConnection.Open();

                    SqlDataReader reader = getSubject.ExecuteReader();

                    if (reader.Read())
                    {
                        subject = SubjectMapper.ReaderToDO(reader);
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

            return subject;
        }

        public List<SubjectDO> ObtainByGrade(Int16 grade)
        {
            List<SubjectDO> subjects = new List<SubjectDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getSubjects = new SqlCommand("SUBJECTS_VIEW_BY_GRADE", sqlConnection))
                {
                    getSubjects.CommandType = CommandType.StoredProcedure;
                    getSubjects.CommandTimeout = 60;

                    getSubjects.Parameters.AddWithValue("Grade", grade);

                    sqlConnection.Open();

                    SqlDataReader reader = getSubjects.ExecuteReader();

                    while (reader.Read())
                    {
                        SubjectDO subject = SubjectMapper.ReaderToDO(reader);

                        subjects.Add(subject);
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

            return subjects;
        }

        public void UpdateSubject(SubjectDO subject)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand updateSubject = new SqlCommand("SUBJECT_UPDATE_BY_ID", sqlConnection))
                {
                    updateSubject.CommandType = CommandType.StoredProcedure;
                    updateSubject.CommandTimeout = 60;

                    updateSubject.Parameters.AddWithValue("Category", subject.Category);
                    updateSubject.Parameters.AddWithValue("Name", subject.Name);
                    updateSubject.Parameters.AddWithValue("Grade", subject.Grade);
                    updateSubject.Parameters.AddWithValue("Material", subject.Material);
                    updateSubject.Parameters.AddWithValue("Description", subject.Description);
                    updateSubject.Parameters.AddWithValue("TeacherID", subject.TeacherID);

                    sqlConnection.Open();

                    updateSubject.ExecuteNonQuery();
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

        public void DeleteSubject(int subjectID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand deleteSubject = new SqlCommand("SUBJECT_DELETE_BY_ID", sqlConnection))
                {
                    deleteSubject.CommandType = CommandType.StoredProcedure;
                    deleteSubject.CommandTimeout = 60;

                    deleteSubject.Parameters.AddWithValue("SubjectID", subjectID);

                    sqlConnection.Open();

                    deleteSubject.ExecuteNonQuery();
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

        public void CreateSubject(SubjectDO subject)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand createSubject = new SqlCommand("SUBJECT_CREATE", sqlConnection))
                {
                    createSubject.CommandType = CommandType.StoredProcedure;
                    createSubject.CommandTimeout = 60;

                    createSubject.Parameters.AddWithValue("Category", subject.Category);
                    createSubject.Parameters.AddWithValue("Name", subject.Name);
                    createSubject.Parameters.AddWithValue("Grade", subject.Grade);
                    createSubject.Parameters.AddWithValue("Material", subject.Material);
                    createSubject.Parameters.AddWithValue("Description", subject.Description);
                    createSubject.Parameters.AddWithValue("TeacherID", subject.TeacherID);

                    sqlConnection.Open();

                    createSubject.ExecuteNonQuery();
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
