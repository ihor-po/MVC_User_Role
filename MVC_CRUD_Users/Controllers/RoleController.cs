using MVC_CRUD_Users.Models.Classes;
using MVC_CRUD_Users.Models.Interfaces;
using MVC_CRUD_Users.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MVC_CRUD_Users.Controllers
{
    public class RoleController : Controller
    {
        IRepository<User> repo = UsersRepository.Repository;
        IRepository<Role> roleRepo = RoleRepository.Repository;

        // GET: Role
        public ActionResult Index()
        {
            ViewBag.Title = "MVC CRUD :: Роли";
            ViewBag.Caption = "Список ролей";

            ViewBag.Roles = roleRepo.GetAll();

            return View();
        }

        //GET: Create Role
        [HttpGet]
        public ViewResult CreateRole()
        {
            ViewBag.Title = "MVC CRUD :: Создание роли";
            ViewBag.Caption = "Создание роли";

            return View("RoleForm");
        }

        //POST: Create Role
        [HttpPost]
        public ActionResult CreateRole(string RoleName)
        {
            ViewBag.Title = "MVC CRUD :: Создание роли";
            ViewBag.Caption = "Создание роли";
            ViewBag.NewRole = RoleName;

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ]{2,15}$";

            if (!Regex.Match(RoleName, text).Success)
            {
                ViewBag.RoleError = "Не менее 2 символов; Цифры не допустимы.";
                return View("RoleForm");
            }

            Role role = new Role();
            role.RoleName = RoleName;

            roleRepo.Add(role);

            return RedirectToAction("EditRole", new { id = role.Id });
        }


        //GET: Edit Role
        [HttpGet]
        public ViewResult EditRole(int id)
        {
            ViewBag.Title = "MVC CRUD :: Редакирование роли";
            ViewBag.Caption = "Редактирование роли";
            ViewBag.NewRole = roleRepo.GetOne(id).RoleName;

            return View("RoleForm");
        }

        //GET: Edit Role
        [HttpPost]
        public ActionResult EditRole(int id, string RoleName)
        {
            ViewBag.Title = "MVC CRUD :: Редакирование роли";
            ViewBag.Caption = "Редактирование роли";
            ViewBag.NewRole = RoleName;

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ]{2,15}$";

            if (!Regex.Match(RoleName, text).Success)
            {
                ViewBag.RoleError = "Не менее 2 символов; Цифры не допустимы.";
                return View("RoleForm");
            }

            Role role = roleRepo.GetOne(id);

            foreach (User user in repo.GetAll())
            {
                if (user.Role == role.RoleName)
                {
                    user.Role = RoleName;
                }
            }

            role.RoleName = RoleName;

            return RedirectToAction("EditRole", new { id = role.Id });
        }

        //GET: Delete role
        public ActionResult DeleteRole(int id)
        {
            Role role = roleRepo.GetOne(id);

            foreach (User user in repo.GetAll())
            {
                if (user.Role == role.RoleName)
                {
                    user.Role = "";
                }
            }

            roleRepo.Delete(id);

            return RedirectToAction("Index");
        }

    }
}