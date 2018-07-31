namespace ShippingStationLogin.Objects
{
    public class Employee
    {
        public string UserId { get; set;  }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee(string UserId, string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserId = UserId;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

    }
}
