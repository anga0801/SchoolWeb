namespace SchoolWeb.Mapping
{
    using SchoolWeb.Models;
    using SchoolWeb_DAL.Models;
    using System.Collections.Generic;

    public class SubjectMapper
    {
        public static SubjectDO POtoDO(SubjectPO from)
        {
            SubjectDO to = new SubjectDO();

            to.SubjectID = from.SubjectID;
            to.Category = from.Category;
            to.Name = from.Name;
            to.Grade = from.Grade;
            to.Material = from.Material;
            to.Description = from.Description;
            to.TeacherID = from.TeacherID;

            return to;
        }

        public static SubjectPO DOtoPO(SubjectDO from)
        {
            SubjectPO to = new SubjectPO();

            to.SubjectID = from.SubjectID;
            to.Category = from.Category;
            to.Name = from.Name;
            to.Grade = from.Grade;
            to.Material = from.Material;
            to.Description = from.Description;
            to.TeacherID = from.TeacherID;

            return to;
        }

        public static List<SubjectPO> DOtoPO(List<SubjectDO> from)
        {
            List<SubjectPO> to = new List<SubjectPO>();

            foreach (SubjectDO subject in from)
            {
                to.Add(DOtoPO(subject));
            }

            return to;
        }
    }
}