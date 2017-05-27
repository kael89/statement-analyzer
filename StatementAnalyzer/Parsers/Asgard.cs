using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.util;

namespace StatementAnalyzer
{
	public class Asgard : PDFParser
	{
		static string stamp = "advisernet";
		public static string Stamp { get { return stamp; } }
		static RectangleJ stampLocation = new RectangleJ(1.61f, 0f, 0.71f, 0.19f);
		public static RectangleJ StampLocation { get { return stampLocation; } }

		RectangleJ transactionsFirstPage = new RectangleJ(0.87f, 0.7f, 5.92f, 6.97f);
		RectangleJ transactionsGeneralPage = new RectangleJ(0.87f, 0.7f, 5.92f, 10.25f);
		const float rowHeight = 0.21f;

		public Asgard(string filepath)
			: base(filepath)
		{
			transactionsColumnWidth = new float[] { 0.89f, 3.42f, 0.86f, 0.75f };
		}

		public override List<string[]> GetTransactions()
		{
			List<string[]> temp = new List<string[]>();
			RectangleJ transactionsPage;

			for (int i = 1; i <= numberOfPages; i++)
			{
				transactionsPage = (i == 1) ? transactionsFirstPage : transactionsGeneralPage;
				temp.AddRange(GetTableCells(i, transactionsPage, rowHeight, transactionsColumnWidth));
			}

			List<string[]> result = new List<string[]>();
			Regex regexDate = new Regex(@"\d{2}-(JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)-\d{4}");
			Regex regexAccountNo = new Regex(@"^(\d{18})\s\D");
			Match match;
			string date;

			for (int i = 0; i < temp.Count; i++)
			{
				date = temp[i][0];
				match = regexDate.Match(date);

				if (!match.Success)
				{
					if (date == "" && temp[i][1] != "")
						temp[i-1][1] += " " + temp[i][1];
					continue;
				}
				else
				{
					//Remove account no from text column
					match = regexAccountNo.Match(temp[i][1]);
					if (match.Success)
						temp[i][1] = temp[i][1].Remove(0, 19);
				}

				result.Add(temp[i]);
			}

			result.RemoveAt(0);
			result.RemoveAt(result.Count - 1);

			return result;
		}
	}
}
