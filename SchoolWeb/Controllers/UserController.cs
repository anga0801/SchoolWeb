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

    public class UserController : Controller
    {
        private readonly string _ConnectionString;
        private readonly string _LogPath;
        private UserDAO _UserDAO;
        private ClassDAO _ClassDAO;

        public UserController()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            string relativePath = ConfigurationManager.AppSettings["logPath"];
            _LogPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), relativePath);
            _UserDAO = new UserDAO(_ConnectionString, _LogPath);
            _ClassDAO = new ClassDAO(_ConnectionString, _LogPath);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    UserDO userData = _UserDAO.ObtainUserByUsername(form.Username);

                    if (!userData.UserID.Equals(0))
                    {
                        if (form.Password.Equals(userData.Password))
                        {
                            Session["UserID"] = userData.UserID;
                            Session["Username"] = userData.Username;
                            Session["RoleID"] = userData.RoleID;

                            response = RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Username or password was incorrect");

                            response = View(form);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Username or password was incorrect");

                        response = View(form);
                    }

                }
                catch (SqlException sqlEx)
                {
                    response = View();
                    TempData["ErrorMessage"] = "Sorry, we encountered an issue.";

                    Logger.SqlExceptionLog(sqlEx);
                }
                catch (Exception ex)
                {
                    response = View();
                    TempData["ErrorMessage"] = "Sorry, we encountered an issue.";

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
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ViewProfile(int userID)
        {
            ActionResult response;

            try
            {
                UserDO userDO = _UserDAO.ObtainUserByID(userID);
                UserPO userPO = UserMapper.DOtoPO(userDO);

                if (userPO.ClassID.HasValue)
                {
                    ClassDO classDO = _ClassDAO.ObtainClassByID((int)userPO.ClassID);
                    ViewBag.ClassName = $"{classDO.Grade}({classDO.Group})";
                }

                response = View(userPO);
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
        public ActionResult ViewAllUsers()
        {
            ActionResult response;

            try
            {
                List<UserDO> allUsersDO = _UserDAO.ObtainAllUsers();
                List<UserPO> allUsersPO = UserMapper.DOtoPO(allUsersDO);
                response = View(allUsersPO);
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

        [SecurityFilter(role: 5)]
        [HttpGet]
        public ActionResult UpdateUser(int userID)
        {
            ActionResult response;

            try
            {
                UserDO userDO = _UserDAO.ObtainUserByID(userID);
                UserPO userPO = UserMapper.DOtoPO(userDO);

                response = View(userPO);
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

        [SecurityFilter(role: 5)]
        [HttpPost]
        public ActionResult UpdateUser(UserPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    if ((form.RoleID == 3 && form.HiredDate.HasValue) || form.RoleID != 3)
                    {
                        if ((form.RoleID == 4 && form.ClassID.HasValue) || form.RoleID != 4)
                        {
                            if ((form.RoleID == 5 && form.ChildID.HasValue) || form.RoleID != 5)
                            {
                                List<string> usernames = _UserDAO.ObtainAllUsernames();

                                if (!(usernames.Contains(form.Username)))
                                {
                                    UserDO userDO = UserMapper.POtoDO(form);
                                    _UserDAO.UpdateUser(userDO);

                                    //SAY YOU'VE SUCCESSFULLY UPDATED

                                    Session["Username"] = form.Username;
                                    Session["Password"] = form.Password;

                                    response = RedirectToAction("ViewAllUsers", "User");
                                }
                                else
                                {
                                    ModelState.AddModelError("Username", "This username has been taken.");
                                    response = View(form);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ChildID", "The ChildID feild is required.");
                                response = View(form);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("ClassID", "The ClassID feild is required.");
                            response = View(form);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("HiredDate", "The Hired date feild is required.");
                        response = View(form);
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
            }
            else
            {
                response = View(form);
            }

            return response;
        }

        [SecurityFilter(role: 2)]
        [HttpGet]
        public ActionResult DeleteUser(int userID)
        {
            ActionResult response;

            try
            {
                _UserDAO.DeleteUser(userID);
                response = RedirectToAction("ViewAllUsers", "User");
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

        [SecurityFilter(role: 2)]
        [HttpGet]
        public ActionResult CreateUser(int roleID)
        {
            ActionResult response;

            try
            {
                UserPO userPO = new UserPO();
                userPO.RoleID = roleID;

                if (userPO.RoleID == 3 || userPO.RoleID == 2)
                {
                    userPO.HiredDate = DateTime.Now;
                }

                response = View(userPO);
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

        [SecurityFilter(role: 2)]
        [HttpPost]
        public ActionResult CreateUser(UserPO form)
        {
            ActionResult response;
            if (ModelState.IsValid)
            {
                try
                {
                    if ((form.RoleID == 3 && form.HiredDate.HasValue) || form.RoleID != 3)
                    {
                        if ((form.RoleID == 4 && form.ClassID.HasValue) || form.RoleID != 4)
                        {
                            if ((form.RoleID == 5 && form.ChildID.HasValue) || form.RoleID != 5)
                            {
                                List<string> usernames = _UserDAO.ObtainAllUsernames();

                                if (!(usernames.Contains(form.Username)))
                                {
                                    UserDO userDO = UserMapper.POtoDO(form);
                                    _UserDAO.CreateUser(userDO);

                                    //SAY YOU'VE SUCCESSFULLY CREATED

                                    response = RedirectToAction("ViewAllUsers", "User");
                                }
                                else
                                {
                                    ModelState.AddModelError("Username", "This username has been taken.");
                                    response = View(form);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ChildID", "The ChildID feild is required.");
                                response = View(form);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("ClassID", "The ClassID feild is required.");
                            response = View(form);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("HiredDate", "The Hired date feild is required.");
                        response = View(form);
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
            }
            else
            {
                response = View(form);
            }

            return response;
        }
    }
}