namespace TimeTwoFix.Application.UserServices.Dtos.Users
{
    public class CreateUserDto
    {
        ///Displying the user type in the UI
        public string UserType { get; set; }

        //Common properties
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public int ZipCode { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
        public string? ImageURL { get; set; }
        public DateOnly HireDate { get; set; }
        public int YearsOfExperience { get; set; }
        public required string LastEmployer { get; set; }
        public string? Status { get; set; }

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