namespace MVC_CRUD_Users.Models.Classes
{
    public class Role
    {
        private int id;
        private string roleName;

        /// <summary>
        /// Role id
        /// </summary>
        public int Id { get => id; set { id = value; } }

        /// <summary>
        /// Role name
        /// </summary>
        public string RoleName { get => roleName; set { roleName = value; } }
    }
}