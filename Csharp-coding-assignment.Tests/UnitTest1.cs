using TimeReflector;

namespace Csharp_coding_assignment.Tests
{
    public class TimeMirrorTests
    {

        [Theory]
        [InlineData("06:35", "05:25")]
        [InlineData("11:59", "12:01")]
        [InlineData("12:02", "11:58")]
        [InlineData("12:0", "12:00")]
        [InlineData("01:00", "11:00")]
        [InlineData("12:59", "11:01")]
        public void StandardTime_ReturnExpectedValue(string input, string expected)
        {
            Assert.Equal(expected, TimeMirror.WhatIsTheTime(input));
        }

        [Fact]
        public void Collection_DuplicateTime_StandardFormat_ReturnsFilteredExpectedValues()
        {
            Assert.Equal("06:35;;08:50", TimeMirror.WhatIsTheTime("05:25;;05:25;;$:10;;05:25;;3:10"));
        }

        [Fact]
        public void Collection_Valid_SpecialTimeNames_ReturnsExpectedResult()
        {
            Assert.Equal("04:15;;04:30;;04:45;;05:00", TimeMirror.WhatIsTheTime(
                "quarter to eight;;" +
                "half past seven;;" +
                "quarter past seven;;" +
                "seven oclock;;" +
                "seven o'clock"));
        }

        [Fact]
        public void Collection_Valid_WordFormat_ReturnsExpectedResult()
        {
            Assert.Equal("05:25;;12:01;;11:58;;10:59;;06:05;;04:16;;10:10;;10:45", TimeMirror.WhatIsTheTime(
                "six thirtyfive;;" +
                "eleven fifty-nine;;" +
                "twelve two;;" +
                "one one;;" +
                "five fiftyfive;;" +
                "seven fourtyfour;;" +
                "one fifty;;" + 
                "one fifteen"));
        }

        [Fact]
        public void Collection_Valid_WithExtraWhiteSpaces_SpecialTimeNames_ReturnsExpectedResult()
        {
            Assert.Equal("04:15;;04:30;;04:45;;05:00", TimeMirror.WhatIsTheTime(
                " quarter to eight ;;" +
                "\nhalf\npast\nseven\n;;" +
                "\tquarter\tpast\tseven\t;;" +
                "     seven     oclock    ;;" +
                "seven         o'clock"));
        }

        [Fact]
        public void Collection_Valid_WithExtraWhiteSpaces_WordFormat_ReturnsExpectedResult()
        {
            Assert.Equal("05:25;;12:01;;11:58;;10:59;;06:05;;04:16;;10:10;;10:45", TimeMirror.WhatIsTheTime(
                "   six    thirty   five    ;;" +
                "\neleven\nfifty\nnine\n;;" +
                "\ttwelve\ttwo\t;;" +
                "   one    one   ;;" +
                "  five   fifty  -  five   ;;" +
                "seven\nfourtyfour;;" +
                "\none \n\nfifty\n\n\n;;" +
                " one fifteen          "));
        }

        [Fact]
        public void Collection_DuplicateTime_MixedFormats_ReturnsFilteredExpectedValues()
        {
            Assert.Equal("06:35;;08:50;;06:30", TimeMirror.WhatIsTheTime("05:25;;05:25;;five twentyfive;;$:10;;05:25;;3:10;;half past five;;5:30"));
        }

        [Fact]
        public void Valid_ExtraWhiteSpaces_StandardFormat_ReturnsExpectedResult()
        {
            Assert.Equal("12:01", TimeMirror.WhatIsTheTime(" \n   1 \t 1   : 5\n9\t  "));
        }

        [Fact]
        public void Valid_BigLetters_WordFormat_ReturnsExpectedResult()
        {
            Assert.Equal("09:00", TimeMirror.WhatIsTheTime("THREE O'CLOCK"));
        }

        [Fact]
        public void Invalid_DoubleColon_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("05::9"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_ExtraDotCharacter_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("05:09."));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_HoursTooBig_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("13:59"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MinutesTooBig_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("10:60"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_HoursTooLow_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("0:10"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_ExtraNumber_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("10:100"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_EmptyInput_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime(""));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WhiteSpaceInput1_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime(" "));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WhiteSpaceInput2_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("\n"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_ExtraSemicolons1_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("01:00;;"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_ExtraSemicolons2_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime(";;01:00"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingSemicolon_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("01:00;10:10"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_SpaceBetweenSemicolons_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("01:00; ;10:10"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingBothSemicolons_StandardFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("01:0010:10"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingSpaceAfterHour_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("fivefive"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WrongHours1_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("fiv.e fourty"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WrongMinutes1_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five fourtyfourty"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WrongMinutes2_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five fivefive"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WrongMinutes3_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five fivefourty"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WrongMinutes4_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five fourteenfour"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_WrongMinutes5_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five fourfourteen"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_ExtraMinutes_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five fourty-five-five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_DoubleDash_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five fourty--five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_DashAtWrongPlace_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five- five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingHours_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("fourtyfive"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingWhiteSpace1_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("fivefourty five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingWhiteSpace2_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("fivefourtyfive"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingMinutes_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_HoursTooBig_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("thirteen fourtyfive"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MinutesTooBig_WordFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("twelve sixty"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingWord1_SpecialNamesFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("quarter five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingWord2_SpecialNamesFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("o'clock twelve"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingWord3_SpecialNamesFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("half to five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }

        [Fact]
        public void Invalid_MissingWord4_SpecialNamesFormat_RaisesException()
        {
            var exception = Assert.Throws<FormatException>(() => TimeMirror.WhatIsTheTime("half past past five"));
            Assert.Equal("Invalid input format!", exception.Message);
        }
    }
}
