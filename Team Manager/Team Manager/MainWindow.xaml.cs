using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

namespace Team_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Properties

        private readonly string Filename = "\\\\emotionsvr1x\\eMotion Jobs\\Employee Tasks.xml";

        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();
        
        private int _SelectedIndex;
        /// <summary>
        /// Gets or Sets the index of employee task list
        /// </summary>
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
        /// Gets or Sets the item selected in the employee task list
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
        /// Gets or Sets the selected employee
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

        private bool _ReadWrite = true;
        /// <summary>
        /// Gets or Sets the read only file attribute
        /// </summary>
        public bool ReadWrite
        {
            get { return this._ReadWrite; }
            set
            {
                if (this._ReadWrite == value)
                    return;

                this._ReadWrite = value;
                OnPropertyChanged("ReadWrite");
            }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            if (!File.Exists(this.Filename))
                Write_File();

            FileAttributes attributes = File.GetAttributes(this.Filename);
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                this.Title = "Team Manager (read only)";
                this.ReadWrite = false;
            }
            
            Read_File();

            File.SetAttributes(this.Filename, attributes | FileAttributes.ReadOnly);
        }

        #endregion

        #region Methods

        private void Read_File()
        {
            FileAttributes attributes = File.GetAttributes(this.Filename);
            File.SetAttributes(this.Filename, File.GetAttributes(this.Filename) & ~FileAttributes.ReadOnly);
            using var file = new FileStream(this.Filename, FileMode.Open);
            File.SetAttributes(this.Filename, attributes);

            var serializer = new XmlSerializer(typeof(ObservableCollection<Employee>));
            var collection = serializer.Deserialize(file) as ObservableCollection<Employee>;

            file.Close();

            foreach (Employee item in collection)
                this.Employees.Add(item);
        }

        private void Write_File()
        {
            FileStream file;

            if (File.Exists(this.Filename))
                File.Delete(this.Filename);

            file = new FileStream(this.Filename, FileMode.Create);
            File.SetAttributes(this.Filename, File.GetAttributes(this.Filename) & ~FileAttributes.ReadOnly);
                
            try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Employee>));
                serializer.Serialize(file, this.Employees);
            }
            catch { }

            file.Close();
            file.Dispose();
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

                if (this.ReadWrite)
                {
                    File.SetAttributes(this.Filename, File.GetAttributes(this.Filename) & ~FileAttributes.ReadOnly);
                    Write_File();
                }
            }
        }

        private void Edit_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Employee == null)
            {
                MessageBox.Show("Select an employee to edit a task");
                return;
            }
            if (this.SelectedItem == null)
            {
                MessageBox.Show("Select an item to edit a task");
                return;
            }

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

                if (this.ReadWrite)
                {
                    File.SetAttributes(this.Filename, File.GetAttributes(this.Filename) & ~FileAttributes.ReadOnly);
                    Write_File();
                }
            }
        }

        private void Delete_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Employee == null)
            {
                MessageBox.Show("Select an employee to delete a task");
                return;
            }
            if (this.SelectedItem == null)
            {
                MessageBox.Show("Select an item to delete a task");
                return;
            }

            bool deduct = this.Employee.Tasks.IndexOf(this.SelectedItem) == this.Employee.Tasks.Count - 1;
            
            this.Employee.Tasks.Remove(this.SelectedItem);

            if (deduct)
                this.SelectedIndex--;

            if (this.ReadWrite)
            {
                File.SetAttributes(this.Filename, File.GetAttributes(this.Filename) & ~FileAttributes.ReadOnly);
                Write_File();
            }
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.ReadWrite)
            {
                File.SetAttributes(this.Filename, File.GetAttributes(this.Filename) & ~FileAttributes.ReadOnly);
                Write_File();
            }
        }

        #endregion
    }
}