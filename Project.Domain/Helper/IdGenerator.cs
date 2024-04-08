using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Helper
{
    public static class IdGenerator
    {
        public static string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
