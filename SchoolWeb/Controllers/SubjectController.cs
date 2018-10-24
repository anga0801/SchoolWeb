using SchoolWeb.Custom;
using SchoolWeb.Logging;
using SchoolWeb.Mapping;
using SchoolWeb.Models;
using SchoolWeb_DAL;
using SchoolWeb_DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace SchoolWeb.Controllers
{
    public class SubjectController : Controller
    {
        private readonly string _ConnectionString;
        private readonly string _LogPath;
        private SubjectDAO _SubjectDAO;
        private UserDAO _UserDAO;
        private ClassDAO _ClassDAO;

        public SubjectController()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            string relativePath = ConfigurationManager.AppSettings["logPath"];
            _LogPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), relativePath);
            _SubjectDAO = new SubjectDAO(_ConnectionString, _LogPath);
            _UserDAO = new UserDAO(_ConnectionString, _LogPath);
            _ClassDAO = new ClassDAO(_ConnectionString, _LogPath);
        }

        [HttpGet]
        [SecurityFilter(role: 5)]
        public ActionResult ViewSubjectDetails(int subjectID)
        {
            ActionResult response;

            try
            {
                SubjectDO subjectDO = _SubjectDAO.ObtainSubjectByID(subjectID);
                SubjectPO subjectPO = SubjectMapper.DOtoPO(subjectDO);

                response = View(subjectPO);
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
        [SecurityFilter(role: 5)]
        public ActionResult ViewAllSubjects()
        {
            ActionResult response;

            try
            {
                List<SubjectDO> allSubjectsDO = _SubjectDAO.ObtainAllSubjects();
                List<SubjectPO> allSubjectsPO = SubjectMapper.DOtoPO(allSubjectsDO);

                response = View(allSubjectsPO);
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
        public ActionResult UpdateSubject(int subjectID)
        {
            ActionResult response;

            try
            {
                SubjectDO subjectDO = _SubjectDAO.ObtainSubjectByID(subjectID);
                SubjectPO subjectPO = SubjectMapper.DOtoPO(subjectDO);

                response = View(subjectPO);
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
        public ActionResult UpdateSubject(SubjectPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    SubjectDO subjectDO = SubjectMapper.POtoDO(form);
                    _SubjectDAO.UpdateSubject(subjectDO);

                    response = RedirectToAction("ViewAllSubjects", "Subject");

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
        public ActionResult DeleteSubject(int subjectID)
        {
            ActionResult response;

            try
            {
                if (ModelState.IsValid)
                {
                    _SubjectDAO.DeleteSubject(subjectID);
                    response = RedirectToAction("ViewAllSubjects", "Subject");
                }
                else
                {
                    response = View();
                }
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
        [SecurityFilter(role: 2)]
        public ActionResult CreateSubject()
        {
            SubjectPO subject = new SubjectPO();
            subject.SubjectID = 1;

            return View(subject);
        }

        [HttpPost]
        [SecurityFilter(role: 2)]
        public ActionResult CreateSubject(SubjectPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    SubjectDO subjectDO = SubjectMapper.POtoDO(form);
                    _SubjectDAO.CreateSubject(subjectDO);

                    response = RedirectToAction("ViewAllSubjects", "Subject");

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
    }
}