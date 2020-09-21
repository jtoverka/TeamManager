using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Security.Permissions;

namespace Team_Manager
{
    [XmlInclude(typeof(Task))]
    [Serializable]
    public struct Percent : ISerializable, IComparable, IEquatable<Percent>
    {
        private double _Value;
        public double Value 
        { 
            get { return this._Value; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Value", Value, "Value is under 0%");

                if (value > 100)
                    throw new ArgumentOutOfRangeException("Value", Value, "Value is over 100%");

                this._Value = value;
            }
        }
        public Percent(double Value)
        {
            this._Value = 0;
            this.Value = Value;
        }
        /// <summary>
        /// Initialize a new instance of this class through deserialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Percent(SerializationInfo info, StreamingContext context) : this(info.GetDouble("Value")) { }

        /// <summary>
        /// This method is called during serialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        /// <summary>
        /// Compares this instance to a specified Percent number
        /// and returns an integer that indicates whether the value of this instance is less
        /// than, equal to, or greater than the value of the specified Percent number.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (obj.GetType() != typeof(Percent))
                throw new ArgumentException("Expected argument of type Percent, '{0}' given", obj.GetType().FullName);

            Percent percent = (obj as Percent?).Value;

            return this.Value.CompareTo(percent.Value);
        }

        /// <summary>
        /// Returns a value indicating whether this instance and a specified Team_Manager.Percent represent the same value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Percent other)
        {
            return this.Value.Equals(other.Value);
        }
    }
}
