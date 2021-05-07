using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class LecturerCursesDataView
    {
        public int IndexCourse { get; set; } 
        public string NameOfCourse { get; set; }
        public int Ects { get; set; }
    }
}
