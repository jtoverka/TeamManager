using System.Collections.ObjectModel;

namespace Team_Manager
{
    public class Employee
    {
        #region Properties

        /// <summary>
        /// Gets the Task collection
        /// </summary>
        public ObservableCollection<Task> Tasks { get; } = new ObservableCollection<Task>();
        /// <summary>
        /// Gets the Employee Name
        /// </summary>
        public string EmployeeName { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        /// <param name="Name"></param>
        public Employee(string Name)
        {
            this.EmployeeName = Name;
        }

        #endregion

        #region Overrides

        public new string ToString()
        {
            return this.EmployeeName;
        }

        #endregion
    }
}