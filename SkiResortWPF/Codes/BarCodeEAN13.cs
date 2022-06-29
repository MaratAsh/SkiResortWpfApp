using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SkiResortWPF.Codes
{
    class BarCodeEAN13: BarCodeInterface
    {
        public int countryCode { get; }
        public int enterpriseCode { get; }
        public int productCode { get; }
        public int specialCode { get; set; }

        private string[] leftA = { "0001101", "0011001", "0010011", "0111101",
                                                "0100011", "0110001", "0101111",
                                                "0111011", "0110111", "0001011" };
        private string[] leftB = { "0100111", "0110011", "0011011", "0100001",
                                                "0011101", "0111001", "0000101",
                                                "0010001", "0001001", "0010111" };
        private string[] right = { "1110010", "1100110", "1101100", "1000010", 
                                                "1011100", "1001110", "1010000", 
                                                "1000100", "1001000", "1110100" };
        private string _space = "01010";

        private string[] leftRule = { "AAAAAA", "AABABB", "AABBAB", "AABBBA",
                                                "ABAABB", "ABBAAB", "ABBBAA",
                                                "ABABAB", "ABABBA", "ABBABA"};

        private double _unit_part_width = 2;
        private double _unit_space_width = 0.1;
        private double _unit_long_height = 100;
        private double _unit_height = 90;



        public BarCodeEAN13(int countryCode, int enterpriseCode, int productCode)
        {
            this.countryCode = countryCode;
            this.enterpriseCode = enterpriseCode;
            this.productCode = productCode;
            setSpecialCode();
        }
        private void setSpecialCode()
        {
            int sum_even = Tools.getDigitAt(countryCode, 2) + 
                Tools.getDigitAt(enterpriseCode, 4) + Tools.getDigitAt(enterpriseCode, 2) +
                Tools.getDigitAt(productCode, 5) + Tools.getDigitAt(productCode, 3) + Tools.getDigitAt(productCode, 1);
            int sum_not_even = Tools.getDigitAt(countryCode, 3) + Tools.getDigitAt(countryCode, 1) +
                Tools.getDigitAt(enterpriseCode, 3) + Tools.getDigitAt(enterpriseCode, 1) +
                Tools.getDigitAt(productCode, 4) + Tools.getDigitAt(productCode, 2);

            this.specialCode = (sum_even * 3 + sum_not_even) % 10;
        }

        public void DrawOn(Canvas control)
        {

        }

        private void ConstructUnit(Canvas control, int unit_num, string pattern, double height)
        {
            Shape shape;
            int part_width = 0;
            int part_start_index = 0;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '0')
                {
                    part_width = 0;
                }
                else
                {
                    if (part_width == 0)
                        part_start_index = i;
                    part_width++;
                    if (i == pattern.Length - 1 || i < pattern.Length - 1 && pattern[i + 1] == '0')
                    {
                        shape = new Rectangle();
                        shape.Width = part_width * _unit_part_width;
                        shape.Height = height;
                        shape.Fill = new SolidColorBrush(Colors.Black);
                        Canvas.SetLeft(
                            shape, 
                            // unit_num * (7 + _unit_space_width) + part_start_index * _unit_part_width
                            unit_num * (7 * _unit_part_width + _unit_space_width) + part_start_index * _unit_part_width
                        );

                        control.Children.Add(
                            shape
                        );
                    }
                }

            }
        }
        private void ConstructUnitLabel(Canvas control, int unit_num, int digit)
        {
            Label label = new Label();
            Canvas.SetLeft(label, unit_num * (7 * _unit_part_width + _unit_space_width) - 5);
            Canvas.SetTop(label, _unit_height - 2);
            label.Content = (digit % 10).ToString();
            //label.Background = new SolidColorBrush(Colors.Red);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            control.Children.Add(label);
        }
        private void ConstructUnitDigit(Canvas control, int unit_num, int digit, string[] patterns)
        {
            ConstructUnit(control, unit_num, patterns[digit], _unit_height);
            ConstructUnitLabel(control, unit_num, digit);
        }

        public void ConstructOn(Canvas control)
        {
            Shape shape;
            control.Children.Clear();

            shape = new Rectangle();
            shape.Margin = new Thickness(0, 0, 0, 10);
            shape.Height = _unit_long_height + 10;
            shape.Width = 17 * (_unit_part_width * 7 + _unit_space_width);
            shape.Fill = new SolidColorBrush(Colors.White);

            control.Children.Add(
                shape
            );

            int drawn_units = 0;
            int identify_num = Tools.getDigitAt(this.countryCode, 3);
            // Unit
            ConstructUnitLabel(control, drawn_units, identify_num);
            drawn_units++;

            ConstructUnit(control, drawn_units, _space, _unit_long_height);
            drawn_units++;

            for (int i = 1; i < 3; i++)
            {
                int current_digit = Tools.getDigitAt(this.countryCode, 3 - i);
                if (leftRule[identify_num][i - 1] == 'A')
                    ConstructUnitDigit(control, drawn_units, current_digit, leftA);
                else
                    ConstructUnitDigit(control, drawn_units, current_digit, leftB);
                drawn_units++;
            }

            for (int i = 0; i < 4; i++)
            {
                int current_digit = Tools.getDigitAt(this.enterpriseCode, 4 - i);
                if (leftRule[identify_num][2 + i] == 'A')
                    ConstructUnitDigit(control, drawn_units, current_digit, leftA);
                else
                    ConstructUnitDigit(control, drawn_units, current_digit, leftB);
                drawn_units++;
            }

            ConstructUnit(control, drawn_units, _space, _unit_long_height);
            drawn_units++;

            for (int i = 0; i < 5; i++)
            {
                int current_digit = Tools.getDigitAt(this.productCode, 5 - i);
                ConstructUnitDigit(control, drawn_units, current_digit, right);
                drawn_units++;
            }

            ConstructUnitDigit(control, drawn_units, this.specialCode, right);
            drawn_units++;
            ConstructUnit(control, drawn_units, _space, _unit_long_height);

        }
    }
}
