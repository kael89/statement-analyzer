using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.util;

namespace StatementAnalyzer
{
	public class Westpac : PDFParser
	{
		static string stamp = "Westpac";
		public static string Stamp { get { return stamp; } }
		static RectangleJ stampLocation = new RectangleJ(1.12f, 6.57f, 0.58f, 0.16f);
		public static RectangleJ StampLocation { get { return stampLocation; } }

		RectangleJ transactionsFirstPage = new RectangleJ(0.9f, 1.8f, 6.57f, 4.66f);
		RectangleJ transactionsGeneralPage = new RectangleJ(0.9f, 1.8f, 6.57f, 7.48f);
		const float rowHeight = 0.23f;

		public Westpac(string filepath)
			:base(filepath)
		{
			transactionsColumnWidth = new float[] { 0.55f, 2.75f, 1.02f, 1.05f };
		}

		public override List<string[]> GetTransactions()
		{
			List<string[]> temp = new List<string[]>();
			List<string[]> result = new List<string[]>();

			Regex regexYearDescription = new Regex("STATEMENT OPENING BALANCE");
			Regex regexClosingBalance = new Regex("CLOSING BALANCE");
			Regex regexDate = new Regex(@"(\d{2})(\s|-)(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)");
			string year = "2015";
			Match match;

			bool closingStatementFound = false;
			bool openingStatementFound = false;
			RectangleJ transactionsArea = transactionsFirstPage;
			for (int page = 1; page <= numberOfPages; page++)
			{
				temp = GetTableCells(page, transactionsArea, rowHeight, transactionsColumnWidth);

				for (int i = 0; i < temp.Count; i++)
				{
					RemoveDots(temp[i]);

					if (regexYearDescription.Match(temp[i][1]).Success)
					{
						openingStatementFound = true;
						transactionsArea = transactionsGeneralPage;
						year = temp[i][0];
						continue;
					}
					else if (regexClosingBalance.Match(temp[i][1]).Success)
					{
						transactionsArea = transactionsFirstPage;
						closingStatementFound = true;
						break;
					}
					else if ((match = regexDate.Match(temp[i][0])).Success)
						temp[i][0] = match.Groups[1].Value + "-" + match.Groups[3].Value + "-" + year;
					else
					{
						//If previous cell in Date containted a date
						if (temp[i][0] == "" && i > 0 && regexDate.Match(temp[i - 1][0]).Success)
						{
							temp[i - 1][1] += " " + temp[i][1];
							temp[i - 1][2] = temp[i][2];
							temp[i - 1][3] = temp[i][3];
						}
						continue;
					}

					if (openingStatementFound)
						result.Add(temp[i]);
				}

				if (closingStatementFound)
				{
					openingStatementFound = closingStatementFound = false;
					continue;
				}
			}

			return result;
		}

		void RemoveDots(string[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				Match match = Regex.Match(columns[i], @"\.{2,}");
				if (match.Success)
					columns[i] = columns[i].Remove(match.Index, match.Value.Length);
				columns[i] = (columns[i].Replace("\n", "")).Trim();
			}
		}
	}
}
