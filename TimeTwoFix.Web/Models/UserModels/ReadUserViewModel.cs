namespace TimeTwoFix.Web.Models.UserModels
{
    public class ReadUserViewModel
    {
        ///Displying the user type in the UI
        public string UserType { get; set; }

        //Common properties
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ImageURL { get; set; }
        public DateOnly HireDate { get; set; }
        public int YearsOfExperience { get; set; }
        public string LastEmployer { get; set; }
        public string Status { get; set; }

        //Assistant properties
        public string? WorkStationNumber { get; set; }

        public string? PhoneExtension { get; set; }
        public string? SpokenLanguage { get; set; }
        public bool BusinessKnowledge { get; set; }

        //General Manager properties
        public string? OfficeNumber { get; set; }

        public int YearsInManagement { get; set; }

        //Mechanic properties
        public string? Specialization { get; set; }

        public string ToolBoxNumber { get; set; }
        public bool AbleToShift { get; set; }

        //Warehouse Manager properties
        public string? WarehouseName { get; set; }

        public string? WarehouseLocation { get; set; }
        public bool AbleToShiftWareHouse { get; set; }

        //Workshop Manager properties
        public int TeamSize { get; set; }
    }
}