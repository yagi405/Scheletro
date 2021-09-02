using System;
using System.IO;
using ClosedXML.Excel;
using Xunit;

namespace Scheletro.ExcelHelper.Tests
{
    /// <summary>
    /// <see cref="ExcelOperation"/> �N���X�̃e�X�g�N���X�ł��B
    /// </summary>
    public class ExcelOperationTests : IDisposable
    {
        private readonly ExcelOperation _operation;

        private readonly IXLWorkbook _testWorkbook;

        private readonly string _testFilePath;

        /// <summary>
        /// <see cref="ExcelOperationTests"/> �N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        public ExcelOperationTests()
        {
            _operation = new ExcelOperation();

            _testWorkbook = _operation.NewWorkbook();
            _testFilePath = Path.GetFullPath($"{nameof(ExcelOperationTests)}.xlsx");
            _testWorkbook.SaveAs(_testFilePath);
        }

        /// <summary>
        /// �V���� Workbook ��Ԃ����Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void NewWorkbook_ShouldReturnNewWorkbook()
        {
            var workbook = _operation.NewWorkbook();
            Assert.NotNull(workbook);
        }

        /// <summary>
        /// �w�肳�ꂽ Excel �t�@�C���̃p�X�� null �̏ꍇ�� <see cref="ArgumentNullException"/> ���������邱�Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _operation.OpenWorkbook(null)
                );
        }

        /// <summary>
        /// �w�肳�ꂽ Excel �t�@�C���̃p�X�� �󕶎� �̏ꍇ�� <see cref="ArgumentException"/> ���������邱�Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => _operation.OpenWorkbook("")
            );
        }

        /// <summary>
        /// �w�肳�ꂽ Excel �t�@�C���̃p�X�����݂��Ȃ��ꍇ�� <see cref="FileNotFoundException"/> ���������邱�Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldThrowFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(
                () => _operation.OpenWorkbook("NotFound.xlsx")
            );
        }

        /// <summary>
        /// Workbook ��Ԃ����Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenWorkbook_ShouldReturnWorkbook()
        {
            var workbook = _operation.OpenWorkbook(_testFilePath);
            Assert.NotNull(workbook);
        }

        /// <summary>
        /// �w�肳�ꂽ Excel �t�@�C���̃p�X�� null �̏ꍇ�� <see cref="ArgumentNullException"/> ���������邱�Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _operation.OpenTemplateWorkbook(null)
            );
        }

        /// <summary>
        /// �w�肳�ꂽ Excel �t�@�C���̃p�X�� �󕶎� �̏ꍇ�� <see cref="ArgumentException"/> ���������邱�Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => _operation.OpenTemplateWorkbook("")
            );
        }

        /// <summary>
        /// �w�肳�ꂽ Excel �t�@�C���̃p�X�����݂��Ȃ��ꍇ�� <see cref="FileNotFoundException"/> ���������邱�Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldThrowFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(
                () => _operation.OpenTemplateWorkbook("NotFound.pptx")
            );
        }

        /// <summary>
        /// Workbook ��Ԃ����Ƃ��e�X�g���܂��B
        /// </summary>
        [Fact]
        public void OpenTemplateWorkbook_ShouldReturnWorkbook()
        {
            var workbook = _operation.OpenTemplateWorkbook(_testFilePath);
            Assert.NotNull(workbook);
        }

        /// <summary>
        /// �{�N���X�Ŏ�舵���A���}�l�[�W�h���\�[�X���J�����܂��B
        /// </summary>
        private void ReleaseUnmanagedResources()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        /// <summary>
        /// �{�N���X�ŕێ����� Excel �֘A�̃��\�[�X���J�����܂��B
        /// </summary>
        /// <param name="disposing">�t�@�C�i���C�U�[����Ăяo���ꂽ�Ƃ��� false�AIDisposable.Dispose ���\�b�h����Ăяo���ꂽ�Ƃ��� true</param>
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
        /// <see cref="IDisposable.Dispose"/> �̎����ł��B
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// �{�N���X�ŕێ����� Excel �֘A�̃��\�[�X���J�����܂��B
        /// </summary>
        ~ExcelOperationTests()
        {
            Dispose(false);
        }
    }
}
