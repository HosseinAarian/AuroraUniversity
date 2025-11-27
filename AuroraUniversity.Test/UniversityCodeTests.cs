using System;
using Xunit;

namespace AuroraUniversity.Tests
{
    public class UniversityCodeTests
    {
        private static int ComputeChecksumForBase(string baseWithoutChecksum)
        {
            var concatenated = baseWithoutChecksum.Replace("-", "");
            var substring = concatenated.Substring(2); // skip "AU"
            int sum = 0;
            foreach (char c in substring)
            {
                if (char.IsDigit(c)) sum += c - '0';
                else if (char.IsLetter(c)) sum += c - 'A' + 10;
                else throw new Exception($"Unexpected char {c}");
            }
            return sum % 97;
        }

        [Fact]
        public void Parse_ValidCode_ReturnsExpected()
        {
            var type = "STU";
            var year = 2023;
            var term = 1;
            var code = "ABC";
            var serial = 1;
            var baseStr = $"AU-{type}-{year}{term}-{code}-{serial:D4}";
            var checksum = ComputeChecksumForBase(baseStr);
            var full = $"{baseStr}-{checksum:D2}";

            var parsed = UniversityCode.Parse(full);

            Assert.Equal(type, parsed.Type);
            Assert.Equal(year, parsed.Year);
            Assert.Equal(term, parsed.Term);
            Assert.Equal(code, parsed.Code);
            Assert.Equal(serial, parsed.Serial);
            Assert.Equal(checksum, parsed.Checksum);
            Assert.Equal(full, parsed.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Parse_EmptyOrWhitespace_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => UniversityCode.Parse(input));
        }

        [Fact]
        public void Parse_InvalidFormat_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => UniversityCode.Parse("not-a-code"));
        }

        [Fact]
        public void Parse_YearOutOfRange_ThrowsFormatException()
        {
            var baseStr = $"AU-STU-18001-ABC-0001";
            var checksum = ComputeChecksumForBase(baseStr);
            var full = $"{baseStr}-{checksum:D2}";
            Assert.Throws<FormatException>(() => UniversityCode.Parse(full));
        }

        [Fact]
        public void Parse_TermInvalid_ThrowsFormatException()
        {
            // Term 4 will not match the regex [1-3]
            Assert.Throws<FormatException>(() => UniversityCode.Parse("AU-STU-20214-ABC-0001-00"));
        }

        [Fact]
        public void Parse_CodeLengthInvalid_ThrowsFormatException()
        {
            // Code length 2 is invalid (must be 3-6)
            Assert.Throws<FormatException>(() => UniversityCode.Parse("AU-STU-20211-AB-0001-00"));
        }

        [Fact]
        public void Parse_ChecksumMismatch_ThrowsFormatException()
        {
            var baseStr = $"AU-STU-20231-ABC-0001";
            var checksum = ComputeChecksumForBase(baseStr);
            var wrong = (checksum + 1) % 97;
            var fullWrong = $"{baseStr}-{wrong:D2}";
            Assert.Throws<FormatException>(() => UniversityCode.Parse(fullWrong));
        }
    }
}