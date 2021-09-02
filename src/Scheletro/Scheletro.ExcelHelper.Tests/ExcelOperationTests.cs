using System;
using System.IO;
using ClosedXML.Excel;
using Xunit;

namespace Scheletro.ExcelHelper.Tests
{
    /// <summary>
    /// <see cref="ExcelOperation"/> クラスのテストクラスです。
    /// </summary>
    public class ExcelOperationTests : IDisposable
    {
        private readonly ExcelOperation _operation;

        private readonly IXLWorkbook _testWorkbook;

        private readonly string _testFilePath;

        /// <summary>
        /// <see cref="ExcelOperationTests"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ExcelOperationTests()
        {
            _operation = new ExcelOperation();

            _testWorkbook = _operation.NewWorkbook();
            _testFilePath = Path.GetFullPath($"{nameof(ExcelOperationTests)}.xlsx");
            _testWorkbook.SaveAs(_testFilePath);
        }

        /// <summary>
        /// 新しい Workbook を返すことをテストします。
        /// </summary>
        [Fact]
        public void NewWorkbook_ShouldReturnNewWorkbook()
        {
            var workbook = _operation.NewWorkbook();
            Assert.NotNull(workbook);
        }

        /// <summary>
        /// 指定された Excel ファイルのパスが null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _operation.OpenWorkbook(null)
                );
        }

        /// <summary>
        /// 指定された Excel ファイルのパスが 空文字 の場合に <see cref="ArgumentException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => _operation.OpenWorkbook("")
            );
        }

        /// <summary>
        /// 指定された Excel ファイルのパスが存在しない場合に <see cref="FileNotFoundException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldThrowFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(
                () => _operation.OpenWorkbook("NotFound.xlsx")
            );
        }

        /// <summary>
        /// Workbook を返すことをテストします。
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldReturnWorkbook()
        {
            var workbook = _operation.OpenWorkbook(_testFilePath);
            Assert.NotNull(workbook);
        }

        /// <summary>
        /// 指定された Excel ファイルのパスが null の場合に <see cref="ArgumentNullException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _operation.OpenTemplateWorkbook(null)
            );
        }

        /// <summary>
        /// 指定された Excel ファイルのパスが 空文字 の場合に <see cref="ArgumentException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => _operation.OpenTemplateWorkbook("")
            );
        }

        /// <summary>
        /// 指定された Excel ファイルのパスが存在しない場合に <see cref="FileNotFoundException"/> が発生することをテストします。
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldThrowFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(
                () => _operation.OpenTemplateWorkbook("NotFound.pptx")
            );
        }

        /// <summary>
        /// Workbook を返すことをテストします。
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldReturnWorkbook()
        {
            var workbook = _operation.OpenTemplateWorkbook(_testFilePath);
            Assert.NotNull(workbook);
        }

        /// <summary>
        /// 本クラスで取り扱うアンマネージドリソースを開放します。
        /// </summary>
        private void ReleaseUnmanagedResources()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        /// <summary>
        /// 本クラスで保持する Excel 関連のリソースを開放します。
        /// </summary>
        /// <param name="disposing">ファイナライザーから呼び出されたときは false、IDisposable.Dispose メソッドから呼び出されたときは true</param>
        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (!disposing)
            {
                return;
            }
            _operation?.Dispose();
            _testWorkbook?.Dispose();
        }

        /// <summary>
        /// <see cref="IDisposable.Dispose"/> の実装です。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 本クラスで保持する Excel 関連のリソースを開放します。
        /// </summary>
        ~ExcelOperationTests()
        {
            Dispose(false);
        }
    }
}
