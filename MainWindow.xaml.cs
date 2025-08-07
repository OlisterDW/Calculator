using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace Calculator
{
    public partial class MainWindow : Window
    {
        private string firstNumber { get; set; }
        private string secondNumber { get; set; }
        private string operation { get; set; }
        private bool isCalculated { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (isCalculated)
            {
                InputLabel.Content = (e.OriginalSource as Button).Content.ToString();
                isCalculated = false;
            }
            else
            {
                InputLabel.Content += (e.OriginalSource as Button).Content.ToString();
            }

        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            // Замена одного арифметического знака на другой
            if (Regex.IsMatch(CalculationLabel.Content.ToString(), @"[\+\-\×/]$") && InputLabel.Content.ToString() == "")   
            {
                operation = (e.OriginalSource as Button).Content.ToString();
                CalculationLabel.Content = CalculationLabel.Content.ToString().Substring(0, CalculationLabel.Content.ToString().Length - 1) + operation;
                return;
            }

            // Если вводится знак, когда нет чисел, то первое число  - ноль
            if (CalculationLabel.Content.ToString() == "" && InputLabel.Content.ToString() == "")
            {
                InputLabel.Content = "0";
            }

            // Сохраняется первое число
            if (CalculationLabel.Content.ToString() == "")         
            {
                firstNumber = InputLabel.Content.ToString().TrimEnd(',');
            }
            // Сохраняется второе число
            else
            {

                secondNumber = InputLabel.Content.ToString().TrimEnd(',');

                if (secondNumber == "0" && operation == "/")
                {
                    MessageBox.Show("Ошибка! На ноль делить нельзя!");
                }
                else
                {
                    firstNumber = Calculate(decimal.Parse(firstNumber), decimal.Parse(secondNumber), operation);
                }

            }
            operation = (e.OriginalSource as Button).Content.ToString();
            
            CalculationLabel.Content = firstNumber + operation;
            InputLabel.Content = "";
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalculationLabel.Content != "" && InputLabel.Content == "")
            {
                firstNumber = Calculate(decimal.Parse(firstNumber), 0, operation);
            }
            else if(CalculationLabel.Content != "" && InputLabel.Content != "")
            {
                secondNumber = InputLabel.Content.ToString();
                firstNumber  = Calculate(decimal.Parse(firstNumber), decimal.Parse(secondNumber), operation);
            }
            InputLabel.Content = firstNumber;
            CalculationLabel.Content = "";
            operation = "";
            firstNumber = "";
            secondNumber = "";
            isCalculated = true;
        }

        private void AlgebraicSignButton_Click(object sender, RoutedEventArgs e)
        {
            string number = InputLabel.Content.ToString();
            if (number == "" || number == "0")
                return;

            if (number.StartsWith('-'))
            {
                InputLabel.Content = number.Remove(0, 1);
            }
            else
            {
                InputLabel.Content = "-" + number;
            }
        }

        private void FloatPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputLabel.Content.ToString().Contains(','))
            {
                InputLabel.Content += ",";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CalculationLabel.Content = "";
            InputLabel.Content = "";
            operation = "";
            firstNumber = "";
            secondNumber = "";
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalculationLabel.Content == "" && InputLabel.Content != "")
            {
                InputLabel.Content = (decimal.Parse(InputLabel.Content.ToString()) / 100).ToString();
                
            }
            else if (CalculationLabel.Content != "" && InputLabel.Content == "")
            {
                decimal percent = (decimal.Parse(firstNumber) / 100);
                firstNumber = Calculate(decimal.Parse(firstNumber), percent, operation);
                secondNumber = "";
                operation = "";
                CalculationLabel.Content = firstNumber;
                InputLabel.Content = "";
            }
            else
            {
                secondNumber = InputLabel.Content.ToString();
                decimal percent = decimal.Parse(firstNumber) * decimal.Parse(secondNumber) / 100;
                firstNumber = Calculate(decimal.Parse(firstNumber), percent, operation);
                secondNumber = "";
                operation = "";
                CalculationLabel.Content = firstNumber;
                InputLabel.Content = "";
            }

        }
        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputLabel.Content != "")
            {
                InputLabel.Content = InputLabel.Content.ToString().Substring(0, InputLabel.Content.ToString().Length - 1);
            }
        }

        private string Calculate(decimal firstNumber, decimal secondNumber, string operation)
        {
            switch (operation)
            {
                case "+":
                    return (firstNumber + secondNumber).ToString();
                case "-":
                    return (firstNumber - secondNumber).ToString();
                case "×":
                    return (firstNumber * secondNumber).ToString();
                case "/":
                    return (firstNumber / secondNumber).ToString();
                default:
                    return null;
            }
        }

        
    }
}
