namespace MyProjectLibrary
{
    partial class BookContentForm
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
            this.pdfViewer1 = new PdfiumViewer.PdfViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(57, 113);
            this.pdfViewer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(1088, 265);
            this.pdfViewer1.TabIndex = 1;
            this.pdfViewer1.Load += new System.EventHandler(this.pdfViewer1_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1586, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // BookContentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1847, 639);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pdfViewer1);
            this.Name = "BookContentForm";
            this.Text = "BookContentForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BookContentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PdfiumViewer.PdfViewer pdfViewer1;
        private System.Windows.Forms.Button button1;
    }
}