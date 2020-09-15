using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Team_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Task> Tasks = new ObservableCollection<Task>();

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Add_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add Task Button Click Not Implemented");
        }

        private void Edit_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit Task Button Click Not Implemented");
        }

        private void Delete_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete Task Button Click Not Implemented");
        }

        private void Employees_Button_Click(object sender, RoutedEventArgs e)
        {
            TaskEditor task = new TaskEditor();
            task.ShowDialog();

            if (task.Result == Team_Manager.DialogResult.OK)
            {
                MessageBox.Show("New Task Add Not Implemented");
            }
        }
    }
}
