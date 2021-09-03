using System;
using System.IO;
using System.Linq;
using ClosedXML.Report;
using Scheletro.ArgumentValidator.NETStandard2_0;

namespace Scheletro.ExcelHelper.NETStandard2_0
{
    /// <summary>
    /// Excel レポートの作成機能を提供するクラスです。
    /// </summary>
    public class ExcelReportCreator : ExcelOperation
    {
        /// <summary>
        /// Excel レポートを作成し、Stream として取得します。
        /// </summary>
        /// <typeparam name="T">Excel レポートのデータとなる型</typeparam>
        /// <param name="model">Excel レポートのデータ</param>
        /// <param name="templateFilePath">Excel レポートのテンプレートファイルパス</param>
        /// <returns>Excel レポートの Stream</returns>
        public Stream CreateReportAsStreamFromTemplate<T>(T model, string templateFilePath) where T : class
        {
            Args.NotNull(model, nameof(model));
            Args.NotEmpty(templateFilePath, nameof(templateFilePath));

            var template = GenerateTemplate(model, templateFilePath);

            using (var stream = new MemoryStream())
            {
                template.SaveAs(stream);
                return stream;
            }
        }

        /// <summary>
        /// Excel レポートを作成します。
        /// </summary>
        /// <typeparam name="T">Excel レポートのデータとなる型</typeparam>
        /// <param name="model">Excel レポートのデータ</param>
        /// <param name="templateFilePath">Excel レポートのテンプレートファイルパス</param>
        /// <param name="outputFilePath">Excel レポートの出力ファイルパス</param>
        public void CreateReportFromTemplate<T>(T model, string templateFilePath, string outputFilePath) where T : class
        {
            Args.NotNull(model, nameof(model));
            Args.NotEmpty(templateFilePath, nameof(templateFilePath));

            var template = GenerateTemplate(model, templateFilePath);

            template.SaveAs(outputFilePath);
        }

        private XLTemplate GenerateTemplate<T>(T model, string templateFilePath)
        {
            if (!File.Exists(templateFilePath))
            {
                throw new ExcelReportCreatorException($"Could not find template file '{templateFilePath}'.");
            }

            var template = OpenTemplateWorkbook(templateFilePath);

            template.AddVariable(model);
            var result = template.Generate();

            if (result.HasErrors)
            {
                throw new ExcelReportCreatorException(
                    string.Join(Environment.NewLine, result.ParsingErrors.Select(x => $"・{x.Message}"))
                );
            }

            return template;
        }
    }
}
