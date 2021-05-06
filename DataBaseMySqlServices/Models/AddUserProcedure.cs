using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class AddUserProcedure
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public string DateOfBirth { get; set; }
        public char Sex { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string StudyField { get; set; }
        public int Degree { get; set; }
        public int Semestr { get; set; }
        public int CurrentUser { get; set; }
    }
}
