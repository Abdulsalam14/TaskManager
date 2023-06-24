using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp007
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public ObservableCollection<Process> Processes { get; set; }
        public ObservableCollection<string> BlackList { get; set; } = new();

        private Timer timer;

        private int totalthread;

        public int TotalThread
        {
            get { return totalthread; }
            set
            {
                totalthread = value;
                OnPropertyChanged();
            }
        }

        private int totalhandle;

        public int TotalHandle
        {
            get { return totalhandle; }
            set
            {
                totalhandle = value;
                OnPropertyChanged();
            }
        }


        private double columnsize;

        public double ColumnSize
        {
            get { return columnsize; }
            set
            {
                columnsize = value;
                OnPropertyChanged();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
            timer = new Timer(5000);
            timer.Elapsed += updateProcesses;
            timer.Start();
            timer.Elapsed += CheckBlackList;
            ColumnSize = listview.Width /4-7;
            TotalThread = Processes.Sum(b => b.Threads.Count);
            TotalHandle = Processes.Sum(b => b.HandleCount);
        }

        private void updateProcesses(object? sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Processes.Clear();
                foreach (Process process in Process.GetProcesses())
                {
                    Processes.Add(process);
                }
                TotalThread = Processes.Sum(b => b.Threads.Count);
                TotalHandle = Processes.Sum(b => b.HandleCount);
            });
        }

        public void CheckBlackList(object? sender, ElapsedEventArgs e)
        {
            var blacklistedProcesses = Process.GetProcesses()
           .Where(p => BlackList.Contains(p.ProcessName)).ToList();
            blacklistedProcesses.ForEach(a => a.Kill());
        }

        private void OnPropertyChanged([CallerMemberName] string propertyname = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public event PropertyChangedEventHandler? PropertyChanged;


        private void Listview_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ColumnSize = listview.ActualWidth / 4 - 7;

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            NewTask newtask = new NewTask(this);
            newtask.Show();
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            if (listview.SelectedItem != null)
            {
                var selectedProcess = listview.SelectedItem as Process;
                if (selectedProcess != null)
                {
                    selectedProcess.Kill();
                    selectedProcess.WaitForExit();
                    Processes.Remove(selectedProcess);
                }
            }
            else
            {
                MessageBox.Show("Select");
            }
        }

        private void Black_List_Click(object sender, RoutedEventArgs e)
        {
            BlackListWindow blackwindow = new(this);
            blackwindow.Show();
        }

        private void Add_Black_List_Click(object sender, RoutedEventArgs e)
        {
                Process? p = listview.SelectedItem as Process;
            if (p != null)
            {
                BlackList.Add(p.ProcessName);
                MessageBox.Show("Added");
            }
            else MessageBox.Show("Select");
        }
    }
}
