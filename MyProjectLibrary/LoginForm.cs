using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyProjectLibrary
{
    public partial class LoginForm : Form
    {
        AuthService _authService;
        Thread th;
        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
        }
        public LoginForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userlogin = txtUsername.Text;
            string userpassword = txtPassword.Text;
            if (_authService.Login(userlogin, userpassword))
            {
                _authService.Login(userlogin, userpassword);
                th = new Thread(openNewForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }

        private void openNewForm()
        {
            Application.Run(new MainForm(_authService));
        }
        private void openRegForm()
        {
            Application.Run(new RegistrationForm(_authService));
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            th = new Thread(openRegForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();

        }
    }
}
