using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Properties

        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();
        
        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (_SelectedIndex == value || value < 0)
                    return;

                _SelectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        private Task _SelectedItem;
        /// <summary>
        /// 
        /// </summary>
        public Task SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem == value)
                    return;

                _SelectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private Employee _Employee;
        /// <summary>
        /// 
        /// </summary>
        public Employee Employee
        {
            get { return this._Employee; }
            set
            {
                if (this._Employee == value)
                    return;

                this._Employee = value;
                OnPropertyChanged("Employee");
            }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Delegates, Events, Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void Add_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Employee == null)
            {
                MessageBox.Show("Select an employee to add a task");
                return;
            }
                
            TaskEditor task = new TaskEditor();
            task.ShowDialog();

            if (task.Result == Team_Manager.DialogResult.OK)
            {
                this.Employee.Tasks.Add(task.Task);
                this.SelectedItem = task.Task;
            }
        }

        private void Edit_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task()
            {
                Description = this.SelectedItem.Description,
                Percent = this.SelectedItem.Percent,
                StartDate = this.SelectedItem.StartDate,
                EndDate = this.SelectedItem.EndDate,
            };

            TaskEditor editor = new TaskEditor(task);
            editor.ShowDialog();

            if (editor.Result == Team_Manager.DialogResult.OK)
            {
                this.SelectedItem.Description = task.Description;
                this.SelectedItem.Percent = task.Percent;
                this.SelectedItem.StartDate = task.StartDate;
                this.SelectedItem.EndDate = task.EndDate;
            }
        }

        private void Delete_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            bool deduct = this.Employee.Tasks.IndexOf(this.SelectedItem) == this.Employee.Tasks.Count - 1;
            
            this.Employee.Tasks.Remove(this.SelectedItem);

            if (deduct)
                this.SelectedIndex--;
        }

        private void Employees_Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeEditor editor = new EmployeeEditor();

            foreach (Employee employee in this.Employees)
            {
                editor.Employees.Add(employee);
            }

            editor.ShowDialog();

            if (editor.Result == Team_Manager.DialogResult.OK)
            {
                this.Employees.Clear();
                foreach (Employee employee in editor.Employees)
                {
                    this.Employees.Add(employee);
                }
            }
        }

        #endregion

    }
}