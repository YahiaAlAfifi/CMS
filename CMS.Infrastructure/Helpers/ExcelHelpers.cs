using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CMS.Infrastructure.Helpers
{
    public class ExcelColumn
    {
        public ExcelColumn(string name, int order = 0)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; set; }
        public int Order { get; set; }
    }

    public class ExcelRow
    {
        public Dictionary<string, string> Values { get; set; }
    }

    public static class ExcelHelpers
    {
        public static byte[] ToExcel(
            Dictionary<string, ExcelColumn> columns,
            List<ExcelRow> rows)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Report");
            var orderedColumns = columns.OrderBy(x => x.Value.Order).ToList();

            worksheet.Cell(1, 1).Value = "#";
            var columnNumber = 2;

            foreach (var column in orderedColumns)
            {
                worksheet.Cell(1, columnNumber++).Value = column.Value.Name;
            }

            var rowNumber = 2;
            foreach (var row in rows)
            {
                worksheet.Cell(rowNumber, 1).Value = rowNumber - 1;
                columnNumber = 2;

                foreach (var column in orderedColumns)
                {
                    if (row.Values.TryGetValue(column.Key, out var value) ||
                        row.Values.TryGetValue(column.Value.Name, out value))
                    {
                        worksheet.Cell(rowNumber, columnNumber).Value = value;
                    }

                    columnNumber++;
                }

                rowNumber++;
            }

            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
