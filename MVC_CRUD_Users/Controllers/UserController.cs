using MVC_CRUD_Users.Models.Classes;
using MVC_CRUD_Users.Models.Interfaces;
using MVC_CRUD_Users.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace MVC_CRUD_Users.Controllers
{
    public class UserController : Controller
    {
        IRepository<User> repo = UsersRepository.Repository;
        IRepository<Role> roleRepo = RoleRepository.Repository;

        // GET: User
        public ActionResult Index()
        {
            ViewBag.Title = "MVC CRUD :: Пользователи";
            ViewBag.Caption = "Список пользователей";

            ViewBag.Users = repo.GetAll();

            return View();
        }

        // GET: Create User
        [HttpGet]
        public ViewResult CreateUser()
        {
            ViewBag.Title = "MVC CRUD :: Создание пользователя";
            ViewBag.Caption = "Создание пользователя";

            ViewBag.Roles = roleRepo.GetAll();

            return View("UserForm");
        }

        //POST: Create User
        public ActionResult CreateUser(FormCollection request)
        {
            ViewBag.Title = "MVC CRUD :: Создание пользователя";
            ViewBag.Caption = "Создание пользователя";

            ViewBag.Roles = roleRepo.GetAll();

            ViewBag.uLogin = request["Login"];
            ViewBag.uPassword = request["Password"];
            ViewBag.uFirstName = request["FirstName"];
            ViewBag.uLastName = request["LastName"];
            ViewBag.uEmail = request["Email"];
            ViewBag.uPhone = request["Phone"];
            ViewBag.uRole = request["Role"];

            User newUser = new User();

            foreach (string item in request.Keys)
            {
                switch (item)
                {
                    case "Password":
                        string password = @"(?=.*\d).{6,}";
                        if (!Regex.Match(request[item], password).Success)
                        {
                            ViewData[item] = "Не менее 6 символов; Хотя бы одна цифра";
                        }
                        break;
                    case "Email":
                        if (!string.IsNullOrWhiteSpace(request[item]))
                        {
                            try
                            {
                                MailAddress m = new MailAddress(request[item]);
                            }
                            catch (FormatException ex)
                            {
                                ViewData[item] = ex.Message;
                            }
                        }
                        else
                        {
                            ViewData[item] = "Поле не должно быть пустым";
                        }
                        break;
                    case "Phone":
                        string phoneNumber = @"380\d{2}-\d{3}-\d{2}-\d{2}";
                        if (!Regex.Match(request[item], phoneNumber).Success)
                        {
                            ViewData[item] = "Не соответствует формату 38000-000-00-00";
                        }
                        break;
                    default:
                        string text = @"^[a-zA-Zа-яА-ЯіІїЇ]{2,25}$";
                        if (!Regex.Match(request[item], text).Success)
                        {
                            ViewData[item] = "Не менее 2 символов; Цифры не допустимы";
                        }
                        break;
                }
            }

            if (ViewData.Values.Count > 10)
            {
                return View("UserForm");
            }

            newUser = FillUsersData(newUser, request);

            repo.Add(newUser);

            return RedirectToAction("EditUser", new { id = newUser.Id });

        }

        // GET: Edit User
        [HttpGet]
        public ViewResult EditUser(int id)
        {
            ViewBag.Title = "MVC CRUD :: Редактирование пользователя";
            ViewBag.Caption = "Редактирование пользователя";

            ViewBag.Roles = roleRepo.GetAll();

            User user = repo.GetOne(id);

            ViewBag.uLogin = user.Login;
            ViewBag.uPassword = user.Password;
            ViewBag.uFirstName = user.FirstName;
            ViewBag.uLastName = user.LastName;
            ViewBag.uEmail = user.Email;
            ViewBag.uPhone = user.Phone;
            ViewBag.uRole = user.Role;

            return View("UserForm");
        }

        // POST: Edit User
        [HttpPost]
        public ActionResult EditUser(int id, FormCollection request)
        {
            ViewBag.Title = "MVC CRUD :: Создание редактирование";
            ViewBag.Caption = "Редактирование пользователя";

            ViewBag.Roles = roleRepo.GetAll();

            User user = repo.GetOne(id);

            ViewBag.uLogin = request["Login"];
            ViewBag.uPassword = request["Password"];
            ViewBag.uFirstName = request["FirstName"];
            ViewBag.uLastName = request["LastName"];
            ViewBag.uEmail = request["Email"];
            ViewBag.uPhone = request["Phone"];
            ViewBag.uRole = request["Role"];

            foreach (string item in request.Keys)
            {
                switch (item)
                {
                    case "Password":
                        string password = @"(?=.*\d).{6,}";
                        if (!Regex.Match(request[item], password).Success)
                        {
                            ViewData[item] = "Не менее 6 символов; Хотя бы одна цифра";
                        }
                        break;
                    case "Email":
                        if (!string.IsNullOrWhiteSpace(request[item]))
                        {
                            try
                            {
                                MailAddress m = new MailAddress(request[item]);
                            }
                            catch (FormatException ex)
                            {
                                ViewData[item] = ex.Message;
                            }
                        }
                        else
                        {
                            ViewData[item] = "Поле не должно быть пустым";
                        }
                        break;
                    case "Phone":
                        string phoneNumber = @"380\d{2}-\d{3}-\d{2}-\d{2}";
                        if (!Regex.Match(request[item], phoneNumber).Success)
                        {
                            ViewData[item] = "Не соответствует формату 38000-000-00-00";
                        }
                        break;
                    default:
                        string text = @"^[a-zA-Zа-яА-ЯіІїЇ]{2,25}$";
                        if (!Regex.Match(request[item], text).Success)
                        {
                            ViewData[item] = "Не менее 2 символов; Цифры не допустимы";
                        }
                        break;
                }
            }

            if (ViewData.Values.Count > 10)
            {
                return View("UserForm");
            }

            user = FillUsersData(user, request);

            return View("UserForm", new { id = user.Id });
        }

        // GET: Delete user
        public ActionResult DeleteUser(int id)
        {
            repo.Delete(id);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Заполнение полей пользователя при создании / редактировании
        /// </summary>
        /// <param name="u"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private User FillUsersData (User u, FormCollection request)
        {
            foreach (var item in u.GetType().GetProperties())
            {
                string reqKey;
                if (!string.IsNullOrWhiteSpace(
                
                    reqKey = request.Keys
                    .OfType<string>()
                    .FirstOrDefault(key => string.Equals(key, item.Name, StringComparison.CurrentCultureIgnoreCase))))
                {
                    item.SetValue(u, Convert.ChangeType(request[reqKey], item.PropertyType));
                }
                
            }
                return u;
        }
    }
}