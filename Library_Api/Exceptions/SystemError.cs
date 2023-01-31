using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Exceptions
{
    public class SystemError :Exception
    {
        public SystemError(string message):base(message)
        {

        }
    }
}
