namespace Models
{
    public class Role
    {
        public int Id { get;set; }
        public string Name { get;set; }



        public static List<Role> roleList = new List<Role>
        {
            new Role {Id = 1, Name = "Admin" },
            new Role {Id = 2, Name = "User" }
        };

        public List<Role> RoleList
        {
            get
            {
                return roleList;
            }
        }

        public static Role FindRoleById(int id)
        {
            Role role = roleList.FirstOrDefault(r => r.Id == id);

            return role;
        }

        public static Role FindRoleByName(string roleName)
        {
            Role role = roleList.FirstOrDefault(r => r.Name == roleName);

            return role;
        }

    }
}
