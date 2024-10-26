using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Exceptions
{
    public class DomainException:Exception
    {
        public DomainException(string message):
            base($"Domain exception \"{message}\" thrown from the domain layer")
        {
            
        }
    }
}
