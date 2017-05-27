using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.util;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace StatementAnalyzer
{
	public class PDFParser
	{
		protected string filepath;
		protected int numberOfPages;
		protected float[] transactionsColumnWidth;
		const float dpi = 72.0f;

		public PDFParser(string filepath)
		{
			this.filepath = filepath;
			using (PdfReader reader = new PdfReader(filepath))
				numberOfPages = reader.NumberOfPages;
		}

		public company GetCompany()
		{
			if (CheckCompanyName(ANZ.Stamp, ANZ.StampLocation, false))
				return company.ANZ;
			else if (CheckCompanyName(Asgard.Stamp, Asgard.StampLocation, false))
				return company.Asgard;
			else if (CheckCompanyName(Linear.Stamp, Linear.StampLocation, true))
				return company.Linear;
			else if (CheckCompanyName(Westpac.Stamp, Westpac.StampLocation, false))
				return company.Westpac;
			else
				return company.other;
		}

		bool CheckCompanyName(string stamp, RectangleJ stampLocation, bool landscape)
		{
			Regex regex = new Regex(@stamp);
			Match match = regex.Match(GetTextByLocation(1, stampLocation, landscape));
			return match.Success;
		}

		//Extract word by location(rect)/
		string GetTextByLocation(int page, RectangleJ area, bool landscape)
		{
			const float dpi = 72.0f;
			float landscapeHeight = 8.23f;
			RectangleJ location = new RectangleJ(area.X, area.Y, area.Width, area.Height);

			if (landscape)
			{
				location.X = landscapeHeight- area.Y - area.Height;
				location.Y = area.X;
				location.Width = area.Height;
				location.Height = area.Width;
			}

			location.X *= dpi;
			location.Y *= dpi;
			location.Width *= dpi;
			location.Height *= dpi;
			
			RenderFilter[] filter = { new RegionTextRenderFilter(location) };
			ITextExtractionStrategy strategy;
			StringBuilder text = new StringBuilder();

			using (PdfReader reader = new PdfReader(filepath))
			{
				strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
				text.AppendLine(PdfTextExtractor.GetTextFromPage(reader, page, strategy));
			}
			return text.ToString();
		}

		List<RectangleJ> GetRowArea(int page, RectangleJ area, float rowHeight, float rowHeightOffset)	/* Gets row areas from a given area in one page */
			{
				List<RectangleJ> result = new List<RectangleJ>();

			float x = area.X;
			float y = area.Y + area.Height - rowHeight;

			for (;  y >= area.Y; y -= rowHeight)
			{
				result.Add(new RectangleJ(x, y, area.Width, rowHeight + 2 * rowHeightOffset));
			}

			return result;
		}

		string[] GetColumns(int page, RectangleJ row, float[] columnWidth, bool landscape)	/* Gets column text given a specified row area and column widths */
		{
			string[] result = new string[columnWidth.Length];
			RectangleJ column = new RectangleJ(row.X, row.Y, columnWidth[0], row.Height);

			for (int i = 0; i < columnWidth.Length; i++)
			{
				column.Width = columnWidth[i];
				result[i] = GetTextByLocation(page, column, landscape).Trim();
				column.X += columnWidth[i];
			}

			return result;
		}

		/***GetTableCells() overloads***/
		public List<string[]> GetTableCells(int page, RectangleJ area, float rowHeight, float[] columnWidth)	
		{
			return GetTableCells(page, area, rowHeight, 0f, columnWidth, false);
		}

		public List<string[]> GetTableCells(int page, RectangleJ area, float rowHeight, float rowHeightOffset, float[] columnWidth)
		{
			return GetTableCells(page, area, rowHeight, rowHeightOffset, columnWidth, false);
		}

		public List<string[]> GetTableCells(int page, RectangleJ area, float rowHeight, float[] columnWidth, bool landscape)
		{
			return GetTableCells(page, area, rowHeight, 0f, columnWidth, landscape);
		}
		/******/

		public List<string[]> GetTableCells(int page, RectangleJ area, float rowHeight, float rowHeightOffset, float[] columnWidth, bool landscape)
		{
			List<string[]> result = new List<string[]>();

			List<RectangleJ> rows = new List<RectangleJ>();
			rows = GetRowArea(page, area, rowHeight, rowHeightOffset);

			foreach (RectangleJ row in rows)
				result.Add(GetColumns(page, row, columnWidth, landscape));

			/* Debugging */
			/*
			int i = 0;
			foreach (string[] row in result) {
				Console.WriteLine("Row: " + i++);
				for (int j = 0; j < 150; j++)
				{
					Console.Write("*");
				}
				Console.WriteLine();
				int colNo = 0;
				foreach (string column in row)
					Console.WriteLine("Column {0}: {1}", colNo++, column);
				Console.WriteLine();
			}
			/* END Debugging */

			return result;
		}

		public virtual List<string[]> GetTransactions()
		{
			return new List<string[]>();
		}
	}
}
