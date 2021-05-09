using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class BrowseGroupsDataView
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameOfCourse { get; set; }
        public int Ects { get; set; }
        public int GroupID { get; set; }
        public string Building { get; set; }
        public int Room { get; set; }
        public int DayOfWeek { get; set; }
        public char TypeOfClasses { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
    }
}
