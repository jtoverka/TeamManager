﻿using System;
using System.ComponentModel;

namespace Team_Manager
{
    public class Task : INotifyPropertyChanged
    {
        #region Properties

        private string _Description;
        /// <summary>
        /// Gets or Sets the task description.
        /// </summary>
        public string Description
        {
            get { return this._Description; }
            set
            {
                if (this._Description == value)
                    return;

                this._Description = value;
                OnPropertyChanged("Description");
            }
        }

        private Percent _Percent;
        /// <summary>
        /// Gets or Sets the percent completion
        /// </summary>
        public Percent Percent
        {
            get { return this._Percent; }
            set
            {
                if (this._Percent.Value == value.Value)
                    return;

                this._Percent = value;
                OnPropertyChanged("Percent");
            }
        }

        private DateTime _StartDate;
        /// <summary>
        /// Gets or Sets the start date
        /// </summary>
        public DateTime StartDate
        {
            get { return this._StartDate; }
            set
            {
                if (this._StartDate == value)
                    return;

                this._StartDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _EndDate;
        /// <summary>
        /// Gets or Sets the end date
        /// </summary>
        public DateTime EndDate
        {
            get { return this._EndDate; }
            set
            {
                if (this._EndDate == value)
                    return;

                this._EndDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public Task(string description, Percent percent, DateTime start, DateTime end)
        {
            this.Description = description;
            this.Percent = percent;
            this.StartDate = start;
            this.EndDate = end;
        }

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public Task(string description, Percent percent) : this(description, percent, DateTime.Today, DateTime.Today) { }
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public Task(string description) : this(description, new Percent(0), DateTime.Today, DateTime.Today) { }

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public Task() : this("New Task", new Percent(0), DateTime.Today, DateTime.Today) { }

        #endregion

        #region Methods

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region Delegates, Events, Handlers

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion
    }
}