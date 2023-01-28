using System.Text.RegularExpressions;

namespace Csharp_coding_assignment
{
    /// <summary>
    /// A static class <c>AnalogTimeParser</c> is able to parse analog time from the given string using public method <c>Parse</c>.
    /// Product of this class is then analog time of the type <c>AnalogTime</c>
    /// </summary>
    public static class AnalogTimeParser
    {
        /// <summary>
        /// Attribute <c>_wordNumberDict</c> is a dictionare holding information about the word representation of supported input 
        /// time numbers.
        /// Order of the values in the dictionare is important for the correct funkcionality of the program.
        /// </summary>
        private static readonly Dictionary<string, int> _wordNumberDict = new Dictionary<string, int>()
        {
            { "fifty",      50 },
            { "fourty",     40 },
            { "thirty",     30 },
            { "twenty",     20 },
            { "nineteen",   19 },
            { "eighteen",   18 },
            { "seventeen",  17 },
            { "sixteen",    16 },
            { "fifteen",    15 },
            { "fourteen",   14 },
            { "thirteen",   13 },
            { "twelve",     12 },
            { "eleven",     11 },
            { "ten",        10 },
            { "nine",       9 },
            { "eight",      8 },
            { "seven",      7 },
            { "six",        6 },
            { "five",       5 },
            { "four",       4 },
            { "three",      3 },
            { "two",        2 },
            { "one",        1 }
        };

        /// <summary>
        /// A public method <c>Parse</c> starts the whole process of parsing given string.
        /// </summary>
        /// <param name="str">
        /// The input string containing time in the analog time format.
        /// Supported analog time formats:
        /// 
        ///     -numeric values separated by colon:     
        ///         -valid inputs:
        ///         
        ///                 '11:05'
        ///                 '12:12'
        ///                 '9:9'
        ///                 ' 1 0 : 0 '
        ///     
        ///     -time expressed in words:
        ///         -hours has to be separated with minutes at least by single whitespace character
        ///         -if the representation of minutes consists of two words, they do not have to be separated by any character,
        ///         but they can by any number of whitespace characters and single dash ('-') character
        ///     
        ///         -valid inputs:
        ///         
        ///                 'ten fourteen'
        ///                 'twelve fourtyfive'
        ///                 'five fourty-five'
        ///                 'five fourty - five'
        ///                 'eleven four'
        ///                 '  nine   nine \n\t'
        ///                 
        ///         -invalid inputs:
        ///                 'fivefive'
        ///                 'five  five'
        ///                 'five fourty  five'
        ///                 'fivefourty  five'
        ///                 
        ///     -time expressed using special time words:
        ///         -valid examples:
        ///                 'twelve o'clock'
        ///                 'twelve oclock'
        ///                 'quarter to six'
        ///                 'quarter past six'
        ///                 'half past six'
        ///                 
        ///     -any number of whitespace characters are allowed at any place of input string
        ///     -parser ignores case of letters
        ///     -input cannot contain any other extra characters (except whitespace characters)
        ///     -input time has to be in analog time format => means that hours cannont be less that 1 and cannot exceed 12
        /// </param>
        /// <returns>
        /// Analog time as a object of type <c>AnalogTime<c/>.
        /// </returns>
        /// <exception cref="FormatException">
        /// Thrown when the input is not in the specified format.
        /// </exception>
        public static AnalogTime Parse(string str)
        {
            try
            {
                var result = ParseStandardTime(str);

                if (result != null)
                    return result;

                result = ParseHalfPastFormat(str);

                if (result != null)
                    return result;

                result = ParseQuarterToFormat(str);

                if (result != null)
                    return result;

                result = ParserQuarterPastFormat(str);

                if (result != null)
                    return result;

                result = ParseOclockFormat(str);

                if (result != null)
                    return result;

                result = ParseWordFormat(str);

                if (result != null)
                    return result;
            }
            catch(FormatException ex)
            {
                throw ex;
            }

            throw new FormatException("Invalid input format!");
        }

