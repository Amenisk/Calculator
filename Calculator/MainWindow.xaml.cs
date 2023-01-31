using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime;
using static System.Collections.Specialized.BitVector32;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _number;
        private string _action;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            tbAnswer.Text += btn.Content;
        }

        private void DeleteOneSymbol(object sender, RoutedEventArgs e)
        {
            if(tbAnswer.Text.Length != 0)
            {
                tbAnswer.Text = tbAnswer.Text.Substring(0, tbAnswer.Text.Length - 1);
            }
        }

        private void ResetToZero(object sender, RoutedEventArgs e)
        {
            tbAnswer.Text = "";
        }

        private void FullResetToZero(object sender, RoutedEventArgs e)
        {
            tbAnswer.Text = "";
            tbAction.Text = "";
            _number = 0;
            _action = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbAnswer.Text.Length != 0 && Char.IsDigit(tbAnswer.Text[tbAnswer.Text.Length - 1]) || tbAnswer.Text == "π")
            {
                for (int i = tbAnswer.Text.Length - 1; i >= 0; i--)
                {
                    if (!(Char.IsDigit(tbAnswer.Text[i]) || tbAnswer.Text[i] == ',' || tbAnswer.Text == "π"))
                    {
                        if (tbAnswer.Text[i] == '-')
                        {
                            tbAnswer.Text = tbAnswer.Text.Substring(0, i) + tbAnswer.Text.Substring(i + 1);
                            return;
                        }
                        else
                        {
                            tbAnswer.Text = tbAnswer.Text.Substring(0, i + 1) + '-' + tbAnswer.Text.Substring(i + 1);
                            return;
                        }
                    }
                    else
                    {
                        if (Char.IsDigit(tbAnswer.Text[i]) && i == 0 || tbAnswer.Text == "π")
                        {
                            tbAnswer.Text = '-' + tbAnswer.Text;
                            return;
                        }
                    }
                }
            }
            else WriteErrorMessage();
        }

        private void OperateWithTwoNumbers(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            var action = btn.Content.ToString();
            double number;

            if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
            {
                if(tbAnswer.Text == "π")
                {
                    number = Math.PI;
                }
                _number = number;
                tbAction.Text = number + $" {action} ";
                tbAnswer.Text = "";
                _action = action;
            }
            else WriteErrorMessage();
        }

        private void OperateWithOneNumber(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            _action = btn.Content.ToString();
            double number;

            GetAnswer(sender, e);
        }

        private void Log(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var action = btn.Content.ToString();
            double number;

            if(double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
            {
                if(number > 0)
                {
                    if (tbAnswer.Text == "π")
                    {
                        number = Math.PI;
                    }
                    _number = number;
                    tbAction.Text = $"log ({number})";
                    tbAnswer.Text = "";
                    _action = action;
                }
                else
                {
                    MessageBox.Show("Аргумент логарифма должно быть больше 0!");
                }
                
            }
            else WriteErrorMessage();
        }

        private void OperateWithFunction(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var action = btn.Content.ToString();
            double number;

            if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
            {
                if (tbAnswer.Text == "π")
                {
                    number = Math.PI;
                }
                _number = number;
                tbAction.Text = $"{action}({_number})";
                tbAnswer.Text = "";
                _action = action;
            }
            else WriteErrorMessage();

            GetAnswer(sender, e);
        }

        private void OperateWithDegree(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var action = btn.Content.ToString();
            double number;

            if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
            {
                if (tbAnswer.Text == "π")
                {
                    number = Math.PI;
                }
                _number = number;
                tbAction.Text = $"{number} ^ ";
                tbAnswer.Text = "";
                _action = action;
            }
            else WriteErrorMessage();

            GetAnswer(sender, e);
        }

        private void OperateWithRoot(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var action = btn.Content.ToString();
            double number;

            if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
            {
                if (tbAnswer.Text == "π")
                {
                    number = Math.PI;
                }
                _number = number;
                tbAction.Text = $"√{number}";
                tbAnswer.Text = "";
                _action = action;
            }
            else WriteErrorMessage();

            GetAnswer(sender, e);
        }

        private void Factorial(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            double number;

            if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
            {
                if (tbAnswer.Text == "π")
                {
                    number = Math.PI;
                }

                if (int.TryParse(number.ToString(), out int num) && num >= 0)
                {
                    _number = 1;
                    _action = btn.Content.ToString();
                    for (int i = 1; i <= number; i++)
                    {
                        _number *= i;
                    }
                    tbAnswer.Text = _number.ToString();
                    tbAction.Text = number + "!";
                }
                else
                {
                    MessageBox.Show("Число факториала должно быть целым и не меньше нуля!");
                }    
            }
            else WriteErrorMessage();
        }

        private void GetAnswer(object sender, RoutedEventArgs e)
        {
            double number;
            switch (_action)
            {
                case "+":
                    if(double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number += number;
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text += number;
                    }
                    else WriteErrorMessage();
                    break;

                case "-":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number -= number;
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text += number;         
                    }
                    else WriteErrorMessage();
                    break;

                case "*":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number *= number;
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text += number;
                    }
                    else WriteErrorMessage();
                    break;

                case "/":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if(number == 0)
                        {
                            MessageBox.Show("Делимое не может быть равно 0!");
                            return;
                        }
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number /= number;
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text += number;
                    }
                    else WriteErrorMessage();
                    break;

                case "%":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number = _number / 100 * number;
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text += number;
                    }
                    else WriteErrorMessage();
                    break;

                case "Mod":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number %= number;
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text += number;
                    }
                    else WriteErrorMessage();
                    break;

                case "ln":
                    if(_number > 0)
                    {
                        _number = Math.Log(_number);
                        tbAnswer.Text = _number.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Аргумент натурального логарифма должен быть больше 0!");
                    }
                    break;

                case "sin":
                    _number = Math.Sin(_number);
                    if (rbGraduce.IsChecked == true)
                        tbAnswer.Text = (_number * 180 / Math.PI).ToString();
                    else
                        tbAnswer.Text = _number.ToString();
                    break;

                case "cos":
                    _number = Math.Cos(_number);
                    if (rbGraduce.IsChecked == true)
                        tbAnswer.Text = (_number * 180 / Math.PI).ToString();
                    else
                        tbAnswer.Text = _number.ToString();
                    break;

                case "tan":
                    _number = Math.Tan(_number);
                    if (rbGraduce.IsChecked == true)
                        tbAnswer.Text = (_number * 180 / Math.PI).ToString();
                    else
                        tbAnswer.Text = _number.ToString();
                    break;

                case "sinh":
                    _number = Math.Sinh(_number);
                    if (rbGraduce.IsChecked == true)
                        tbAnswer.Text = (_number * 180 / Math.PI).ToString();
                    else
                        tbAnswer.Text = _number.ToString();
                    break;

                case "cosh":
                    _number = Math.Cosh(_number);
                    if (rbGraduce.IsChecked == true)
                        tbAnswer.Text = (_number * 180 / Math.PI).ToString();
                    else
                        tbAnswer.Text = _number.ToString();
                    break;

                case "tanh":
                    _number = Math.Tanh(_number);
                    if (rbGraduce.IsChecked == true)
                        tbAnswer.Text = (_number * 180 / Math.PI).ToString();
                    else
                        tbAnswer.Text = _number.ToString();
                    break;

                case "Exp":
                    _number = Math.Exp(_number);
                    tbAnswer.Text = _number.ToString();
                    break;

                case "x²":
                    _number *= _number;
                    tbAnswer.Text = _number.ToString();
                    tbAction.Text += 2;
                    break;

                case "x³":
                    _number *= _number * _number;
                    tbAnswer.Text = _number.ToString();
                    tbAction.Text += 3;
                    break;

                case "xᵞ":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number = Math.Pow(_number, number);
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text += number;
                    }
                    else WriteErrorMessage();
                    break;

                case "√":
                    if (_number > 0)
                    {
                        _number = Math.Sqrt(_number);
                        tbAnswer.Text = _number.ToString();
                    }
                    else
                    {
                        tbAction.Text = "";
                        tbAnswer.Text = _number.ToString();
                        MessageBox.Show("Число под квадратным корнем должно быть положительным!");
                    }
                    break;

                case "∛x":
                    _number = Math.Pow(_number, 1.0/3.0);
                    tbAnswer.Text = _number.ToString();
                    tbAction.Text = 3 + tbAction.Text;
                    break;

                case "ᵞ√x":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        if (int.TryParse(number.ToString(),out int num))
                        {
                            if(number % 2 == 0 && _number < 0)
                            {
                                tbAction.Text = "";
                                tbAnswer.Text = _number.ToString();
                                MessageBox.Show("Число под квадратным корнем должно быть положительным!");
                                return;
                            }
                            _number = Math.Pow(_number, 1.0 / number);
                            tbAnswer.Text = _number.ToString();
                            tbAction.Text = number + tbAction.Text;
                        }
                        else
                        {
                            MessageBox.Show("Степень корня должна быть положительным числом!");
                        }
                    }
                    else WriteErrorMessage();
                    break;

                case "10ˣ":
                    if(double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        _number = Math.Pow(10, number);
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text = 10 + " ^ " + number;
                    }
                    else WriteErrorMessage();
                    break;

                case "1/x":
                    if(double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if(number == 0)
                        {
                            MessageBox.Show("Число не должно быть равно 0!");
                            return;
                        }
                        if (tbAnswer.Text == "π")
                        {
                            number = Math.PI;
                        }
                        _number = 1/number;
                        tbAnswer.Text = _number.ToString();
                        tbAction.Text = 1 + " / " + number;
                    }
                    else WriteErrorMessage();
                    break;

                case "log":
                    if (double.TryParse(tbAnswer.Text, out number) || tbAnswer.Text == "π")
                    {
                        if(number > 0)
                        {
                            if (tbAnswer.Text == "π")
                            {
                                number = Math.PI;
                            }
                            tbAction.Text = $"log{number}({_number})";
                            _number = Math.Log(_number, number);
                            tbAnswer.Text = _number.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Основание логарифма должно быть больше 0!");
                        }
                        
                    }
                    else WriteErrorMessage();
                    break;
            }
        }

        private void WriteErrorMessage()
        {
            MessageBox.Show("Некорректные данные!");
        }
    }
}
