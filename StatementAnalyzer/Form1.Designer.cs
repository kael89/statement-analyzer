namespace StatementAnalyzer
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.browsePDF = new System.Windows.Forms.Button();
			this.browsePDFLabel = new System.Windows.Forms.Label();
			this.browsePDFDialog = new System.Windows.Forms.OpenFileDialog();
			this.convert = new System.Windows.Forms.Button();
			this.filepathTextBox = new System.Windows.Forms.TextBox();
			this.status = new System.Windows.Forms.Label();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SuspendLayout();
			// 
			// browsePDF
			// 
			this.browsePDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.browsePDF.Location = new System.Drawing.Point(213, 37);
			this.browsePDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.browsePDF.Name = "browsePDF";
			this.browsePDF.Size = new System.Drawing.Size(115, 37);
			this.browsePDF.TabIndex = 0;
			this.browsePDF.Text = "Browse...";
			this.browsePDF.UseVisualStyleBackColor = true;
			this.browsePDF.Click += new System.EventHandler(this.browsePDF_Click);
			// 
			// browsePDFLabel
			// 
			this.browsePDFLabel.AutoSize = true;
			this.browsePDFLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.browsePDFLabel.Location = new System.Drawing.Point(36, 46);
			this.browsePDFLabel.Name = "browsePDFLabel";
			this.browsePDFLabel.Size = new System.Drawing.Size(154, 20);
			this.browsePDFLabel.TabIndex = 1;
			this.browsePDFLabel.Text = "Select a statement:";
			// 
			// browsePDFDialog
			// 
			this.browsePDFDialog.FileName = "openFileDialog1";
			this.browsePDFDialog.Filter = "PDF Files (*.pdf)|*.pdf";
			// 
			// convert
			// 
			this.convert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.convert.Location = new System.Drawing.Point(40, 146);
			this.convert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.convert.Name = "convert";
			this.convert.Size = new System.Drawing.Size(100, 37);
			this.convert.TabIndex = 2;
			this.convert.Text = "Convert!";
			this.convert.UseVisualStyleBackColor = true;
			this.convert.Click += new System.EventHandler(this.convert_Click);
			// 
			// filepathTextBox
			// 
			this.filepathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.filepathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.filepathTextBox.Location = new System.Drawing.Point(40, 94);
			this.filepathTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.filepathTextBox.Name = "filepathTextBox";
			this.filepathTextBox.ReadOnly = true;
			this.filepathTextBox.Size = new System.Drawing.Size(407, 26);
			this.filepathTextBox.TabIndex = 3;
			// 
			// status
			// 
			this.status.AutoSize = true;
			this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.status.Location = new System.Drawing.Point(36, 214);
			this.status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(199, 20);
			this.status.TabIndex = 4;
			this.status.Text = "Status: Waiting for PDF...";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(67, 4);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(472, 276);
			this.Controls.Add(this.status);
			this.Controls.Add(this.filepathTextBox);
			this.Controls.Add(this.convert);
			this.Controls.Add(this.browsePDFLabel);
			this.Controls.Add(this.browsePDF);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.RightToLeftLayout = true;
			this.Text = "Statement Analyser";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button browsePDF;
		private System.Windows.Forms.Label browsePDFLabel;
		private System.Windows.Forms.OpenFileDialog browsePDFDialog;
		private System.Windows.Forms.Button convert;
		private System.Windows.Forms.TextBox filepathTextBox;
		private System.Windows.Forms.Label status;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	}
}

