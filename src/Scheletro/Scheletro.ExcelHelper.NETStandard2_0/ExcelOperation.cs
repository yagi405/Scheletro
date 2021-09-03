using System;
using ClosedXML.Excel;
using ClosedXML.Report;
using Scheletro.ArgumentValidator.NETStandard2_0;

namespace Scheletro.ExcelHelper.NETStandard2_0
{
    /// <summary>
    /// Excel 操作を伴う処理に関する共通機能を提供するクラスです。
    /// </summary>
    public class ExcelOperation : IDisposable
    {
        /// <summary>
        /// Excel 操作処理でのコンテキスト情報
        /// </summary>
        private readonly ExcelOperationContext _context;

        /// <summary>
        /// <see cref="ExcelOperation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ExcelOperation()
        {
            _context = new ExcelOperationContext();
        }

        /// <summary>
        /// 新しい Workbook を生成し、取得します。
        /// </summary>
        /// <returns>新しい Workbook</returns>
        public IXLWorkbook NewWorkbook()
        {
            return _context.NewWorkbook();
        }

        /// <summary>
        /// 指定された Excel ファイルを開き、対応する Workbook を取得します。
        /// </summary>
        /// <param name="filePath">Excel ファイルのパス</param>
        /// <param name="isReadOnly">読み取り専用で開く場合は true、それ以外は false</param>
        /// <returns>指定された Excel ファイルに対応する Workbook</returns>
        public IXLWorkbook OpenWorkbook(string filePath, bool isReadOnly = false)
        {
            Args.NotEmpty(filePath, nameof(filePath));
            return _context.OpenWorkbook(filePath, isReadOnly);
        }

        /// <summary>
        /// 指定された Excel ファイルをテンプレートとして開き、対応する Workbook を取得します。
        /// </summary>
        /// <param name="filePath">テンプレートとする Excel ファイルのパス</param>
        /// <returns>テンプレートとする Excel ファイル（のコピー）に対応する Workbook</returns>

        public XLTemplate OpenTemplateWorkbook(string filePath)
        {
            Args.NotEmpty(filePath, nameof(filePath));
            return _context.OpenTemplateWorkbook(filePath);
        }

        /// <summary>
        /// 本クラスで保持する Excel 関連のリソースを開放します。
        /// </summary>
        /// <param name="disposing">ファイナライザーから呼び出されたときは false、IDisposable.Dispose メソッドから呼び出されたときは true</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            _context?.Dispose();
        }

        /// <summary>
        /// <see cref="IDisposable.Dispose"/> の実装です。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}