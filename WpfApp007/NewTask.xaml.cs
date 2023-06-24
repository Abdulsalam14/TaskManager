using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for NewTask.xaml
    /// </summary>
    public partial class NewTask : Window
    {
        public MainWindow main { get; set; }
        public NewTask(MainWindow mainWindow)
        {
            InitializeComponent();
            main = mainWindow;
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtbx.Text))
            {
                try
                {
                    var pr=Process.Start(txtbx.Text);
                    main.Processes.Add(pr);
                    App.Current.Windows[1].Close();

                }
                catch (Exception)
                {

                    MessageBox.Show("Name was not found");
                    txtbx.Text="";
                }
            }
            else
            {
                MessageBox.Show("Enter ProcessName");
            }
        }
    }
}
