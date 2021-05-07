using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class StudentDataView
    {
        public string StudyFiled { get; set; }
        public int Degree { get; set; }
        public int Semestr { get; set; }
        public int IndexNumber { get; set; }
    }
}
