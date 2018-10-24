namespace SchoolWeb_DAL.Mapping
{
    using SchoolWeb_DAL.Models;
    using System;
    using System.Data.SqlClient;

    class SubjectMapper
    {
        public static SubjectDO ReaderToDO(SqlDataReader from)
        {
            SubjectDO to = new SubjectDO();

            to.SubjectID = (int)from["SubjectID"];
            to.Category = (string)from["Category"];
            to.Name = (string)from["Name"];
            to.Grade = (Int16)from["Grade"];
            to.Material = from["Material"] as string;
            to.Description = from["Description"] as string;
            to.TeacherID = (int)from["TeacherID"];

            return to;
        }
    }
}
