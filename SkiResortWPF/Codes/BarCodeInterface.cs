using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SkiResortWPF.Codes
{
    interface BarCodeInterface
    {
        int countryCode { get; }
        int enterpriseCode { get; }
        int productCode { get; }
        int specialCode { get; }

        void DrawOn(Canvas control);
        void ConstructOn(Canvas control);
    }
}
