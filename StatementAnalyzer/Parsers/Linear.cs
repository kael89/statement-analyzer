using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.util;

namespace StatementAnalyzer
{
	public class Linear : PDFParser
	{
		static string stamp = "Linear";
		public static string Stamp { get { return stamp; } }
		static RectangleJ stampLocation = new RectangleJ(5.95f, 3.04f, 0.74f, 0.3f);
		public static RectangleJ StampLocation { get { return stampLocation; } }

		RectangleJ transactionsFirstPage = new RectangleJ(0.43f, 0.71f, 6.8f, 5.27f);
		RectangleJ transactionsGeneralPage = new RectangleJ(0.43f, 0.59f, 6.8f, 6.1f);
		int transactionsPageNumber;
		const float rowHeight = 0.21f;

		public Linear(string filepath)
			:base(filepath)
		{
			transactionsColumnWidth = new float[] { 0.73f, 3.94f, 1.09f, 1.04f };
			transactionsPageNumber = GetTransactionsPage();
		}

		public override List<string[]> GetTransactions()
		{
			List<string[]> temp = new List<string[]>();
			List<string[]> result = new List<string[]>();

			float[] transactionsHeader = new float[] { 0.73f };
			int rowsToHeader;
			bool headerFound = false;
			bool transactionsEnd = false;

			for (int page = transactionsPageNumber; page <= numberOfPages && !transactionsEnd; page++)
			{
				rowsToHeader = 0;
				//Find where Transactions start
				if (!headerFound)
				{
					rowsToHeader = GetRowsToHeader(page);
					if (rowsToHeader > 0)
						headerFound = true;
				}

				//Get Transactions
				if (headerFound)
				{
					RectangleJ area = GetTransactionsArea(page);
					area.Height -= rowsToHeader * rowHeight;

					temp = GetTableCells(page, area, rowHeight, transactionsColumnWidth, true);
					foreach (string[] row in temp)
					{
						Match match;

						if (Regex.Match(row[1], "Opening Balance").Success || Regex.Match(row[0], "MA_CASH").Success)
							continue;
						else
						{
							match = Regex.Match(row[0], @"(\d{2})\/(\d{2})\/(\d{4})");

							if (match.Success)
							{
								//Format Date
								row[0] = match.Groups[3].Value + "-" + match.Groups[2].Value + "-" + match.Groups[1].Value;
								row[2] = (row[2] == "-") ? "" : row[2].Substring(1);
								result.Add(row);
							}
							else if (row[0] != "" && !Regex.Match(row[0], "Code").Success)
							{
								transactionsEnd = true;
								break;
							}
						}						
					}
				}
			}

			return result;		 
		}

		int GetTransactionsPage()
		{
			RectangleJ area = new RectangleJ(1.09f, 0.45f, 8f, 6.09f);
			float rowHeight = 0.31f;
			float[] columnWidth = new float[] { 6f, 2f };

			List<string[]> contents = GetTableCells(2, area, rowHeight, columnWidth, true);
			foreach (string[] row in contents)
				if (Regex.Match(row[0], "Detailed Cash Flow").Success)
					return int.Parse(row[1]);

			return -1;
		}

		int GetRowsToHeader(int page)
		{
			RectangleJ area = GetTransactionsArea(page);
			area.Width = 0.73f;
			float[] column = new float[] { 0.73f };

			int i = 1;
			List<string[]> rows = GetTableCells(page, area, rowHeight, column, true);
			foreach (string[] header in rows)
			{  
				if (Regex.Match(header[0], "MA_CASH").Success)
					return i;
				i++;
			}
			return 0;
		}

		RectangleJ GetTransactionsArea(int page)
		{
			return (page == transactionsPageNumber) ? new RectangleJ(transactionsFirstPage.X, transactionsFirstPage.Y, 
															transactionsFirstPage.Width, transactionsFirstPage.Height)
												   : new RectangleJ(transactionsGeneralPage.X, transactionsGeneralPage.Y, 
															transactionsGeneralPage.Width, transactionsGeneralPage.Height);
		}
	}
}
