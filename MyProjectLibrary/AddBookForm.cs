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
    public partial class AddBookForm : Form
    {
        string filepath = "";
        AuthService _authService;
        Thread th;
        LibraryService _libraryService;
        public AddBookForm()
        {
            InitializeComponent();
            
            
        }
        public AddBookForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            _libraryService = new LibraryService(_authService.CurrentUser.Library);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files|*.pdf"; 
                openFileDialog.Title = "Select a PDF File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filepath = openFileDialog.FileName;
                }
            }
        }

        private void AddBookBtn_Click(object sender, EventArgs e)
        {
            string Title = txtTitle.Text;
            string Author = txtAuthor.Text;
            if (Title == string.Empty  || Author == string.Empty)
            {
                MessageBox.Show("Название или автор не могут быть пустыми");
                return;
            }
            string time = (DateTime.Now).ToString();
            int userId = _authService.CurrentUser.Id;
            if (filepath == "")
            {
                MessageBox.Show("Укажите верный путь");
                return;
            }
            else
            {
                _libraryService.AddBook(Title, filepath, userId, time, Author);
                th = new Thread(openNewForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();
            }
            
        }
        private void openNewForm()
        {
            Application.Run(new MainForm(_authService));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
    }
}
