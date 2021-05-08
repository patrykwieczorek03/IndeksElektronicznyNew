using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class ChangeStudentDataProcedure
    {
        public int StudentID { get; set; }
        public string StudyField { get; set; }
        public int Degree { get; set; }
        public int Semestr { get; set; }
        public int CurrentUser { get; set; }
    }
}
