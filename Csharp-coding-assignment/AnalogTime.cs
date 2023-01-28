using System.Text;

namespace Csharp_coding_assignment
{

    /// <summary>
    /// Class <c>AnalogTime</c> represents data type that holds information about time in analog format.
    /// That means that attribute <c>Hours</c> is always in range 1 <= <c>Hours</c> <= 12.
    /// If bigger value than 12 is assigned to <c>Hours</c> attribute, method <c>CorrectAnalogFormat()</c>
    /// automatically corrects the value to analog format.
    /// </summary>
    public class AnalogTime
    {
        /// <summary>
        /// An attribute that represents 'hours' in analog time format.
        /// </summary>
        private int _hours;

        /// <summary>
        /// An attributet that represents 'minutes'.
        /// </summary>
        private int _minutes;

        /// <summary>
        /// An attribute that represents public access to the other attrribute <c>_hours</c>.
        /// When the new value is beeing assigned, input validation and analog time correction is applied.
        /// </summary>
        public int Hours
        {
            get => _hours;
            set
            {
                if (value < 0 || value > 24)
                    throw new FormatException("Invalid input format!");

                _hours = value;
                CorrectAnalogFormat();
            }
        }

        /// <summary>
        /// An attribute that represents public access to the other attribute <c>_minutes</c>.
        /// When the new value is beeing assigned, input validation is applied.
        /// </summary>
        public int Minutes
        {
            get => _minutes;
            set
            {
                if (value > 59 || value < 0)
                    throw new FormatException("Invalid input format!");

                _minutes = value;
            }
        }

        /// <summary>
        /// Method that corrects analog time format of attribute <c>_hours</c>.
        /// Value of attribute <c>_hours</c> should be always in range 1 <= <c>_hours</c> <= 12.
        /// </summary>
        private void CorrectAnalogFormat()
        {
            _hours = _hours == 0 ? 12 : _hours > 12 ? _hours - 12 : _hours;
        }

        /// <summary>
        /// Overriden method for generating string value of analog time.
        /// </summary>
        /// <returns>
        /// String value in the time format 'hh:mm'.
        /// </returns>
        override
        public string ToString()
        {
            StringBuilder sb = new();

            if (_hours < 10)
            {
                sb.Append('0');
            }
            sb.Append(_hours);

            sb.Append(':');

            if (_minutes < 10)
            {
                sb.Append('0');
            }
            sb.Append(_minutes);

            return sb.ToString();
        }
    }
}
