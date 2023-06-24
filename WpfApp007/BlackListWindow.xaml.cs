using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace WpfApp007
{
    /// <summary>
    /// Interaction logic for BlackListWindow.xaml
    /// </summary>
    /// 

    public partial class BlackListWindow : Window
    {
        public MainWindow mainWindow { get; set; }
        public BlackListWindow(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            DataContext = mainWindow;
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (txt.Text != "")
            {
                mainWindow.BlackList.Add(txt.Text);
                txt.Text = "";
            }
            else MessageBox.Show("Enter ProcessName");
        }

        private void Removebtn_Click(object sender, RoutedEventArgs e)
        {
            if (listview2.SelectedItem != null)
            {
                mainWindow.BlackList.Remove(listview2.SelectedItem.ToString()!);
            }
            else MessageBox.Show("Select");
        }
    }
}