        /// <summary>
        /// Method used for parsing input in standard numeric time format 'hh:mm'.
        /// </summary>
        /// <param name="str">
        /// String containing an analog time.
        /// </param>
        /// <returns>
        /// Analog time as a object of type <c>AnalogTime<c/>.
        /// </returns>
        private static AnalogTime? ParseStandardTime(string str)
        {
            if (!Regex.Match(str, @"^\s*(1\s*[0-2]|0?\s*[1-9\$])\s*:\s*([0-5\$]?\s*[0-9\$])\s*$", RegexOptions.IgnoreCase).Success)
                return null;

            var correctedString = str.Replace('$', '3');

            return new AnalogTime()
            {
                Hours = ParseNumericHoursToInt(correctedString),
                Minutes = ParseNumericMinutesToInt(correctedString)
            };
        }

        /// <summary>
        /// Method used for converting hours from string in standard analog time format 'hh:mm' to iteger value. 
        /// </summary>
        /// <param name="str">
        /// String containing analog time in format 'hh:mm'.
        /// </param>
        /// <returns>
        /// Integer value representing hours.
        /// </returns>
        private static int ParseNumericHoursToInt(string str)
        {
            var firstDigitRegex = Regex.Match(str, @"\d(?=\s*\d\s*:)");

            int firstDigit = firstDigitRegex.Success ? int.Parse(firstDigitRegex.Value) * 10 : 0;

            var secondDigitRegex = Regex.Match(str, @"\d(?=\s*:)");

            int secondDigit = int.Parse(secondDigitRegex.Value);

            return firstDigit + secondDigit;
        }

        /// <summary>
        /// Method used for converting minutes from string in standard analog time format 'hh:mm' to iteger value. 
        /// </summary>
        /// <param name="str">
        /// String containing analog time in format 'hh:mm'.
        /// </param>
        /// <returns>
        /// Integer value representing minutes.
        /// </returns>
        private static int ParseNumericMinutesToInt(string str)
        {
            var firstDigitRegex = Regex.Match(str, @"(?<=:\s*)\d");

            int firstDigit = int.Parse(firstDigitRegex.Value);

            var secondDigitRegex = Regex.Match(str, @"(?<=:\s*\d\s*)\d");

            if (secondDigitRegex.Success)
                return firstDigit * 10 + int.Parse(secondDigitRegex.Value);

            return firstDigit;
        }

        /// <summary>
        /// Method used for parsing input in a word format with special time name 'half past ' + number.
        /// </summary>
        /// <param name="str">
        /// String containing an analog time.
        /// </param>
        /// <returns>
        /// Analog time as a object of type <c>AnalogTime<c/>.
        /// </returns>
        private static AnalogTime? ParseHalfPastFormat(string str)
        {
            var rgx = Regex.Match(str, @"(?<=^\s*half\s+past\s+)[a-z]+(?=\s*$)", RegexOptions.IgnoreCase);

            if (!rgx.Success)
                return null;

            try
            {
                return new AnalogTime() { Hours = ParseWordHoursToInt(rgx.Value), Minutes = 30 };
            }
            catch (FormatException ex) { throw ex; }
        }

        /// <summary>
        /// Method used for parsing input in a word format with special time name 'quarter to  ' + number.
        /// </summary>
        /// <param name="str">
        /// String containing an analog time.
        /// </param>
        /// <returns>
        /// Analog time as a object of type <c>AnalogTime<c/>.
        /// </returns>
        private static AnalogTime? ParseQuarterToFormat(string str)
        {
            var rgx = Regex.Match(str, @"(?<=^\s*quarter\s+to\s+)[a-z]+(?=\s*$)", RegexOptions.IgnoreCase);

            if (!rgx.Success)
                return null;

            try
            {
                return new AnalogTime() { Hours = ParseWordHoursToInt(rgx.Value) - 1, Minutes = 45 };
            }
            catch (FormatException ex) { throw ex; }
        }

        /// <summary>
        /// Method used for parsing input in a word format with special time name 'quarter past  ' + number.
        /// </summary>
        /// <param name="str">
        /// String containing an analog time.
        /// </param>
        /// <returns>
        /// Analog time as a object of type <c>AnalogTime<c/>.
        /// </returns>
        private static AnalogTime? ParserQuarterPastFormat(string str)
        {
            var rgx = Regex.Match(str, @"(?<=^\s*quarter\s+past\s+)[a-z]+(?=\s*$)", RegexOptions.IgnoreCase);

            if (!rgx.Success)
                return null;

            try
            {
                return new AnalogTime() { Hours = ParseWordHoursToInt(rgx.Value), Minutes = 15 };
            }
            catch (FormatException ex) { throw ex; }
        }

