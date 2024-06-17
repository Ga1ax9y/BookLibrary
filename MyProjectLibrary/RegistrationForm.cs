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
    public partial class RegistrationForm : Form
    {
        private AuthService _authService;
        Thread th;
        public RegistrationForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            th = new Thread(openLogForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmationPassword = txtPasswordConfirmation.Text;

            if (username.Length >= 8 && password.Length >= 8)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both login and password.");
                    return;
                }

                if (password == confirmationPassword)
                {
                    _authService.Registration(username, password);
                    th = new Thread(openNewForm);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают!");
                    txtPasswordConfirmation.Clear();
                    txtPassword.Clear();
                }
            }
            else
            {
                MessageBox.Show("Длина логина и пароля должна быть более 8 символов!");
                txtPassword.Clear();
                txtUsername.Clear();
            }
        }
        private void openNewForm()
        {
            Application.Run(new MainForm(_authService));
        }
        private void openLogForm()
        {
            Application.Run(new LoginForm(_authService));
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '\0';
            txtPasswordConfirmation.PasswordChar = '\0';
        }
    }
}
