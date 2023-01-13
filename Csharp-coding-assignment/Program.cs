
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
        public static void Main()
        {
            Console.WriteLine("Hello!");
        }

        public static string WhatIsTheTime(string timeInMirror)
        {
            return string.Empty;
        }
    }
}