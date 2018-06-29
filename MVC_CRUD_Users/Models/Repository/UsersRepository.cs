using MVC_CRUD_Users.Models.Classes;
using System.Collections.Generic;
using System.Linq;
using MVC_CRUD_Users.Models.Interfaces;

namespace MVC_CRUD_Users.Models.Repository
{
    public class UsersRepository : IRepository<User>
    {
        private static UsersRepository _repository;

        private List<User> _usersRepository;

        private UsersRepository()
        {
            _usersRepository = new List<User>();

            _usersRepository.Add(new User { FirstName = "Ivan", LastName = "Ivanov", Login = "ILog", Email = "ilog@asp.net", Password = "password", Role = "Пользователь" });
        }

        /// <summary>
        /// Получение экземпляра коллекции пользователей
        /// </summary>
        public static UsersRepository Repository => _repository ?? ( _repository = new UsersRepository() );

        /// <summary>
        /// Добавление пользователя в коллекцию
        /// </summary>
        /// <param name="item"></param>
        public void Add(User item)
        {
            //Получение значения id последнего пользователя в коллекции
            //Если пользователей еще нет - то значение -1
            //Прибавление к значению + 1
            item.Id = (_usersRepository.LastOrDefault()?.Id ?? -1) + 1;

            _usersRepository.Add(item);
        }

        /// <summary>
        /// Удаление пользователя из коллекции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id) => _usersRepository.Remove(GetOne(id));

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll() => _usersRepository;


        /// <summary>
        /// Получение польлзователя из коллекции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetOne(int id) => _usersRepository.Find(user => user.Id == id);

    }
}