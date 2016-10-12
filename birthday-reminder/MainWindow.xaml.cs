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

namespace birthday_reminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "firstname")
            {
                e.Column.Header = "Vorname";
            }
            else if (e.Column.Header.ToString() == "lastname")
            {
                e.Column.Header = "Nachname";
            }
            else if (e.Column.Header.ToString() == "birthday")
            {
                e.Column.Header = "Geburtsdatum";
            }
            else if (e.Column.Header.ToString() == "notification")
            {
                e.Column.Header = "Erinnerung";
            }
        }
    }
}
