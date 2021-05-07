using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class StudentsList
    {
        public int UserID { get; set; }
        public int IndexNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public string StudyFiled { get; set; }
        public int Degree { get; set; }
        public int Semestr { get; set; }
        public string ContactNumber { get; set; }
    }
}
