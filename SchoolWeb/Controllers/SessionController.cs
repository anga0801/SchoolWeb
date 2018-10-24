namespace SchoolWeb.Controllers
{
    using SchoolWeb.Mapping;
    using SchoolWeb.Logging;
    using SchoolWeb.Models;
    using SchoolWeb_DAL;
    using SchoolWeb_DAL.Models;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.Mvc;
    using SchoolWeb.Custom;
    using System.Collections.Generic;

    public class SessionController : Controller
    {
        private readonly string _ConnectionString;
        private readonly string _LogPath;
        private SessionDAO _SessionDAO;
        private SubjectDAO _SubjectDAO;
        private UserDAO _UserDAO;
        private ClassDAO _ClassDAO;

        public SessionController()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            string relativePath = ConfigurationManager.AppSettings["logPath"];
            _LogPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), relativePath);
            _SessionDAO = new SessionDAO(_ConnectionString, _LogPath);
            _SubjectDAO = new SubjectDAO(_ConnectionString, _LogPath);
            _UserDAO = new UserDAO(_ConnectionString, _LogPath);
            _ClassDAO = new ClassDAO(_ConnectionString, _LogPath);
        }

        [HttpGet]
        [SecurityFilter(role: 5)]
        public ActionResult Calendar()
        {
            SearchCriterion sc = new SearchCriterion();
            return View(sc);
        }

        [HttpPost]
        [SecurityFilter(role: 5)]
        public ActionResult Calendar(SearchCriterion form)
        {
            return RedirectToAction("Schedule", "Session", form);
        }

        [HttpGet]
        [SecurityFilter(role: 5)]
        public ActionResult Schedule(SearchCriterion search)
        {
            ActionResult response;

            try
            {
                ScheduleVM schedule = new ScheduleVM();

                List<SessionDO> sessionsDO = _SessionDAO.ObtainSessionsByDate(search.SearchDate);
                schedule.Sessions = SessionMapper.DOtoPO(sessionsDO);

                List<ClassDO> classesDO = _ClassDAO.ObtainAllClasses();
                schedule.Classes = ClassMapper.DOtoPO(classesDO);

                ViewBag.Date = search.SearchDate;

                response = View(schedule);
            }
            catch (SqlException sqlEx)
            {
                TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                response = View();

                Logger.SqlExceptionLog(sqlEx);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                response = View();

                Logger.ExceptionLog(ex);
            }
            return response;
        }

        [HttpGet]
        [SecurityFilter(role: 4)]
        public ActionResult SessionDetails(int sessionID = 1)
        {
            ActionResult response;

            try
            {
                SessionDetailsVM sessionDetailsVM = new SessionDetailsVM();

                SessionDO sessionDO = _SessionDAO.ObtainSessionByID(sessionID);
                sessionDetailsVM.Session = SessionMapper.DOtoPO(sessionDO);

                ClassDO classDO = _ClassDAO.ObtainClassByID(sessionDO.ClassID);
                sessionDetailsVM.ClassPO = ClassMapper.DOtoPO(classDO);

                SubjectDO subject = _SubjectDAO.ObtainSubjectByID(sessionDO.SubjectID);
                sessionDetailsVM.Subject = SubjectMapper.DOtoPO(subject);

                List<SubjectDO> subjectsDO = _SubjectDAO.ObtainByGrade(classDO.Grade);
                sessionDetailsVM.Subjects = SubjectMapper.DOtoPO(subjectsDO);

                response = View(sessionDetailsVM);
            }
            catch (SqlException sqlEx)
            {
                TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                response = View();

                Logger.SqlExceptionLog(sqlEx);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                response = View();

                Logger.ExceptionLog(ex);
            }

            return response;
        }

        [HttpPost]
        [SecurityFilter(role: 4)]
        public ActionResult SessionDetails(SessionDetailsVM form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    SessionDO sessionDO = SessionMapper.POtoDO(form.Session);
                    _SessionDAO.UpdateSession(sessionDO);

                    SearchCriterion date = new SearchCriterion();
                    date.SearchDate = sessionDO.Date;

                    response = RedirectToAction("Schedule", "Session", date);
                }

                catch (SqlException sqlEx)
                {
                    TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                    response = View();

                    Logger.SqlExceptionLog(sqlEx);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                    response = View();

                    Logger.ExceptionLog(ex);
                }
            }
            else
            {
                response = View(form);
            }

            return response;
        }

        [HttpGet]
        [SecurityFilter(role: 2)]
        public ActionResult DeleteSession(int sessionID)
        {
            ActionResult response;

            try
            {
                _SessionDAO.DeleteSession(sessionID);
                response = RedirectToAction("Calendar", "Session");
            }
            catch (SqlException sqlEx)
            {
                TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                response = View();

                Logger.SqlExceptionLog(sqlEx);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Sorry, we encountered an issue.";
                response = View();

                Logger.ExceptionLog(ex);
            }

            return response;
        }

        private SessionDetailsVM SessionDetailPopulator(TimeSpan startTime, DateTime date, int classID)
        {
            SessionDetailsVM sessionDetails = new SessionDetailsVM();

            sessionDetails.Session.SessionID = 1;
            sessionDetails.Session.ClassID = classID;
            sessionDetails.Session.Date = date;
            sessionDetails.Session.StartTime = startTime;
            sessionDetails.Session.EndTime = startTime.Add(new TimeSpan(0, 40, 0));

            List<ClassDO> allClassesDO = _ClassDAO.ObtainAllClasses();
            sessionDetails.Classes = ClassMapper.DOtoPO(allClassesDO);

            ClassDO classDO = _ClassDAO.ObtainClassByID(classID);
            sessionDetails.ClassPO = ClassMapper.DOtoPO(classDO);

            List<SubjectDO> subjectsDO = _SubjectDAO.ObtainByGrade(classDO.Grade);
            sessionDetails.Subjects = SubjectMapper.DOtoPO(subjectsDO);

            return sessionDetails;
        }
        
        [HttpGet]
        [SecurityFilter(role: 2)]
        public ActionResult CreateSession(TimeSpan startTime, DateTime date, int classID)
        {
            SessionDetailsVM sessionDetails = SessionDetailPopulator(startTime, date, classID);

            return View(sessionDetails);
        }

        [HttpPost]
        [SecurityFilter(role: 2)]
        public ActionResult CreateSession(SessionDetailsVM form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    SessionDO sessionDO = SessionMapper.POtoDO(form.Session);
                    _SessionDAO.CreateSession(sessionDO);

                    SearchCriterion date = new SearchCriterion();
                    date.SearchDate = sessionDO.Date;

                    response = RedirectToAction("Schedule", "Session", date);

                }
                catch (SqlException sqlEx)
                {
                    TempData["ErrorMessage"] = "Sorry, we encountered an issue.";

                    SessionDetailsVM sessionDetails = SessionDetailPopulator(form.Session.StartTime, form.Session.Date, form.Session.ClassID);
                    sessionDetails.Session = form.Session;
                    response = View(sessionDetails);

                    Logger.SqlExceptionLog(sqlEx);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Sorry, we encountered an issue.";

                    SessionDetailsVM sessionDetails = SessionDetailPopulator(form.Session.StartTime, form.Session.Date, form.Session.ClassID);
                    sessionDetails.Session = form.Session;
                    response = View(sessionDetails);

                    Logger.ExceptionLog(ex);
                }
            }
            else
            {
                SessionDetailsVM sessionDetails = SessionDetailPopulator(form.Session.StartTime, form.Session.Date, form.Session.ClassID);
                sessionDetails.Session = form.Session;
                response = View(sessionDetails);
            }

            return response;
        }
    }
}