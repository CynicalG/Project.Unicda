using CRUD_PERSONAS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PERSONAS
{
    public partial class FormLogin : Form
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUD_PERSONAS;Integrated Security=True;TrustServerCertificate=True;";


        public static int CurrentUserId = 0;
        public static string CurrentUserName = "";
        public static string CurrentShift = "";
        public static string CurrentRol = "";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            using var db = new AppDbContext(optionsBuilder.Options);


            var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text);

            if (user != null)
            {

                CurrentUserId = user.Id;
                CurrentUserName = user.Username;
                CurrentShift = user.Shift;
                CurrentRol = user.Role;

                this.Hide(); // Ocultamos el Login

                // ESCENARIO 1: Si es Administrador
                if (user.Role == "Admin")
                {
                    MessageBox.Show($"Bienvenido {user.Username}");
                    Form1 adminForm = new Form1(); // Panel de Admin
                    adminForm.ShowDialog();
                }

                else if (user.Username == "cajero")
                {
                    FormSelectCashier selectForm = new FormSelectCashier();
                    selectForm.ShowDialog();
                }

                else if (user.Role == "Cajero")
                {
                    MessageBox.Show($"Bienvenido {user.Username}\nTurno: {user.Shift}");
                    FormInvoice posForm = new FormInvoice(); // Va directo al POS
                    posForm.ShowDialog();
                }

                this.Close(); // Al cerrar la ventana que se abrió, se cierra todo
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void FormLogin_Load(object sender, EventArgs e) { }
        private void loginLabel_Click(object sender, EventArgs e) { }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}