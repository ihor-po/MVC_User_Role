namespace MVC_CRUD_Users.Models.Classes
{
    public class User
    {

        private int id;
        private string login;
        private string password;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private string role;

        /// <summary>
        /// User id
        /// </summary>
        public int Id { get => id; set { id = value; } }

        /// <summary>
        /// User login
        /// </summary>
        public string Login { get => login; set { login = value; } }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get => password; set { password = value; } }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get => firstName; set { firstName = value; } }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get => lastName; set { lastName = value; } }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get => email; set { email = value; } }

        /// <summary>
        /// User phone
        /// </summary>
        public string Phone { get => phone; set { phone = value; } }

        /// <summary>
        /// User Role
        /// </summary>
        public string Role { get => role; set { role = value; } }

    }
}