        /// <summary>
        /// Method used for parsing input in a word format with special time name number + 'o'clock' (apostroph is optional).
        /// </summary>
        /// <param name="str">
        /// String containing an analog time.
        /// </param>
        /// <returns>
        /// Analog time as a object of type <c>AnalogTime<c/>.
        /// </returns>
        private static AnalogTime? ParseOclockFormat(string str)
        {
            var rgx = Regex.Match(str, @"(?<=^\s*)[a-z]+(?=\s+o'?clock\s*$)", RegexOptions.IgnoreCase);

            if (!rgx.Success)
                return null;

            try
            {
                return new AnalogTime() { Hours = ParseWordHoursToInt(rgx.Value), Minutes = 0 };
            }
            catch (FormatException ex) { throw ex; }
        }

        /// <summary>
        /// Method used for parsing input in a standard word format.
        /// </summary>
        /// <param name="str">
        /// String containing an analog time.
        /// </param>
        /// <returns>
        /// Analog time as a object of type <c>AnalogTime<c/>.
        /// </returns>
        private static AnalogTime? ParseWordFormat(string str)
        {
            if (!Regex.Match(str, @"^\s*[a-z]+\s+[a-z]+\s*-?\s*[a-z]+\s*$", RegexOptions.IgnoreCase).Success)
                return null;

            var hours = Regex.Match(str, @"[a-z]+(?=\s+[a-z]+\s*-?\s*[a-z]+\s*)", RegexOptions.IgnoreCase).Value;
            var minutes = Regex.Match(str, @"(?<=\s*[a-z]+\s+)[a-z]+\s*-?\s*[a-z]+", RegexOptions.IgnoreCase).Value;

            try
            {
                return new AnalogTime() { Hours = ParseWordHoursToInt(hours), Minutes = ParseWordMinutesToInt(minutes) };
            }
            catch (FormatException ex) { throw ex; }
        }

        /// <summary>
        /// Method used for converting hours in word representation to integer.
        /// </summary>
        /// <param name="str">
        /// String containing word representation of number.
        /// </param>
        /// <returns>
        /// Integer value.
        /// </returns>
        /// <exception cref="FormatException">
        /// Thrown if word number in string is not in valid format.
        /// </exception>
        private static int ParseWordHoursToInt(string str)
        {
            str = str.ToLower();

            foreach (var item in _wordNumberDict)
            {
                if (str == item.Key)
                {
                    // checking if hours value exceeds 12-hour analog format
                    if (item.Value > 12) break;

                    return item.Value;
                }
            }

            throw new FormatException("Invalid input format!");
        }

        /// <summary>
        /// Method used for converting minutes in a word representation to integer value.
        /// </summary>
        /// <param name="str">
        /// String containing word representation of number.
        /// </param>
        /// <returns>
        /// Integer value.
        /// </returns>
        /// <exception cref="FormatException">
        /// Thrown if word number in string is not in valid format.
        /// </exception>
        private static int ParseWordMinutesToInt(string str)
        {
            str = str.ToLower();
            var remainingStr = str;
                                   
            List<KeyValuePair<string, int>> values = new();

            foreach (var item in _wordNumberDict)
            {
                if (remainingStr.Contains(item.Key))
                {
                    values.Add(item);
                    remainingStr = remainingStr.Replace(item.Key, "");
                }
            }

            // if minutes consists only of single word-number
            if (values.Count == 1 && values.First().Key == str)
            {
                return values.First().Value;
            }

            // if minutes consists of two word-numbers (more than two is not correct)
            if (values.Count == 2)
            {   
                // checking if minutes word-numbers were in the correct order, e.g. five-twenty is not correct, but twenty-five is ok
                var sortedValues = values.OrderByDescending(x => x.Value);
                var bigger = sortedValues.ElementAt(0);
                var smaller = sortedValues.ElementAt(1);

                if (bigger.Value >= 20 && smaller.Value <= 9 && bigger.Key + remainingStr + smaller.Key == str)
                {
                    return bigger.Value + smaller.Value;
                }
            }
            
            throw new FormatException("Invalid input format!");
        }
    }
}
