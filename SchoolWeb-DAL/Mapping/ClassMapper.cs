namespace SchoolWeb_DAL.Mapping
{
    using SchoolWeb_DAL.Models;
    using System;
    using System.Data.SqlClient;

    public class ClassMapper
    {
        public static ClassDO ReaderToDO(SqlDataReader from)
        {
            ClassDO to = new ClassDO();

            to.ClassID = (int)from["ClassID"];
            to.Grade = (Int16)from["Grade"];
            to.Group = (string)from["Group"];
            to.Name = from["Name"] as string;
            to.ClassTeacher = from["Class Teacher"] as int?;
            to.ClassPresident = from["Class President"] as int?;
            to.PhotoPath = from["Photo Path"] as string;
            to.About = from["About"] as string;

            return to;
        }
    }
}
