using Csharp_coding_assignment;
using System.Text;

namespace TimeReflector
{
    /// <summary>
    /// 1)
    /// Implement WhatIsTheTime method which will return clock in the mirror position.
    /// Examples
    ///     05:25 --> 06:35
    ///     01:50 --> 10:10
    ///     11:58 --> 12:02
    ///     12:01 --> 11:59
    /// Notes
    ///     Hours should be between 1 <= hour <= 12
    ///         0:20 should be 12:20
    ///         13:20 should be 01:20
    ///         
    /// 1a)
    /// Invalid input value the method will return error message instead.
    /// Examples
    ///     05:89 --> Invalid input! 
    /// 
    /// 1b)
    /// Let method accept $ symbol as 3
    /// Examples
    ///     0$:25 == 03:25
    /// 
    /// 2) 
    /// Add support for words ("one", "two", "three",...)
    /// Examples    
    ///     one twenty == 1:20
    /// 
    /// 3) 
    /// Add support for special english time names
    /// Example    
    ///     half past eight == 08:30
    ///     quarter to eight == 07:45
    /// 
    /// 4) 
    /// add support for mutiple inputs in string separated by ;; symbol. The return value will be always in the numeric form.
    /// Examples     
    ///     05:25;;one fifty;;11:58 --> 06:35;;10:10;;12:02
    /// 
    ///   
    /// 4a)
    /// If in there are two same times in the colletion the method will not include mirror values in the result 
    /// Examples
    ///     05:25;;five twentyfive --> 06:35
    /// 
    /// 
    /// Final words: 
    ///     - Dont forget to validate inputs!
    ///     - Feel free to add any additional test cases you want!
    ///     
    /// </summary>
    public static class TimeMirror
    {
        /// <summary>
        /// Public method for parsing the given input string representing analog time format.
        /// </summary>
        /// <param name="str">
        /// String containing analog time values separated by two semicolons.
        /// </param>
        /// <returns>
        /// List of analog times as objects of type <c>AnalogTime</c>.
        /// </returns>
        /// <exception cref="FormatException">
        /// Thrown if input is not in a valid format.
        /// </exception>
        private static List<AnalogTime> ParseInput(string str)
        {
            List<AnalogTime> times = new();

            foreach (var subString in str.Split(";;"))
            {
                try
                {
                    var time = AnalogTimeParser.Parse(subString);

                    if (times.Any(x => x.Hours == time.Hours && x.Minutes == time.Minutes)) 
                        continue;

                    times.Add(time);
                }
                catch (FormatException ex)
                {
                    throw ex;
                }
            }

            return times;
        }

        /// <summary>
        /// Method for mirroring given analog time.
        /// </summary>
        /// <param name="time">
        /// Analog time as object of type <c>AnalogTime</c>.
        /// </param>
        private static void MirrorTheTime(AnalogTime time)
        {
            var hours = time.Hours == 12 ? 11 : 11 - time.Hours;
            var minutes = 60 - time.Minutes;

            if (minutes == 60)
            {
                minutes = 0;
                ++hours;
            }

            time.Hours = hours;
            time.Minutes = minutes;
        }

        /// <summary>
        /// Method for converting given times in a string separated by two semicolons and transforming it to mirror analog times.
        /// </summary>
        /// <param name="timeInMirror">
        /// String containing analog times.
        /// </param>
        /// <returns>
        /// String containing mirror analog times.
        /// </returns>
        public static string WhatIsTheTime(string timeInMirror)
        {
            List<AnalogTime> times;
            try
            {
                times = ParseInput(timeInMirror);
            } catch(FormatException ex)
            {
                throw ex;
            }

            times.ForEach(s => MirrorTheTime(s));

            StringBuilder sb = new();
            sb.AppendJoin(";;", times);

            return sb.ToString();
        }
    }
}