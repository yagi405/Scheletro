using System;
using System.Globalization;
using System.Linq;
using Scheletro.Utils;
using Xunit;

namespace Scheletro.Tests.Utils
{
    /// <summary>
    /// <see cref="DateTimeExtensions"/> のテストクラスです。
    /// </summary>
    public class DateTimeExtensionsTests
    {
        /// <summary>
        /// 日付の会計年度が取得できることをテストします。
        /// </summary>
        [Fact]
        public void FiscalYear_ShouldReturnFiscalYear()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var fiscalYear = date.FiscalYear();
                Assert.Equal(date.Month < DateTimeExtensions.FirstMonthOfFiscalYear ? date.Year - 1 : date.Year,
                    fiscalYear);
            }
        }

        /// <summary>
        /// 日付の会計年度の初日が取得できることをテストします。
        /// </summary>
        [Fact]
        public void FirstDateOfFiscalYear_ShouldReturnFirstDateOfFiscalYear()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var firstDateOfFiscalYear = date.FirstDateOfFiscalYear();
                var expected = new DateTime(
                    date.Month < DateTimeExtensions.FirstMonthOfFiscalYear ? date.Year - 1 : date.Year,
                    DateTimeExtensions.FirstMonthOfFiscalYear,
                    1);
                Assert.Equal(expected, firstDateOfFiscalYear);
            }
        }

        /// <summary>
        /// 日付の会計年度の最終日が取得できることをテストします。
        /// </summary>
        [Fact]
        public void LastDateOfFiscalYear_ShouldReturnLastDateOfFiscalYear()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var lastDateOfFiscalYear = date.LastDateOfFiscalYear();
                var expected = new DateTime(
                        (date.Month < DateTimeExtensions.FirstMonthOfFiscalYear ? date.Year - 1 : date.Year) + 1,
                        DateTimeExtensions.FirstMonthOfFiscalYear,
                        1)
                    .AddSeconds(-1);
                Assert.Equal(expected, lastDateOfFiscalYear);
            }
        }

        /// <summary>
        /// 日付の四半期（第何四半期か）が取得できることをテストします。
        /// </summary>
        [Fact]
        public void QuarterNumber_ShouldReturnQuarterNumber()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var month = DateTimeExtensions.FirstMonthOfFiscalYear;
                var quarterNumber = date.QuarterNumber();
                if (
                    date.Month == month ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    Assert.Equal(1, quarterNumber);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    Assert.Equal(2, quarterNumber);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    Assert.Equal(3, quarterNumber);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month - 12 : month)
                )
                {
                    Assert.Equal(4, quarterNumber);
                }
            }
        }

        /// <summary>
        /// 日付の四半期の初日が取得できることをテストします。
        /// </summary>
        [Fact]
        public void FirstDateOfQuarter_ShouldReturnFirstDateOfQuarter()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var firstDateOfQuarter = date.FirstDateOfQuarter();
                var firstMonthOfFiscalYear = DateTimeExtensions.FirstMonthOfFiscalYear;
                var month = DateTimeExtensions.FirstMonthOfFiscalYear;
                if (
                    date.Month == month ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedYear = date.Month < firstMonthOfFiscalYear ? date.Year - 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, firstMonthOfFiscalYear, 1), firstDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFiscalYear += 3) > 12
                        ? firstMonthOfFiscalYear - 12
                        : firstMonthOfFiscalYear;
                    var expectedYear = date.Month < expectedMonth ? date.Year - 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1), firstDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFiscalYear += 6) > 12
                        ? firstMonthOfFiscalYear - 12
                        : firstMonthOfFiscalYear;
                    var expectedYear = date.Month < expectedMonth ? date.Year - 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1), firstDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month - 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFiscalYear += 9) > 12
                        ? firstMonthOfFiscalYear - 12
                        : firstMonthOfFiscalYear;
                    var expectedYear = date.Month < expectedMonth ? date.Year - 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1), firstDateOfQuarter);
                }
            }
        }

        /// <summary>
        /// 日付の四半期の最終日が取得できることをテストします。
        /// </summary>
        [Fact]
        public void LastDateOfQuarter_ShouldReturnLastDateOfQuarter()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var lastDateOfQuarter = date.LastDateOfQuarter();
                var firstMonthOfFiscalYear = DateTimeExtensions.FirstMonthOfFiscalYear;
                var month = DateTimeExtensions.FirstMonthOfFiscalYear;
                if (
                    date.Month == month ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFiscalYear += 3) > 12
                        ? firstMonthOfFiscalYear - 12
                        : firstMonthOfFiscalYear;
                    var expectedYear = expectedMonth < date.Month ? date.Year + 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1).AddDays(-1), lastDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFiscalYear += 6) > 12
                        ? firstMonthOfFiscalYear - 12
                        : firstMonthOfFiscalYear;
                    var expectedYear = expectedMonth < date.Month ? date.Year + 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1).AddDays(-1), lastDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFiscalYear += 9) > 12
                        ? firstMonthOfFiscalYear - 12
                        : firstMonthOfFiscalYear;
                    var expectedYear = expectedMonth < date.Month ? date.Year + 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1).AddDays(-1), lastDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month - 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFiscalYear += 12) > 12
                        ? firstMonthOfFiscalYear - 12
                        : firstMonthOfFiscalYear;
                    var expectedYear = expectedMonth < date.Month ? date.Year + 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1).AddDays(-1), lastDateOfQuarter);
                }
            }
        }

        /// <summary>
        /// 日時が指定精度で切り捨てられることをテストします。
        /// </summary>
        [Fact]
        public void Floor_ShouldReturnFlooredDateTime()
        {
            var now = DateTime.Now;
            Assert.Equal(now, now.Floor(TimeSpan.Zero));
            Assert.Equal(
                new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second),
                now.Floor(TimeSpan.FromSeconds(1))
            );
            Assert.Equal(
                new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0),
                now.Floor(TimeSpan.FromMinutes(1))
            );
            Assert.Equal(
                new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0),
                now.Floor(TimeSpan.FromHours(1))
            );
        }

        /// <summary>
        /// 月の英名を返すことをテストします。
        /// </summary>
        [Fact]
        public void UsMonth_ShouldReturnUsMonth()
        {
            var cultureInfoEnUs = new CultureInfo("en-us");

            var startDate = new DateTime(2021, 1, 1);
            foreach (var date in Enumerable.Range(0, 12).Select(x => startDate.AddMonths(x)))
            {
                Assert.Equal(date.ToString(@"MMMM", cultureInfoEnUs), date.UsMonth());
            }
        }

        /// <summary>
        /// 月の英名（略称）を返すことをテストします。
        /// </summary>
        [Fact]
        public void UsShortMonth_ShouldReturnUsShortMonth()
        {
            var cultureInfoEnUs = new CultureInfo("en-us");

            var startDate = new DateTime(2021, 1, 1);
            foreach (var date in Enumerable.Range(0, 12).Select(x => startDate.AddMonths(x)))
            {
                Assert.Equal(date.ToString(@"MMM", cultureInfoEnUs), date.UsShortMonth());
            }
        }
    }
}