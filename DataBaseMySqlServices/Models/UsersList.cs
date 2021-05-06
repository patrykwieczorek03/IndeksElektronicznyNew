using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class UsersList
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public char Role { get; set; }
    }
}
