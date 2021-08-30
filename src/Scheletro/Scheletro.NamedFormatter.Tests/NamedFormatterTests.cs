using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Scheletro.NamedFormatter.Tests
{
    /// <summary>
    /// <see cref="NamedFormatter"/> のテストクラスです。
    /// </summary>
    public class NamedFormatterTests
    {
        /// <summary>
        /// 指定された文字列が null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void IsMatch_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new NamedFormatter().IsMatch(null)
            );
        }

        /// <summary>
        /// 指定された文字列が、現在の正規表現パターンに合致する場合に true を返すことをテストします。
        /// </summary>
        [Fact]
        public void IsMatch_ShouldReturnTrue()
        {
            var formatter = new NamedFormatter();
            Assert.True(formatter.IsMatch("XYZ_${literal}_xyz"));
        }

        /// <summary>
        /// 指定された文字列が、現在の正規表現パターンに合致しない場合に false を返すことをテストします。
        /// </summary>
        [Fact]
        public void IsMatch_ShouldReturnFalse()
        {
            var formatter = new NamedFormatter();
            Assert.False(formatter.IsMatch("FooBarBaz"));
        }

        /// <summary>
        /// 正規表現パターンが null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void NamedFormatter_ShouldThrowArgumentNullException_WhenPatternIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new NamedFormatter((string)null)
            );
        }

        /// <summary>
        /// 正規表現パターンに name グループが含まれない場合に <see cref="ArgumentException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void NamedFormatter_ShouldThrowArgumentException_WhenPatternDoesNotContainNameGroup()
        {
            Assert.Throws<ArgumentException>(
                () => new NamedFormatter(@"^\d{3}-\d{4}$")
            );
        }

        /// <summary>
        /// キーワードが null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void Add_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new NamedFormatter().Add(null, DateTime.Now)
            );
        }

        /// <summary>
        /// キーワードが 空文字 又は 空白文字 の場合に <see cref="ArgumentException"/> が発生することをテストします。
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("　")]
        public void Add_ShouldThrowArgumentException_WhenNameIsEmptyOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentException>(
                () => new NamedFormatter().Add(name, DateTime.Now)
            );
        }

        /// <summary>
        /// 値を返すデリゲートが null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void Add_ShouldThrowArgumentNullException_WhenValueFactoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new NamedFormatter().Add("name", (Func<object>)null)
            );
        }

        /// <summary>
        /// Converter を追加することをテストします。
        /// </summary>
        [Fact]
        public void Add_ShouldAddConverter()
        {
            var i = 0;
            var formatter = new NamedFormatter
            {
                { "literal", "abc" },
                { "date", DateTime.Now },
                { "type", () => nameof(NamedFormatterTests) },
                { "number", () => ++i },
                { "us_date", format => DateTime.Now.ToString(format, CultureInfo.GetCultureInfo("en-US")) }
            };

            Assert.Equal(5, formatter.Count());
        }

        /// <summary>
        /// 指定された文字列が null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void Format_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new NamedFormatter().Format(null)
            );
        }

        /// <summary>
        /// 指定された文字列に含まれるキーワードを、関連付けられた値に置換することをテストします。
        /// </summary>
        [Fact]
        public void Format_ShouldFormat()
        {
            var i = 0;
            var now = DateTime.Now;
            var formatter = new NamedFormatter
            {
                { "literal", "abc" },
                { "date", now },
                { "type", () => nameof(NamedFormatterTests) },
                { "number", () => ++i },
                { "us_date", format => now.ToString(format, CultureInfo.GetCultureInfo("en-US")) }
            };

            var usDate = now.ToString("MMM-dd-yyyy", CultureInfo.GetCultureInfo("en-US"));
            Assert.Equal($"XYZ_abc_{now.ToString(CultureInfo.InvariantCulture)}_{now:yyyyMMdd}_{nameof(NamedFormatterTests)}_1_002_{usDate}_xyz",
                formatter.Format(@"XYZ_${literal}_${date}_${date:yyyyMMdd}_${type}_${number}_${number:000}_${us_date:MMM-dd-yyyy}_xyz"));
        }
    }
}
