using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using ClosedXML.Report;
using Scheletro.ArgumentValidator.NETStandard2_0;

namespace Scheletro.ExcelHelper.NETStandard2_0
{
    /// <summary>
    /// <see cref="ExcelOperation"/> における、Excel 操作処理でのコンテキスト情報を保持するクラスです。
    /// </summary>
    public sealed class ExcelOperationContext : IDisposable
    {
        /// <summary>
        /// Workbooks を取得します。
        /// </summary>
        public IList<IXLWorkbook> Workbooks { get; }

        /// <summary>
        /// Templates を取得します。
        /// </summary>
        public IList<XLTemplate> Templates { get; }

        /// <summary>
        /// 一時フォルダへコピーしたテンプレートファイルごとのパス
        /// </summary>
        private readonly List<string> _tempFilePath;

        /// <summary>
        /// <see cref="ExcelOperationContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ExcelOperationContext()
        {
            Workbooks = new List<IXLWorkbook>();
            Templates = new List<XLTemplate>();
            _tempFilePath = new List<string>();
        }

        /// <summary>
        /// 新しい Workbook を生成し、取得します。
        /// </summary>
        /// <returns>新しい Workbook</returns>
        public IXLWorkbook NewWorkbook()
        {
            var workbook = new XLWorkbook();
            workbook.Worksheets.Add();
            Workbooks.Add(workbook);
            return workbook;
        }

        /// <summary>
        /// 指定された Excel ファイルを開き、対応する Workbook を取得します。
        /// </summary>
        /// <param name="filePath">Excel ファイルのパス</param>
        /// <param name="isReadOnly">読み取り専用で開く場合は true、それ以外は false</param>
        /// <returns>指定された Excel ファイルに対応する Workbook</returns>
        public IXLWorkbook OpenWorkbook(string filePath, bool isReadOnly)
        {
            Args.NotEmpty(filePath, nameof(filePath));
            IXLWorkbook workbook;
            if (isReadOnly)
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    workbook = new XLWorkbook(fs, XLEventTracking.Disabled);
                }
            }
            else
            {
                workbook = new XLWorkbook(Path.GetFullPath(filePath));
            }
            Workbooks.Add(workbook);
            return workbook;
        }

        /// <summary>
        /// 指定された Excel ファイルをテンプレートとして開き、対応する Template を取得します。
        /// </summary>
        /// <param name="filePath">テンプレートとする Excel ファイルのパス</param>
        /// <returns>テンプレートとする Excel ファイル（のコピー）に対応する Workbook</returns>
        public XLTemplate OpenTemplateWorkbook(string filePath)
        {
            Args.NotEmpty(filePath, nameof(filePath));
            var tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.{Path.GetExtension(filePath)}");
            File.Copy(filePath, tempPath);
            _tempFilePath.Add(tempPath);
            var template = new XLTemplate(tempPath);
            Templates.Add(template);
            return template;
        }

        /// <summary>
        /// 本クラスで取り扱うアンマネージドリソースを開放します。
        /// </summary>
        private void ReleaseUnmanagedResources()
        {
            foreach (var tempFilePath in _tempFilePath)
            {
                try
                {
                    File.Delete(tempFilePath);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            _tempFilePath.Clear();
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

            if (Workbooks != null)
            {
                foreach (var workbook in Workbooks)
                {
                    workbook?.Dispose();
                }
            }

            if (Templates == null)
            {
                return;
            }

            foreach (var template in Templates)
            {
                template?.Dispose();
            }
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
        ~ExcelOperationContext()
        {
            Dispose(false);
        }
    }
}