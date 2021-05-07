using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class ChangeLoginPasswordProcedure
    {
        public int UserToChangeLoginPassword { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int CurrentUser { get; set; }
    }
}
