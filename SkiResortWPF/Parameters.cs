using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF
{
    public static class Parameters
    {
        public static int NotAuthenticated          = 0b00000;
        public static int PasswordNotCorrect        = 0b01000;
        public static int EmailNotCorrect           = 0b10000;
        public static int Authenticated             = 0b00001;
        public static int AuthenticatedAsClient     = 0b00101;
        public static int AuthenticatedAsEmployee   = 0b00011;
    }
}
