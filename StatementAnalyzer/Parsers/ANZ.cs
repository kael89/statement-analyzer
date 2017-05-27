using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.util;

namespace StatementAnalyzer
{
	public class ANZ : PDFParser
	{
		static string stamp = "ANZ";
		public static string Stamp { get { return stamp; } }
		static RectangleJ stampLocation = new RectangleJ(5.49f, 7.83f, 1.4f, 0.26f);
		public static RectangleJ StampLocation { get { return stampLocation; } }

		RectangleJ transactionsFirstPage = new RectangleJ(0.667f, 0.4f, 9.35f, 6.29f);
		RectangleJ transactionsGeneralPage = new RectangleJ(0.667f, 0.36f, 9.35f, 8.02f);
		const float rowHeight = 0.177f;

		public ANZ(string filepath)
			: base(filepath)
		{
			transactionsColumnWidth = new float[] { 0.88f, 6.87f, 0.89f, 0.69f };
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

			Regex regexDate = new Regex(@"(\d{2})/(\d{2})/(\d{4})");
			Regex regexCurrency = new Regex(@"(\$)(\d*,?\d+\.\d+)");
			Match match;

			for (int i = 0; i < temp.Count; i++)
			{
				if ((match = regexDate.Match(temp[i][0])).Success)
					//Format Date
					temp[i][0] = match.Groups[3].Value + "-" + match.Groups[2].Value + "-" + match.Groups[1].Value;
				else
					continue;

				//Format Currency
				if ((match = regexCurrency.Match(temp[i][2])).Success)
					temp[i][2] = match.Groups[2].Value;
				else
					temp[i][2] = "";

				if ((match = regexCurrency.Match(temp[i][3])).Success)
					temp[i][3] = match.Groups[2].Value;
				else
					temp[i][3] = "";

				result.Add(temp[i]);
			}

			return result;
		}
	}
}
