namespace SchoolWeb.Mapping
{
    using SchoolWeb.Models;
    using SchoolWeb_DAL.Models;
    using System.Collections.Generic;

    public class SessionMapper
    {
        public static SessionDO POtoDO(SessionPO from)
        {
            SessionDO to = new SessionDO();

            to.SessionID = from.SessionID;
            to.Date = from.Date;
            to.StartTime = from.StartTime;
            to.EndTime = from.EndTime;
            to.ClassID = from.ClassID;
            to.SubjectID = from.SubjectID;
            to.Notes = from.Notes;
            to.Homework = from.Homework;

            return to;
        }

        public static SessionPO DOtoPO(SessionDO from)
        {
            SessionPO to = new SessionPO();

            to.SessionID = from.SessionID;
            to.Date = from.Date;
            to.StartTime = from.StartTime;
            to.EndTime = from.EndTime;
            to.ClassID = from.ClassID;
            to.SubjectID = from.SubjectID;
            to.Notes = from.Notes;
            to.Homework = from.Homework;

            return to;
        }

        public static List<SessionPO> DOtoPO(List<SessionDO> from)
        {
            List<SessionPO> to = new List<SessionPO>();

            foreach (SessionDO session in from)
            {
                to.Add(DOtoPO(session));
            }

            return to;
        }
    }
}