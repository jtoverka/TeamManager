using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Team_Manager
{
    /// <summary>
    /// Interaction logic for EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window, INotifyPropertyChanged
    {
        #region Properties

        private DialogResult _Result;
        /// <summary>
        /// Gets or Sets the Dialog Result of this window
        /// </summary>
        public DialogResult Result
        {
            get { return this._Result; }
            set
            {
                if (this._Result == value)
                    return;

                this._Result = value;
                OnPropertyChanged("Result");
            }
        }

        /// <summary>
        /// Gets the Employees Collection
        /// </summary>
        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();

        private Employee _Employee;
        /// <summary>
        /// Gets or Sets the Selected Employee in Employees Collection
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

        private string _EmployeeName = "";
        /// <summary>
        /// Gets or Sets the Employee Name to be made
        /// </summary>
        public string EmployeeName
        {
            get { return this._EmployeeName; }
            set
            {
                if (this._EmployeeName == value)
                    return;

                this._EmployeeName = value;
                OnPropertyChanged("EmployeeName");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public EmployeeEditor()
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

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Result = Team_Manager.DialogResult.OK;
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Result = Team_Manager.DialogResult.Cancel;
            this.Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            namebox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (this.EmployeeName == "")
                return;

            foreach (Employee employee in this.Employees)
            {
                if (employee.EmployeeName == this.EmployeeName)
                    return;
            }

            this.Employees.Add(new Employee(this.EmployeeName));

            this.EmployeeName = "";
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Employees.Remove(this.Employee);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.Result == Team_Manager.DialogResult.None)
                this.Result = Team_Manager.DialogResult.Abort;
        }

        private void Up_Button_Click(object sender, RoutedEventArgs e)
        {
            IList selection = Box.SelectedItems;
            ObservableCollection<Employee> collection = this.Employees;

            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0; i < selection.Count; i++)
                oldIndices[i] = collection.IndexOf(selection[i] as Employee);

            Array.Sort(oldIndices);

            for (int i = 0; i < selection.Count; i++)
            {
                if (oldIndices[i] > i)
                    newIndices[i] = oldIndices[i] - 1;
                else
                    newIndices[i] = oldIndices[i];
            }
            Array.Sort(newIndices, oldIndices);

            for (int i = 0; i < selection.Count; i++)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }
        private void Down_Button_Click(object sender, RoutedEventArgs e)
        {
            IList selection = Box.SelectedItems;
            ObservableCollection<Employee> collection = this.Employees;

            int lastBoxIndex = collection.Count - 1;

            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0; i < selection.Count; i++)
                oldIndices[i] = collection.IndexOf(selection[i] as Employee);

            Array.Sort(oldIndices);

            for (int i = 0, j = selection.Count - 1; i < selection.Count; i++, j--)
            {
                if (oldIndices[i] < lastBoxIndex - j)
                    newIndices[i] = oldIndices[i] + 1;
                else
                    newIndices[i] = oldIndices[i];
            }

            Array.Sort(newIndices, oldIndices);

            for (int i = selection.Count - 1; i >= 0; i--)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }
        private void Top_Button_Click(object sender, RoutedEventArgs e)
        {
            IList selection = Box.SelectedItems;
            ObservableCollection<Employee> collection = this.Employees;

            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0; i < selection.Count; i++)
            {
                oldIndices[i] = collection.IndexOf(selection[i] as Employee);
                newIndices[i] = i;
            }
            Array.Sort(oldIndices);
            Array.Sort(newIndices, oldIndices);

            for (int i = 0; i < selection.Count; i++)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }
        private void Bottom_Button_Click(object sender, RoutedEventArgs e)
        {
            IList selection = Box.SelectedItems;
            ObservableCollection<Employee> collection = this.Employees;

            int lastBoxIndex = collection.Count - 1;

            int[] oldIndices = new int[selection.Count];
            int[] newIndices = new int[selection.Count];

            for (int i = 0, j = lastBoxIndex;
                i < selection.Count;
                i++, j--)
            {
                oldIndices[i] = collection.IndexOf(selection[i] as Employee);
                newIndices[i] = j;
            }
            Array.Sort(oldIndices);
            Array.Sort(newIndices);

            for (int i = selection.Count - 1; i >= 0; i--)
            {
                if (oldIndices[i] != newIndices[i])
                    collection.Move(oldIndices[i], newIndices[i]);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CharRange range1 = new CharRange()
            {
                Min = "a",
                Max = "z",
            };
            CharRange range2 = new CharRange()
            {
                Min = "A",
                Max = "Z",
            };
            foreach (Char character in e.Text)
            {
                if (!(range1.IsValid(character) || range2.IsValid(character)))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Add_Button_Click(null, null);
                e.Handled = true;
            }
        }

        #endregion

    }
}