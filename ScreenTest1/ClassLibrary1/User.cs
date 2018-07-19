using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class UserInfo
    {
        public string UserID { get; set; }
        public string LargePersonGroupID { get; set; }
        public string Name { get; set; }
        public Guid PersonGuid { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
