namespace SchoolWeb.Mapping
{
    using SchoolWeb.Models;
    using SchoolWeb_DAL.Models;
    using System.Collections.Generic;

    public static class ClassMapper
    {
        public static ClassDO POtoDO(ClassPO from)
        {
            ClassDO to = new ClassDO();

            to.ClassID = from.ClassID;
            to.Grade = from.Grade;
            to.Group = from.Group;
            to.Name = from.Name;
            to.ClassTeacher = from.ClassTeacher;
            to.ClassPresident = from.ClassPresident;
            to.PhotoPath = from.PhotoPath;
            to.About = from.About;

            return to;
        }

        public static ClassPO DOtoPO(ClassDO from)
        {
            ClassPO to = new ClassPO();

            to.ClassID = from.ClassID;
            to.Grade = from.Grade;
            to.Group = from.Group;
            to.Name = from.Name;
            to.ClassTeacher = from.ClassTeacher;
            to.ClassPresident = from.ClassPresident;
            to.PhotoPath = from.PhotoPath;
            to.About = from.About;

            return to;
        }

        public static List<ClassPO> DOtoPO(List<ClassDO> from)
        {
            List<ClassPO> to = new List<ClassPO>();

            foreach (ClassDO classDO in from)
            {
                to.Add(DOtoPO(classDO));
            }

            return to;
        }
    }
}