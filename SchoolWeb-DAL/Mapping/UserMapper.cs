namespace SchoolWeb_DAL.Mapping
{
    using SchoolWeb_DAL.Models;
    using System;
    using System.Data.SqlClient;

    public class UserMapper
    {
        public static UserDO ReaderToDO(SqlDataReader from)
        {
            UserDO to = new UserDO();

            to.UserID = (int)from["UserID"];
            to.FirstName = (string)from["First Name"];
            to.LastName = (string)from["Last Name"];
            to.Nickname = from["Nickname"] as string;
            to.Title = from["Title"] as string;
            to.DateOfBirth = (DateTime)from["DOB"];
            to.HiredDate = from["Hired Date"] as DateTime?;
            to.Email = from["E-Mail"] as string;
            to.Phone = (string)from["Phone"];
            to.HomeAddress = from["Home Address"] as string;
            to.PhotoPath = from["Photo Path"] as string;
            to.About = from["About"] as string;
            to.Username = (string)from["Username"];
            to.Password = from["Password"] as string;
            to.ChildID = from["ChildID"] as int?;
            to.RoleID = (int)from["RoleID"];
            to.ClassID = from["ClassID"] as int?;

            return to;
        }
        public static UserDO ReaderToDOForPublic(SqlDataReader from)
        {
            UserDO to = new UserDO();

            to.UserID = (int)from["UserID"];
            to.FirstName = (string)from["First Name"];
            to.LastName = (string)from["Last Name"];
            to.Nickname = from["Nickname"] as string;
            to.Title = from["Title"] as string;
            to.DateOfBirth = (DateTime)from["DOB"];
            to.HiredDate = from["Hired Date"] as DateTime?;
            to.Email = from["E-Mail"] as string;
            to.Phone = (string)from["Phone"];
            to.HomeAddress = from["Home Address"] as string;
            to.PhotoPath = from["Photo Path"] as string;
            to.About = from["About"] as string;
            to.Username = (string)from["Username"];
            to.ChildID = from["ChildID"] as int?;
            to.RoleID = (int)from["RoleID"];
            to.ClassID = from["ClassID"] as int?;

            return to;
        }
    }
}
