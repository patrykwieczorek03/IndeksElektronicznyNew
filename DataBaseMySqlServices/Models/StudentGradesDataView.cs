using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class StudentGradesDataView
    {
        public int MembershipID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameOfCoure { get; set; }
        public int Ects { get; set; }
        public int GroupID { get; set; }
        public float Grade { get; set; }
        public char GradeStatus { get; set; }   
    }
}
