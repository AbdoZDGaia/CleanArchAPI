using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class InvalidCredentialsException : NotFoundException
    {
        public InvalidCredentialsException() :
            base($"Invalid username or password")
        {

        }
    }
}
