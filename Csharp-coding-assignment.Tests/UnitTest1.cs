using TimeReflector;

namespace Csharp_coding_assignment.Tests
{
    public class TimeMirrorTests
    {

        [Theory]
        [InlineData("06:35", "05:25")]
        [InlineData("11:59", "12:01")]
        [InlineData("12:02", "11:58")]
        [InlineData("12:00", "12:00")]
        public void StandardTime_ReturnExpectedValue(string input, string expected)
        {
            Assert.Equal(expected, TimeMirror.WhatIsTheTime(input));
        }

        [Fact]
        public void Collection_DuplicateTime_ReturnsFilteredExpectedValues()
        {
            Assert.Equal("06:35;;08:50", TimeMirror.WhatIsTheTime("05:25;;05:25;;$:10;;05:25;;3:10"));
        }

        [Fact]
        public void Collection_valid_returnsExpectedResult()
        {
            Assert.Equal("04:15;;04:30;;04:45;;05:00", TimeMirror.WhatIsTheTime(
                "quarter to eight;;" +
                "half past seven;;" +
                "quarter past seven;;" +
                "seven oclock;;" +
                "seven o'clock"));
        }
    }
}
