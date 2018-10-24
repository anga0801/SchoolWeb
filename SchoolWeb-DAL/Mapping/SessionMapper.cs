namespace SchoolWeb_DAL.Mapping
{
    using SchoolWeb_DAL.Models;
    using System.Data.SqlClient;
    using System;

    public class SessionMapper
    {
        public static SessionDO ReaderToDO(SqlDataReader from)
        {
            SessionDO to = new SessionDO();

            to.SessionID = (int)from["SessionID"];
            to.Date = (DateTime)from["Date"];
            to.StartTime = (TimeSpan)from["Start Time"];
            to.EndTime = (TimeSpan)from["End Time"];
            to.ClassID = (int)from["ClassID"];
            to.SubjectID = (int)from["SubjectID"];
            to.Notes = from["Notes"] as string;
            to.Homework = from["Homework"] as string;

            return to;
        }
    }
}
