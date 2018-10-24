namespace SchoolWeb.Controllers
{
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

    public class ClassController : Controller
    {
        private readonly string _ConnectionString;
        private readonly string _LogPath;
        private ClassDAO _ClassDAO;
        private UserDAO _UserDAO;

        public ClassController()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            string relativePath = ConfigurationManager.AppSettings["logPath"];
            _LogPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), relativePath);
            _ClassDAO = new ClassDAO(_ConnectionString, _LogPath);
            _UserDAO = new UserDAO(_ConnectionString, _LogPath);

        }

        [HttpGet]
        [SecurityFilter(role: 5)]
        public ActionResult ViewClassDetails(int classID)
        {
            ActionResult response;

            try
            {
                ClassDetailsVM classDataVM = new ClassDetailsVM();

                ClassDO ClassDO = _ClassDAO.ObtainClassByID(classID);
                classDataVM.Class = ClassMapper.DOtoPO(ClassDO);

                List<UserDO> usersDO = _UserDAO.ObtainUsersByClassID(classDataVM.Class.ClassID);
                classDataVM.Students = UserMapper.DOtoPO(usersDO);

                if (classDataVM.Class.ClassTeacher.HasValue)
                {
                    UserDO teacher = _UserDAO.ObtainUserByID((int)classDataVM.Class.ClassTeacher);
                    classDataVM.ClassTeacher = UserMapper.DOtoPO(teacher);
                }

                if (classDataVM.Class.ClassPresident.HasValue)
                {
                    UserDO president = _UserDAO.ObtainUserByID((int)classDataVM.Class.ClassPresident);
                    classDataVM.ClassPresident = UserMapper.DOtoPO(president);
                }

                response = View(classDataVM);
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
        public ActionResult ViewAllClasses()
        {
            ActionResult response;

            try
            {
                List<ClassDO> allClassesDO = _ClassDAO.ObtainAllClasses();
                List<ClassPO> allClassesPO = ClassMapper.DOtoPO(allClassesDO);

                response = View(allClassesPO);
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
        public ActionResult UpdateClass(int classID)
        {
            ActionResult response;

            try
            {
                ClassDO classDO = _ClassDAO.ObtainClassByID(classID);
                ClassPO classPO = ClassMapper.DOtoPO(classDO);

                response = View(classPO);
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
        public ActionResult UpdateClass(ClassPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    ClassDO classDO = ClassMapper.POtoDO(form);
                    _ClassDAO.UpdateClass(classDO);

                    response = RedirectToAction("ViewAllClasses", "Class");
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
        public ActionResult DeleteClass(int classID)
        {
            ActionResult response;

            try
            {
                _ClassDAO.DeleteClass(classID);
                response = RedirectToAction("ViewAllClasses", "Class");
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
        public ActionResult CreateClass()
        {
            return View();
        }

        [HttpPost]
        [SecurityFilter(role: 2)]
        public ActionResult CreateClass(ClassPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    ClassDO classDO = ClassMapper.POtoDO(form);
                    _ClassDAO.CreateClass(classDO);

                    response = RedirectToAction("ViewAllClasses", "Class");

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