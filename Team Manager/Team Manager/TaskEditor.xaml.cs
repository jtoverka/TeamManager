using System.ComponentModel;
using System.Windows;

namespace Team_Manager
{
    /// <summary>
    /// Interaction logic for TaskEditor.xaml
    /// </summary>
    public partial class TaskEditor : Window, INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Result generated from this dialog box
        /// </summary>
        public DialogResult Result { get; protected set; }

        private Task _Task;
        /// <summary>
        /// Task object to modify
        /// </summary>
        public Task Task
        {
            get { return this._Task; }
            set
            {
                if (this._Task == value)
                    return;

                this._Task = value;
                OnPropertyChanged("Task");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public TaskEditor()
        {
            DataContext = this;
            this.Task = new Task();
            Result = Team_Manager.DialogResult.None;
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        /// <param name="task"></param>
        public TaskEditor(Task task)
        {
            DataContext = this;
            this.Task = task;
            Result = Team_Manager.DialogResult.None;
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invokes the PropertyChanged event
        /// </summary>
        /// <param name="property"></param>
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region Delegates, Events, Handlers

        /// <summary>
        /// Event captures a property change of this component
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Cancel button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Result = Team_Manager.DialogResult.Cancel;
        }

        /// <summary>
        /// OK button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            Result = Team_Manager.DialogResult.OK;
        }

        /// <summary>
        /// Window closing, set Result property if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Result == Team_Manager.DialogResult.None)
                this.Result = Team_Manager.DialogResult.Abort;
        }

        #endregion
    }
}
