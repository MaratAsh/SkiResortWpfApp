using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF
{
    public static class Tools
    {
        public static int getDigitAt(int number, int index)
        {
            if (number < 0)
                return getDigitAt(-number, index);
            return (int) (number % Math.Pow(10, index) / Math.Pow(10, index - 1));
        }
        public static int getCountDigits(int number)
        {
            int i = 1;
            while (number > 9)
            {
                number /= 10;
                i++;
            }
            return i;
        }

        public static bool checkExistanceEAN13(string code)
        {
            int _sum_even = 0, _sum_not_even = 0;
            int digit_num = 1;
            foreach (char symbol in code)
            {
                if (symbol >= '0' && symbol <= '9')
                {
                    if (digit_num % 2 == 0)
                    {
                        _sum_even += symbol - '0';
                    }
                    else
                    {
                        _sum_not_even += symbol - '0';
                    }
                    digit_num++;
                }
            }
            if (digit_num != 13)
                return (false);
            return (true);
        }
    }
}
