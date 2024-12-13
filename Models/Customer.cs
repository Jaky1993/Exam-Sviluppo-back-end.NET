namespace Models{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get;set; }
        public int RoleId { get;set; }

        public static List<Customer> customerList = new List<Customer>
        {
            new Customer {Id = 1, Name = "Jacopo", UserName = "jacopo1993", Email = "jacopo@gmail.com", Password = "jacopo1", RoleId = 1},
            new Customer {Id = 2, Name = "Paolo", UserName = "paolo0909", Email = "paolo@gmail.com", Password = "paolo1", RoleId = 2}
        };

        public static Customer FindCustomerById(int id)
        {
            Customer customer = customerList.FirstOrDefault(c => c.Id == id);

            return customer;
        }

        public static Customer FindCustomerByUsername(string username)
        {
            Customer customer = customerList.FirstOrDefault(c => c.UserName == username);

            return customer;
        }

        public void InputValidation(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                throw new Exception("Name is null");
            }

            if (string.IsNullOrWhiteSpace(customer.UserName))
            {
                throw new Exception("UserName is null");
            }

            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new Exception("Email is null");
            }

            if (string.IsNullOrWhiteSpace(customer.Password))
            {
                throw new Exception("Password is null");
            }

            if (customer.RoleId == 0)
            {
                throw new Exception("Role is null");
            }
       
            Role role = Role.FindRoleById(customer.RoleId);

            if (role == null)
            {
                throw new Exception("Role is not valid");
            }
        }
    }
}
