using System;
using System.Globalization;

namespace Scheletro.FinancialYearCalculator
{
    /// <summary>
    /// <see cref="DateTime"/> クラスの拡張メソッドを提供します。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 会計年度初めの月です。
        /// </summary>
        public const int FirstMonthOfFiscalYear = 4;

        private static readonly CultureInfo _cultureInfoEnUs = new("en-us");

        /// <summary>
        /// 日付の会計年度を取得します。
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>日付の会計年度</returns>
        public static int FiscalYear(this DateTime self)
        {
            return self.Month < FirstMonthOfFiscalYear ? self.Year - 1 : self.Year;
        }

        /// <summary>
        /// 日付の会計年度の初日を取得します。
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>日付の会計年度の初日</returns>
        public static DateTime FirstDateOfFiscalYear(this DateTime self)
        {
            return new DateTime(self.FiscalYear(), FirstMonthOfFiscalYear, 1);
        }

        /// <summary>
        /// 日付の会計年度の最終日を取得します。
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>日付の会計年度の最終日</returns>
        public static DateTime LastDateOfFiscalYear(this DateTime self)
        {
            return new DateTime(self.FiscalYear() + 1, FirstMonthOfFiscalYear, 1).AddSeconds(-1);
        }

        /// <summary>
        /// 日付の四半期（第何四半期か）を取得します。
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>日付の四半期</returns>
        public static int QuarterNumber(this DateTime self)
        {
            return (self.AddMonths(1 - FirstMonthOfFiscalYear).Month + 2) / 3;
        }

        /// <summary>
        /// 日付の四半期の初日を取得します。
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>日付の四半期の初日</returns>
        public static DateTime FirstDateOfQuarter(this DateTime self)
        {
            var month = (self.QuarterNumber() - 1) * 3 + FirstMonthOfFiscalYear;
            month = month > 12 ? month - 12 : month;

            var year = self.Month < month ? self.Year - 1 : self.Year;
            return new DateTime(year, month, 1);
        }

        /// <summary>
        /// 日付の四半期の最終日を取得します。
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>日付の四半期の最終日</returns>
        public static DateTime LastDateOfQuarter(this DateTime self)
        {
            return self.AddMonths(3).FirstDateOfQuarter().AddDays(-1);
        }

        /// <summary>
        /// 日時を指定された精度で切り捨てます。
        /// </summary>
        /// <param name="self">日時</param>
        /// <param name="span">精度</param>
        /// <returns>切り捨てた日時</returns>
        public static DateTime Floor(this DateTime self, TimeSpan span)
        {
            if (span == TimeSpan.Zero)
            {
                return self;
            }
            var ticks = self.Ticks / span.Ticks;
            return new DateTime(ticks * span.Ticks, self.Kind);
        }

        /// <summary>
        /// 月の英名を返します
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>月の英名</returns>
        public static string UsMonth(this DateTime self)
        {
            return self.ToString(@"MMMM", _cultureInfoEnUs);
        }

        /// <summary>
        /// 月の英名（略称）を返します
        /// </summary>
        /// <param name="self">日付</param>
        /// <returns>月の英名（略称）</returns>
        public static string UsShortMonth(this DateTime self)
        {
            return self.ToString(@"MMM", _cultureInfoEnUs);
        }
    }
}
