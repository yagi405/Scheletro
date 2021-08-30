using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheletro.ArgumentValidator
{
    /// <summary>
    /// メソッドの引数の検証を行う汎用的なメソッドを提供するクラスです。
    /// </summary>
    public static class Args
    {
        /// <summary>
        /// 指定された引数が null でないことを検証します。
        /// </summary>
        /// <typeparam name="T">引数の型</typeparam>
        /// <param name="value">検証対象の引数</param>
        /// <param name="name">検証対象の引数名</param>
        public static void NotNull<T>(T value, string name) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// 指定された引数が null でないことを検証します。
        /// </summary>
        /// <typeparam name="T">引数の型</typeparam>
        /// <param name="value">検証対象の引数</param>
        /// <param name="name">検証対象の引数名</param>
        public static void NotNull<T>(T? value, string name) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// 指定された引数が null 又は 空文字 でないことを検証します。
        /// </summary>
        /// <param name="value">検証対象の引数</param>
        /// <param name="name">検証対象の引数名</param>
        public static void NotEmpty(string value, string name)
        {
            NotNull(value, name);

            if (value.Length == 0)
            {
                throw new ArgumentException(@"値を空にすることはできません。", name);
            }
        }

        /// <summary>
        /// 指定されたコレクションが null 又は 要素が空 でないことを検証します。
        /// </summary>
        /// <param name="collection">検証対象の引数</param>
        /// <param name="name">検証対象の引数名</param>
        public static void NotEmpty<T>(ICollection<T> collection, string name)
        {
            NotNull(collection, name);

            if (!collection.Any())
            {
                throw new ArgumentException(@"コレクションを空にすることはできません。", name);
            }
        }
    }
}
