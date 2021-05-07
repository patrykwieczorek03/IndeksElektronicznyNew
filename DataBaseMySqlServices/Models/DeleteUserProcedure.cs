using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseMySqlServices.Models
{
    public class DeleteUserProcedure
    {
        public int UserToDrop { get; set; }
        public int CurrentUser { get; set; }
    }
}
