using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class LecturerGroupsDataView
    {
        public int GroupID { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public int DayOfWeek { get; set; }
        public char TypeOfClasses { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
