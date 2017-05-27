using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace StatementAnalyzer
{
	public class ExcelWriter :IDisposable
	{
		Application app;
		Workbook workbook;
		Worksheet worksheet;

		public ExcelWriter()
		{
			try
			{
				app = new Application();
				app.Visible = true;
				workbook = app.Workbooks.Add(1);
				worksheet = (Worksheet)workbook.Sheets[1];
			}
			catch (Exception)
			{
				System.Windows.Forms.MessageBox.Show("Error: Microsoft Excel is not properly installed in your system");
			}
		}

		public void WriteCells(Dictionary<cell, string> cells)
		{
			foreach (KeyValuePair<cell, string> cell in cells)
				worksheet.Cells[cell.Key.row, cell.Key.col] = cell.Value;
		}

		public void SetColumnSize(int[] columnSize)
		{
			for (int i = 1; i < columnSize.Length; i++)
				worksheet.Cells[1, i].ColumnWidth = columnSize[i];
		}

		public void MakeRowBold(int row)
		{
			worksheet.Cells[row, 1].EntireRow.Font.Bold = true;
		}

		public void Dispose()
		{
			Marshal.FinalReleaseComObject(app);
			Marshal.FinalReleaseComObject(worksheet);
			Marshal.FinalReleaseComObject(workbook);
		}
	}
}
