using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProjectLibrary
{
    public partial class BookContentForm : Form
    {
        private Button backButton;
        Thread th;
        AuthService _authService;
        public BookContentForm(string pdfPath, AuthService authService)
        {
            InitializeComponent();

            _authService = authService;
            backButton = new Button();
            backButton.Text = "Назад";
            backButton.Dock = DockStyle.Top;
            backButton.Click += BackButton_Click;

            pdfViewer1.Dock = DockStyle.Fill;


            this.Controls.Add(pdfViewer1);
            this.Text = "Просмотр книги";
            this.WindowState = FormWindowState.Maximized;

            LoadPdf(pdfPath);
        }

        private void BookContentForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadPdf(string pdfPath)
        {
            try
            {
                var document = PdfDocument.Load(pdfPath);
                pdfViewer1.Document = document;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить PDF-файл: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm()
        {
            Application.Run(new MainForm(_authService));
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
