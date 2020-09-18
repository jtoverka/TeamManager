using System.Windows;

namespace Team_Manager
{
    /// <summary>
    /// Interaction logic for TaskEditor.xaml
    /// </summary>
    public partial class TaskEditor : Window
    {
        #region Properties

        /// <summary>
        /// Gets the Result generated from this dialog box
        /// </summary>
        public DialogResult Result { get; protected set; }

        /// <summary>
        /// Gets the Task Object
        /// </summary>
        public Task Task{ get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public TaskEditor() : this (new Task()) { }

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

        #region Delegates, Events, Handlers

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Result = Team_Manager.DialogResult.Cancel;
            this.Close();
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            Result = Team_Manager.DialogResult.OK;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Result == Team_Manager.DialogResult.None)
                this.Result = Team_Manager.DialogResult.Abort;
        }

        #endregion
    }
}