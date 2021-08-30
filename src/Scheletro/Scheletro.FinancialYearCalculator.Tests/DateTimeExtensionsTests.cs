using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Scheletro.FinancialYearCalculator.Tests
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
        public void FinancialYear_ShouldReturnFinancialYear()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var financialYear = date.FinancialYear();
                Assert.Equal(date.Month < DateTimeExtensions.FirstMonthOfFinancialYear ? date.Year - 1 : date.Year,
                    financialYear);
            }
        }

        /// <summary>
        /// 日付の会計年度の初日が取得できることをテストします。
        /// </summary>
        [Fact]
        public void FirstDateOfFinancialYear_ShouldReturnFirstDateOfFinancialYear()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var firstDateOfFinancialYear = date.FirstDateOfFinancialYear();
                var expected = new DateTime(
                    date.Month < DateTimeExtensions.FirstMonthOfFinancialYear ? date.Year - 1 : date.Year,
                    DateTimeExtensions.FirstMonthOfFinancialYear,
                    1);
                Assert.Equal(expected, firstDateOfFinancialYear);
            }
        }

        /// <summary>
        /// 日付の会計年度の最終日が取得できることをテストします。
        /// </summary>
        [Fact]
        public void LastDateOfFinancialYear_ShouldReturnLastDateOfFinancialYear()
        {
            var startDate = DateTime.Today.AddDays(-10000);
            foreach (var date in Enumerable.Range(0, 20000).Select(x => startDate.AddDays(x)))
            {
                var lastDateOfFinancialYear = date.LastDateOfFinancialYear();
                var expected = new DateTime(
                        (date.Month < DateTimeExtensions.FirstMonthOfFinancialYear ? date.Year - 1 : date.Year) + 1,
                        DateTimeExtensions.FirstMonthOfFinancialYear,
                        1)
                    .AddSeconds(-1);
                Assert.Equal(expected, lastDateOfFinancialYear);
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
                var month = DateTimeExtensions.FirstMonthOfFinancialYear;
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
                var firstMonthOfFinancialYear = DateTimeExtensions.FirstMonthOfFinancialYear;
                var month = DateTimeExtensions.FirstMonthOfFinancialYear;
                if (
                    date.Month == month ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedYear = date.Month < firstMonthOfFinancialYear ? date.Year - 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, firstMonthOfFinancialYear, 1), firstDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFinancialYear += 3) > 12
                        ? firstMonthOfFinancialYear - 12
                        : firstMonthOfFinancialYear;
                    var expectedYear = date.Month < expectedMonth ? date.Year - 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1), firstDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFinancialYear += 6) > 12
                        ? firstMonthOfFinancialYear - 12
                        : firstMonthOfFinancialYear;
                    var expectedYear = date.Month < expectedMonth ? date.Year - 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1), firstDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month - 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFinancialYear += 9) > 12
                        ? firstMonthOfFinancialYear - 12
                        : firstMonthOfFinancialYear;
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
                var firstMonthOfFinancialYear = DateTimeExtensions.FirstMonthOfFinancialYear;
                var month = DateTimeExtensions.FirstMonthOfFinancialYear;
                if (
                    date.Month == month ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFinancialYear += 3) > 12
                        ? firstMonthOfFinancialYear - 12
                        : firstMonthOfFinancialYear;
                    var expectedYear = expectedMonth < date.Month ? date.Year + 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1).AddDays(-1), lastDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFinancialYear += 6) > 12
                        ? firstMonthOfFinancialYear - 12
                        : firstMonthOfFinancialYear;
                    var expectedYear = expectedMonth < date.Month ? date.Year + 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1).AddDays(-1), lastDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFinancialYear += 9) > 12
                        ? firstMonthOfFinancialYear - 12
                        : firstMonthOfFinancialYear;
                    var expectedYear = expectedMonth < date.Month ? date.Year + 1 : date.Year;
                    Assert.Equal(new DateTime(expectedYear, expectedMonth, 1).AddDays(-1), lastDateOfQuarter);
                }
                else if (
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month -= 12 : month) ||
                    date.Month == (++month > 12 ? month - 12 : month)
                )
                {
                    var expectedMonth = (firstMonthOfFinancialYear += 12) > 12
                        ? firstMonthOfFinancialYear - 12
                        : firstMonthOfFinancialYear;
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