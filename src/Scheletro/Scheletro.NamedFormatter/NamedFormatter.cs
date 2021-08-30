using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Scheletro.NamedFormatter
{
    /// <summary>
    /// ユーザー定義の複合書式指定機能を提供します。
    /// </summary>
    public class NamedFormatter : IEnumerable<string>
    {
        private static readonly Regex _defaultPattern = new(@"\${\s*(?<name>\w+?)\s*(:(?<format>.*?))?}", RegexOptions.Compiled);

        private readonly Regex _pattern;

        private readonly Dictionary<string, Func<string, string>> _converters;

        /// <summary>
        /// 指定された文字列が、現在の正規表現パターンに合致するかを判定します。
        /// </summary>
        /// <param name="input">指定文字列</param>
        /// <returns>現在の正規表現パターンに合致する場合は true、それ以外は false</returns>
        public bool IsMatch(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            return _pattern.IsMatch(input);
        }

        /// <summary>
        /// 既定の正規表現パターンを使用して、 <see cref="NamedFormatter"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public NamedFormatter()
            : this(_defaultPattern)
        {
        }

        /// <summary>
        /// 指定した正規表現パターンと指定した等価比較を使用して、 <see cref="NamedFormatter"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="pattern">正規表現パターン</param>
        /// <param name="comparer">等価比較の実装</param>
        public NamedFormatter(string pattern, IEqualityComparer<string> comparer = null)
            : this(new Regex(pattern), comparer)
        {
        }

        /// <summary>
        /// 指定した正規表現と指定した等価比較を使用して、 <see cref="NamedFormatter"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="pattern">正規表現</param>
        /// <param name="comparer">等価比較の実装</param>
        public NamedFormatter(Regex pattern, IEqualityComparer<string> comparer = null)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (!pattern.GetGroupNames().Contains("name"))
            {
                throw new ArgumentException(@"キャプチャグループ名 name が定義されている必要があります", nameof(pattern));
            }

            _pattern = pattern;
            _converters = new Dictionary<string, Func<string, string>>(comparer);
        }

        /// <summary>
        /// 指定したキーワードと値（指定したオブジェクトの文字列表記）を関連付けます。
        /// </summary>
        /// <param name="name">キーワード</param>
        /// <param name="value">値</param>
        /// <returns>現在のインスタンス</returns>
        public NamedFormatter Add(string name, object value) => Add(name, _ => value?.ToString());

        /// <summary>
        /// 指定したキーワードと値（指定したオブジェクトの文字列表記）を関連付けます。
        /// </summary>
        /// <param name="name">キーワード</param>
        /// <param name="value">値</param>
        /// <returns>現在のインスタンス</returns>
        public NamedFormatter Add(string name, IFormattable value) => Add(name, format => value?.ToString(format, CultureInfo.InvariantCulture));

        /// <summary>
        /// 指定したキーワードと値を返すデリゲートを関連付けます。
        /// </summary>
        /// <param name="name">キーワード</param>
        /// <param name="valueFactory">値を返すデリゲート</param>
        /// <returns>現在のインスタンス</returns>
        public NamedFormatter Add(string name, Func<object> valueFactory)
        {
            if (valueFactory == null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            return Add(name, _ => valueFactory()?.ToString());
        }

        /// <summary>
        /// 指定したキーワードと値を返すデリゲートを関連付けます。
        /// </summary>
        /// <param name="name">キーワード</param>
        /// <param name="valueFactory">値を返すデリゲート</param>
        /// <returns>現在のインスタンス</returns>
        public NamedFormatter Add(string name, Func<IFormattable> valueFactory)
        {
            if (valueFactory == null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            return Add(name, format => valueFactory()?.ToString(format, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 指定したキーワードと値を返すデリゲートを関連付けます。
        /// </summary>
        /// <param name="name">キーワード</param>
        /// <param name="valueFactory">値を返すデリゲート</param>
        /// <returns>現在のインスタンス</returns>
        public NamedFormatter Add(string name, Func<string, string> valueFactory)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = name.Trim();

            if (name.Length == 0)
            {
                throw new ArgumentException(nameof(name));
            }

            if (valueFactory == null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            _converters.Add(name, valueFactory);

            return this;
        }

        /// <summary>
        /// 指定された文字列に含まれるキーワードを、関連付けられた値に置換します。
        /// </summary>
        /// <param name="input">指定文字列</param>
        /// <returns>指定された文字列に含まれるキーワードが、関連付けられた値に置換された文字列</returns>
        public string Format(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return _pattern.Replace(input, m =>
            {
                var name = m.Groups["name"].Value;
                if (_converters.TryGetValue(name, out var converter))
                {
                    return converter(m.Groups["format"].Value) ?? "";
                }
                return m.Value;
            });
        }

        /// <inheritdoc />
        IEnumerator<string> IEnumerable<string>.GetEnumerator() => _converters.Keys.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _converters.Keys.GetEnumerator();
    }
}