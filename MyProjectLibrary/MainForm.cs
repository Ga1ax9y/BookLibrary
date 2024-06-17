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
    public partial class MainForm : Form
    {
        Thread th;
        AuthService _authService;
        LibraryService _libraryService;
        List<Book> UserBooks = new List<Book>();
        private bool pageLoad = true;
        string path;
        Book EdBook;
        public MainForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            _libraryService = new LibraryService(_authService.CurrentUser.Library);
            LoadBooksList();

            labelLogin.Text = "Пользователь: " + _authService.CurrentUser.Login;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns.Add("Title", "Название");
            dataGridView1.Columns.Add("Author", "Автор");
            dataGridView1.Columns.Add("Date", "Дата");
            dataGridView1.Columns.Add("Path", "Путь");
            dataGridView1.Columns["Path"].Visible = false;

            DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
            viewButtonColumn.Name = "View";
            viewButtonColumn.HeaderText = "Просмотр";
            viewButtonColumn.Text = "Просмотр";
            viewButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(viewButtonColumn);

            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.Name = "Edit";
            editButtonColumn.HeaderText = "Изменить";
            editButtonColumn.Text = "Изменить";
            editButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Удалить";
            deleteButtonColumn.Text = "Удалить";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.CellClick += DataGridViewBooks_CellClick;
            dataGridView1.Columns.Add(deleteButtonColumn);


            RefreshBookList(_authService.CurrentUser.Library.Books);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //books = Database.Loadbooks()
            th = new Thread(openAddBookFormForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openAddBookFormForm()
        {
            Application.Run(new AddBookForm(_authService));
        }
        private void openContentForm()
        {
            Application.Run(new BookContentForm(path,_authService));
        }
        private void LoadBooksList()
        {
            UserBooks = _libraryService.GetUserBooks(_authService.CurrentUser.Id);
            _authService.CurrentUser.Library.Books = UserBooks; 


        }


        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void comboBox_IndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                _libraryService.SortByDate();

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                _libraryService.SortByName();

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                _libraryService.SortByAuthor();

            }
            RefreshBookList(_authService.CurrentUser.Library.Books);
        }
        private void RefreshBookList(List<Book> books)
        {
            LoadBooksList();
            dataGridView1.Rows.Clear();

            foreach (var book in books)
            {
                dataGridView1.Rows.Add(book.Id, book.Title, book.Author, book.Date, book.Path, "Просмотр", "Удалить");
            }
        }
        private void DataGridViewBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (e.ColumnIndex == dataGridView1.Columns["View"].Index)
                {
                    path = row.Cells["Path"].Value.ToString();
                    th = new Thread(openContentForm);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    this.Close();
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
                {
                    int bookId = (int)row.Cells["Id"].Value;
                    var result = MessageBox.Show("Вы уверены, что хотите изменить эту книгу?", "Подтверждение изменения", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        EdBook = _libraryService.FindBookById(bookId);
                        th = new Thread(openUpdateForm);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();
                        this.Close();
                    }
                }

                else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
                {
                    int bookId = (int)row.Cells["Id"].Value;
                    var result = MessageBox.Show("Вы уверены, что хотите удалить эту книгу?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        _libraryService.RemoveBook(bookId);
                        RefreshBookList(_libraryService.GetUserBooks(_authService.CurrentUser.Id));
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _authService.Logout();
            th = new Thread(openLogForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();

        }
        private void openLogForm()
        {
            Application.Run(new LoginForm(_authService));
        }
        private void openUpdateForm()
        {
            Application.Run(new UpdateBook(EdBook,_libraryService,_authService));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text_for_search = textBox1.Text;
            UserBooks = Database.GetUserBooks(_authService.CurrentUser.Id);
            if (!string.IsNullOrEmpty(text_for_search))
            {
                UserBooks = UserBooks.Where(b => b.Title.StartsWith(text_for_search)).ToList();
            }
            RefreshBookList(UserBooks);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
