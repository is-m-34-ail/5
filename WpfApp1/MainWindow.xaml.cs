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
namespace WpfApp1

{
    public partial class MainWindow : Window
    {
        private IIntegralCalculator calculator;
         
        public MainWindow()
        {
            InitializeComponent();
            calculator = new TrapezoidalIntegralCalculator(x => ((x) * (x)));
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxA.Text, out double a) &&
                double.TryParse(TextBoxB.Text, out double b) &&
                int.TryParse(TextBoxN.Text, out int n) && n > 0)
            {
                // Выбор метода по RadioButton (например)
                if (MethodSimpsonRadio.IsChecked == true)
                    calculator = new SimpsonIntegralCalculator(x => ((x)*(x)));
                else
                    calculator = new TrapezoidalIntegralCalculator(x => ((x) * (x)));

                double result = calculator.Calculate(a, b, n);
                ResultText.Text = $"Результат: {result:F6}";
            }
            else
            {
                MessageBox.Show("Введите корректные значения.");
            }
        }
    }
}
