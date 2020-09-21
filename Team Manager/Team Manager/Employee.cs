using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Team_Manager
{
    /// <summary>
    /// An employee of a company
    /// </summary>
    [Serializable]
    public class Employee
    {
        #region Properties

        /// <summary>
        /// Gets the Task collection
        /// </summary>
        [XmlElement("Tasks")]
        public ObservableCollection<Task> Tasks { get; } = new ObservableCollection<Task>();
        /// <summary>
        /// Gets the Employee Name
        /// </summary>
        [XmlElement("EmployeeName")]
        public string EmployeeName { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public Employee()
        {
            this.EmployeeName = "John Doe";
        }

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