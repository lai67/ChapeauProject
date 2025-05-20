namespace Model
{
    public enum Role
    {
        Waiter, 
        Chef,
        Barman,
        Manager
    }
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }


        public Employee(int id, string firstName, string lastName, string password,Role  role)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Role = role;
        }


        public Employee() { }
                                                
    }

    
}
