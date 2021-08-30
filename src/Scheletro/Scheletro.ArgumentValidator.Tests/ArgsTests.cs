using System;
using System.Collections.Generic;
using Xunit;

namespace Scheletro.ArgumentValidator.Tests
{
    /// <summary>
    /// <see cref="Args"/> のテストクラスです。
    /// </summary>
    public class ArgsTests
    {
        /// <summary>
        /// 引数が null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        /// <param name="arg">検証対象の引数</param>
        [Theory]
        [InlineData(null)]
        public static void NotNull_ShouldThrowArgumentNullException_WhenClassArgumentIsNull(string arg)
        {
            Assert.Throws<ArgumentNullException>(
                () => Args.NotNull(arg, nameof(arg))
            );
        }

        /// <summary>
        /// 引数が null でない場合に 例外が発生しないことをテストします。
        /// </summary>
        /// <param name="arg">検証対象の引数</param>
        [Theory]
        [InlineData("foo")]
        [InlineData("")]
        public static void NotNull_ShouldNotThrowException_WhenClassArgumentIsNotNull(string arg)
        {
            var exception = Record.Exception(
                () => Args.NotNull(arg, nameof(arg))
            );
            Assert.Null(exception);
        }

        /// <summary>
        /// 引数が null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        /// <param name="arg">検証対象の引数</param>
        [Theory]
        [InlineData(null)]
        public static void NotNull_ShouldThrowArgumentNullException_WhenStructArgumentIsNull(int? arg)
        {
            Assert.Throws<ArgumentNullException>(
                () => Args.NotNull(arg, nameof(arg))
            );
        }

        /// <summary>
        /// 引数が null でない場合に 例外が発生しないことをテストします。
        /// </summary>
        /// <param name="arg">検証対象の引数</param>
        [Theory]
        [InlineData(100)]
        [InlineData(0)]
        public static void NotNull_ShouldNotThrowException_WhenStructArgumentIsNotNull(int? arg)
        {
            var exception = Record.Exception(
                () => Args.NotNull(arg, nameof(arg))
            );
            Assert.Null(exception);
        }

        /// <summary>
        /// 引数が null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        /// <param name="arg">検証対象の引数</param>
        [Theory]
        [InlineData(null)]
        public static void NotEmpty_ShouldThrowArgumentNullException_WhenArgumentIsNull(string arg)
        {
            Assert.Throws<ArgumentNullException>(
                () => Args.NotEmpty(arg, nameof(arg))
            );
        }

        /// <summary>
        /// 引数が 空文字 の場合に <see cref="ArgumentException"/> が発生することをテストします。
        /// </summary>
        /// <param name="arg">検証対象の引数</param>
        [Theory]
        [InlineData("")]
        public static void NotEmpty_ShouldThrowArgumentException_WhenArgumentIsEmpty(string arg)
        {
            Assert.Throws<ArgumentException>(
                () => Args.NotEmpty(arg, nameof(arg))
            );
        }

        /// <summary>
        /// 引数が 空のコレクション の場合に <see cref="ArgumentException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public static void NotEmpty_ShouldThrowArgumentException_WhenArgumentIsEmptyCollection()
        {
            var arg = new List<string>();
            Assert.Throws<ArgumentException>(
                () => Args.NotEmpty(arg, nameof(arg))
            );
        }

        /// <summary>
        /// 引数が null 又は 空文字 でない場合に 例外が発生しないことをテストします。
        /// </summary>
        /// <param name="arg">検証対象の引数</param>
        [Theory]
        [InlineData("foo")]
        [InlineData(" ")]
        public static void NotEmpty_ShouldNotThrowException_WhenArgumentIsNotNullOrEmpty(string arg)
        {
            var exception = Record.Exception(
                () => Args.NotEmpty(arg, nameof(arg))
            );
            Assert.Null(exception);
        }
    }
}
