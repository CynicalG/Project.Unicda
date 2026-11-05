using CRUD_LOGIN_PEOPLE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_LOGIN_PEOPLE
{
    public partial class FormLogin : Form
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUD_PERSONAS;Integrated Security=True;TrustServerCertificate=True;";

        public FormLogin()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            using var db = new AppDbContext(optionsBuilder.Options);

            var user = db.Users.FirstOrDefault(u =>
                u.Username == txtUsername.Text && u.Password == txtPassword.Text);

            if (user != null)
            {
                MessageBox.Show($"¡Bienvenido {user.Username}!");
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void FormLogin_Load(object sender, EventArgs e) { }

        private void loginLabel_Click(object sender, EventArgs e) { }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormLogin
            // 
            this.ClientSize = new System.Drawing.Size(744, 261);
            this.Name = "FormLogin";
            this.Load += new System.EventHandler(this.FormLogin_Load_1);
            this.ResumeLayout(false);

        }

        private void FormLogin_Load_1(object sender, EventArgs e)
        {

        }
    }
}
