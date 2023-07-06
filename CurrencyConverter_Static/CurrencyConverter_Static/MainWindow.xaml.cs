using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CurrencyConverter_Static
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency(); 
        }
        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            dtCurrency.Rows.Add("--SELECT--",0);
            dtCurrency.Rows.Add("EUR",1);
            dtCurrency.Rows.Add("PLN",4.46);
            dtCurrency.Rows.Add("USD",1.09);
            dtCurrency.Rows.Add("RUB",98.89);
            dtCurrency.Rows.Add("NOK", 11.62);
            dtCurrency.Rows.Add("CHF", 0.98);

            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath= "Text";
            cmbToCurrency.SelectedValuePath="Value"; 
            cmbToCurrency.SelectedIndex = 0;
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            double ConvertedValue;
            //Sprawdza czy wartosc podana w textbox nie jest null albo pusta
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Currency","Information",MessageBoxButton.OK,MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            }
            else if(cmbFromCurrency.SelectedValue== null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            }
            else if(cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            }

            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                ConvertedValue = double.Parse(txtCurrency.Text);

                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                ConvertedValue = (double.Parse(cmbToCurrency.SelectedValue.ToString()) *
                    double.Parse(txtCurrency.Text)) /
                    double.Parse(cmbFromCurrency.SelectedValue.ToString());

                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled= regex.IsMatch(e.Text);   

            
        }

        private void cmbFromCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if(cmbFromCurrency.Items.Count > 0) cmbFromCurrency.SelectedIndex = 0;
            if(cmbToCurrency.Items.Count>0) cmbToCurrency.SelectedIndex = 0;
            lblCurrency.Content = "";
            txtCurrency.Focus();
        }
    }
}
