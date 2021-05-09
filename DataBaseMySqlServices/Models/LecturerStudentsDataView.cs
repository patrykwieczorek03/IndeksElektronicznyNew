using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class LecturerStudentsDataView
    {
        public int IndexNumber { get; set; }
        public string NameOfCourse { get; set; }
        public int GroupID { get; set; }
        public char TypeOfClasses { get; set; }
        public float Grade { get; set; }
        public char GradeStatus { get; set; }
    }
}
