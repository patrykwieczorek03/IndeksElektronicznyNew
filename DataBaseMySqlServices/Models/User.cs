using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class User
    {
        public int UserID;
        public char Role;

        public bool CheckUser()
        {
            if ((this.UserID == -1) && (this.Role == 'n'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
