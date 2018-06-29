using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_CRUD_Users.Models.Classes;
using MVC_CRUD_Users.Models.Interfaces;

namespace MVC_CRUD_Users.Models.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        private static RoleRepository _repository;

        private List<Role> _roleRepository;

        private RoleRepository()
        {
            _roleRepository = new List<Role>();

            _roleRepository.Add(new Role {RoleName = "Пользователь" });
        }

        /// <summary>
        /// Получение экземпляра коллекции ролей
        /// </summary>
        public static RoleRepository Repository => _repository ?? (_repository = new RoleRepository());

        /// <summary>
        /// Добавление роли в коллекцию
        /// </summary>
        /// <param name="item"></param>
        public void Add(Role item)
        {
            //Получение значения id последней роли в коллекции
            //Если пользователей еще нет - то значение -1
            //Прибавление к значению + 1
            item.Id = (_roleRepository.LastOrDefault()?.Id ?? -1) + 1;

            _roleRepository.Add(item);
        }

        /// <summary>
        /// Удаление роли из коллекции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id) => _roleRepository.Remove(GetOne(id));


        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> GetAll() => _roleRepository;

        /// <summary>
        /// Получение польлзователя из коллекции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetOne(int id) => _roleRepository.Find(item => item.Id == id);
        
    }
}