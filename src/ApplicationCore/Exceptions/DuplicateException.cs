using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    // 自定义重复异常
    public class DuplicateException:Exception
    {
        public DuplicateException(string message):base(message)
        {
            
        }
    }
}
