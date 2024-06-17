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
    public partial class UpdateBook : Form
    {
        string path = "";
        int bookId;
        Thread th;
        LibraryService _libraryService;
        AuthService _authService;
        public UpdateBook(Book book, LibraryService libraryService, AuthService authService)
        {
            InitializeComponent();
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            path = book.Path;
            _libraryService = libraryService;
            bookId = book.Id;
            _authService = authService;
        }

        private void SaveBookBtn_Click(object sender, EventArgs e)
        {
            string Title = txtTitle.Text;
            string Author = txtAuthor.Text;

            _libraryService.UpadateBook(bookId, Title, Author, path);

            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files|*.pdf";
                openFileDialog.Title = "Select a PDF File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                }
            }
        }
        private void openNewForm()
        {
            Application.Run(new MainForm(_authService));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
    }
}
