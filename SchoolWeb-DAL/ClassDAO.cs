namespace SchoolWeb_DAL
{
    using SchoolWeb_DAL.Logging;
    using SchoolWeb_DAL.Mapping;
    using SchoolWeb_DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class ClassDAO
    {
        private readonly string _ConnectionString;

        public ClassDAO(string connectionString, string logPath)
        {
            _ConnectionString = connectionString;
            Logger.SetLogPath(logPath);
        }

        public List<ClassDO> ObtainAllClasses()
        {
            List<ClassDO> classes = new List<ClassDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getAllClasses = new SqlCommand("CLASSES_VIEW_ALL", sqlConnection))
                {
                    getAllClasses.CommandType = CommandType.StoredProcedure;
                    getAllClasses.CommandTimeout = 60;

                    sqlConnection.Open();

                    SqlDataReader reader = getAllClasses.ExecuteReader();

                    while (reader.Read())
                    {
                        ClassDO classDO = ClassMapper.ReaderToDO(reader);

                        classes.Add(classDO);
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

            return classes;
        }

        public ClassDO ObtainClassByID(int classID)
        {
            ClassDO classDO = new ClassDO();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand getClass = new SqlCommand("CLASS_VIEW_BY_ID", sqlConnection))
                {
                    getClass.CommandType = CommandType.StoredProcedure;
                    getClass.Parameters.AddWithValue("ClassID", classID);
                    getClass.CommandTimeout = 60;

                    sqlConnection.Open();

                    SqlDataReader reader = getClass.ExecuteReader();

                    if (reader.Read())
                    {
                        classDO = ClassMapper.ReaderToDO(reader);
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

            return classDO;
        }

        public void UpdateClass(ClassDO classInfo)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand updateClass = new SqlCommand("CLASS_UPDATE_BY_ID", sqlConnection))
                {
                    updateClass.CommandType = CommandType.StoredProcedure;
                    updateClass.CommandTimeout = 60;

                    updateClass.Parameters.AddWithValue("ClassID", classInfo.ClassID);
                    updateClass.Parameters.AddWithValue("Grade", classInfo.Grade);
                    updateClass.Parameters.AddWithValue("Group", classInfo.Group);
                    updateClass.Parameters.AddWithValue("Name", classInfo.Name);
                    updateClass.Parameters.AddWithValue("PhotoPath", classInfo.PhotoPath);
                    updateClass.Parameters.AddWithValue("ClassTeacher", classInfo.ClassTeacher);
                    updateClass.Parameters.AddWithValue("ClassPresident", classInfo.ClassPresident);
                    updateClass.Parameters.AddWithValue("About", classInfo.About);

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

        public void DeleteClass(int classID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand deleteClass = new SqlCommand("CLASS_DELETE_BY_ID", sqlConnection))
                {
                    deleteClass.CommandType = CommandType.StoredProcedure;
                    deleteClass.CommandTimeout = 60;

                    deleteClass.Parameters.AddWithValue("ClassID", classID);

                    sqlConnection.Open();

                    deleteClass.ExecuteNonQuery();
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

        public void CreateClass(ClassDO classInfo)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand createClass = new SqlCommand("CLASS_CREATE", sqlConnection))
                {
                    createClass.CommandType = CommandType.StoredProcedure;
                    createClass.CommandTimeout = 60;

                    createClass.Parameters.AddWithValue("Grade", classInfo.Grade);
                    createClass.Parameters.AddWithValue("Group", classInfo.Group);
                    createClass.Parameters.AddWithValue("Name", classInfo.Name);
                    createClass.Parameters.AddWithValue("PhotoPath", classInfo.PhotoPath);
                    createClass.Parameters.AddWithValue("ClassTeacher", classInfo.ClassTeacher);
                    createClass.Parameters.AddWithValue("ClassPresident", classInfo.ClassPresident);
                    createClass.Parameters.AddWithValue("About", classInfo.About);

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
