using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Drawing;
using System.IO;

class Program
{
    static void Main()
    {
        // No LicenseContext setting required in EPPlus v4.x

        using (var package = new ExcelPackage())
        {
            // Create a worksheet
            var worksheet = package.Workbook.Worksheets.Add("SampleSheet");

            // Create a DataTable to simulate data
            DataTable dataTable = GetSampleData();

            // Start populating data from row 3 for this example
            int startRow = 3;
            int startCol = 1;

            // Set up merged headers
            worksheet.Cells[startRow - 2, startCol, startRow - 2, startCol + dataTable.Columns.Count - 1].Merge = true;
            worksheet.Cells[startRow - 2, startCol].Value = "Merged Header for the Table";
            worksheet.Cells[startRow - 2, startCol].Style.Font.Bold = true;
            worksheet.Cells[startRow - 2, startCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[startRow - 2, startCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[startRow - 2, startCol].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            worksheet.Cells[startRow - 2, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            // Load DataTable into worksheet starting from row 3
            worksheet.Cells[startRow, startCol].LoadFromDataTable(dataTable, true);

            // Style for the header row
            using (ExcelRange headerRange = worksheet.Cells[startRow, startCol, startRow, startCol + dataTable.Columns.Count - 1])
            {
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }

            // Style for data cells
            using (ExcelRange dataRange = worksheet.Cells[startRow + 1, startCol, startRow + dataTable.Rows.Count, startCol + dataTable.Columns.Count - 1])
            {
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }

            // Apply AutoFitColumns to the specific range (the entire table in this case)
            worksheet.Cells[startRow, startCol, startRow + dataTable.Rows.Count, startCol + dataTable.Columns.Count - 1].AutoFitColumns();

            // Auto-fit columns for better visibility
            worksheet.Cells.AutoFitColumns();

            // Save the Excel package
            var fileInfo = new FileInfo("SampleExcel_EPPlus_v4.xlsx");
            package.SaveAs(fileInfo);

            Console.WriteLine($"Excel file '{fileInfo.FullName}' created successfully.");
        }
    }

    // Method to generate sample data
    static DataTable GetSampleData()
    {
        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));
        dataTable.Columns.Add("Country", typeof(string));

        dataTable.Rows.Add(1, "Alice", 30, "USA");
        dataTable.Rows.Add(2, "Bob", 25, "Canada");
        dataTable.Rows.Add(3, "Charlie", 35, "UK");
        dataTable.Rows.Add(4, "Daisy", 28, "Australia");

        return dataTable;
    }
}
