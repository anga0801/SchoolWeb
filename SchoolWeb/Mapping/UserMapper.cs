namespace SchoolWeb.Mapping
{
    using SchoolWeb.Models;
    using SchoolWeb_DAL.Models;
    using System.Collections.Generic;

    public static class UserMapper
    {
        public static UserDO POtoDO(UserPO from)
        {
            UserDO to = new UserDO();

            to.UserID = from.UserID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Nickname = from.Nickname;
            to.Title = from.Title;
            to.DateOfBirth = from.DateOfBirth;
            if (from.HiredDate.HasValue)
            {
                to.HiredDate = from.HiredDate;
            }
            to.Email = from.Email;
            to.Phone = from.Phone;
            to.HomeAddress = from.HomeAddress;
            to.PhotoPath = from.PhotoPath;
            to.About = from.About;
            to.Username = from.Username;
            to.Password = from.Password;
            if (from.ChildID.HasValue)
            {
                to.ChildID = from.ChildID;
            }
            to.RoleID = from.RoleID;
            if (from.ClassID.HasValue)
            {
                to.ClassID = from.ClassID;
            }

            return to;
        }

        public static UserPO DOtoPO(UserDO from)
        {
            UserPO to = new UserPO();

            to.UserID = from.UserID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Nickname = from.Nickname;
            to.Title = from.Title;
            to.DateOfBirth = from.DateOfBirth;
            if (from.HiredDate.HasValue)
            {
                to.HiredDate = from.HiredDate;
            }
            to.Email = from.Email;
            to.Phone = from.Phone;
            to.HomeAddress = from.HomeAddress;
            to.PhotoPath = from.PhotoPath;
            to.About = from.About;
            to.Username = from.Username;
            to.Password = from.Password;
            if (from.ChildID.HasValue)
            {
                to.ChildID = from.ChildID;
            }
            to.RoleID = from.RoleID;
            if (from.ClassID.HasValue)
            {
                to.ClassID = from.ClassID;
            }

            return to;
        }

        public static List<UserPO> DOtoPO(List<UserDO> from)
        {
            List<UserPO> to = new List<UserPO>();

            foreach (UserDO user in from)
            {
                to.Add(DOtoPO(user));
            }

            return to;
        }
    }
}