using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class ChangeGradeProcedure
    {
        public string NewGrade { get; set; }
        public string GradeStatus { get; set; }
        public int StudentID { get; set; }
        public int GroupID { get; set; }
        public int currentUser { get; set; }
    }
}
