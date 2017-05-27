using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StatementAnalyzer
{
	public partial class Form1 : Form
	{
		PDFParser PDFParser;
		ExcelWriter excelWriter;

		public Form1()
		{
			InitializeComponent();
		}

		private void browsePDF_Click(object sender, EventArgs e)
		{
			string filepath;
			if (browsePDFDialog.ShowDialog() == DialogResult.OK)
			{
				status.Text = "Status: Ready for convertion";
				filepath = browsePDFDialog.FileName;
				PDFParser = new PDFParser(filepath);
				company company = PDFParser.GetCompany();
				switch (company)
				{
					case company.ANZ:
						filepathTextBox.Text = filepath;
						PDFParser = new ANZ(filepath);
						break;
					case company.Asgard:
						filepathTextBox.Text = filepath;
						PDFParser = new Asgard(filepath);
						break;
					case company.Linear:
						filepathTextBox.Text = filepath;
						PDFParser = new Linear(filepath);
						break;
					case company.Westpac:
						filepathTextBox.Text = filepath;
						PDFParser = new Westpac(filepath);
						break;
					default:
						string output = "Unknown bank/organisation";
						MessageBox.Show(output);
						break;
				}
			}
		}

		private void convert_Click(object sender, EventArgs e)
		{
			if (PDFParser == null)
			{
				MessageBox.Show("Please select a PDF file");
			}
			else
			{
				try
				{
					WriteData();
				}
				catch (System.IO.IOException error)
				{
					if (error.Message == "")
						MessageBox.Show("Data write error: Please select a valid location");
					else
						MessageBox.Show(error.Message);
				}
			}
		}

		private void WriteData()
		{
			status.Text = "Status: Converting...";
			using (excelWriter = new ExcelWriter())
			{
				WriteToXl();
				status.Text = "Status: Document converted!";
			}
		}

		//Returns the sizes of the Excel columns
		private int[] XlColumnSize()
		{
			int max = 0;
			int column;
			foreach (xlHeader header in Enum.GetValues(typeof(xlHeader)))
			{
				column = (int)header;
				if (column > max)
					max = column;
			}
			max++;

			int[] columnSize = new int[max];

			Array.Clear(columnSize, 0, columnSize.Length);
			columnSize[(int)xlHeader.Transaction_Date] = 20;
			columnSize[(int)xlHeader.Amount] = 15;
			columnSize[(int)xlHeader.Text] = 45;
			
			return columnSize;
		}

		private void WriteToXl()
		{
			excelWriter.SetColumnSize(XlColumnSize());

			cell xlCell;
			Dictionary<cell, string> cells;

			//Write headers
			cells = new Dictionary<cell, string>();
			foreach (xlHeader header in Enum.GetValues(typeof(xlHeader)))
			{
				xlCell = new cell(1, (int) header);
				string text = Enum.GetName(typeof(xlHeader), header);
				cells.Add(xlCell, text);
			}
			excelWriter.MakeRowBold(1);
			excelWriter.WriteCells(cells);

			cells = new Dictionary<cell, string>();

			List<string[]> transactions = PDFParser.GetTransactions();

			int row = 2;
			foreach (string[] transaction in transactions)
			{
				//Write transactions
				xlCell = new cell(row, (int)xlHeader.Transaction_Date);
				cells.Add(xlCell, transaction[0]);

				xlCell = new cell(row, (int)xlHeader.Text);
				cells.Add(xlCell, transaction[1]);

				xlCell = new cell(row, (int)xlHeader.Amount);
				if (transaction[2] == "")
				{
					cells.Add(xlCell, "-" + transaction[3]);
				}
				else
					cells.Add(xlCell, transaction[2]);

				row++;
			}
			excelWriter.WriteCells(cells);
		}
	}

	public struct cell
	{
		public int row;
		public int col;

		public cell(int row, int col)
		{
			this.row = row;
			this.col = col;
		}
	}

	public enum company
	{
		ANZ,
		Asgard,
		Linear,
		Westpac,
		other
	}

	public enum xlHeader
	{	
		Transaction_Date = 1,
		Amount,
		Text
	}
}
