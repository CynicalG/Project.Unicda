using CRUD_PERSONAS.Data;
using CRUD_PERSONAS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PERSONAS
{
    public partial class FormSelectCashier : Form
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUD_PERSONAS;Integrated Security=True;TrustServerCertificate=True;";

        public FormSelectCashier()
        {
            InitializeComponent();
        }

        private void FormSelectCashier_Load(object sender, EventArgs e)
        {
            CargarCajeros();
        }

        private void CargarCajeros()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            using var db = new AppDbContext(optionsBuilder.Options);

            // Buscamos solo a los usuarios que sean cajeros
            var cajeros = db.Users.Where(u => u.Role == "Cajero").ToList();

            if (cajeros.Count == 0)
            {
                MessageBox.Show("No hay cajeros registrados en el sistema. Pídele al Admin que cree uno.");
                this.Close();
                return;
            }

            cmbCashiers.DataSource = cajeros;
            cmbCashiers.DisplayMember = "Username"; // Muestra "cajero1", "cajero2", etc.
        }

        private void btnStartShift_Click(object sender, EventArgs e)
        {
            if (cmbCashiers.SelectedItem == null) return;

            var cajeroSeleccionado = (User)cmbCashiers.SelectedItem;

            // Guardamos los datos globales en el FormLogin
            FormLogin.CurrentUserId = cajeroSeleccionado.Id;
            FormLogin.CurrentUserName = cajeroSeleccionado.Username;
            FormLogin.CurrentShift = cajeroSeleccionado.Shift;
            FormLogin.CurrentRol = "Cajero";

            MessageBox.Show($"Turno Iniciado: {cajeroSeleccionado.Username} ({cajeroSeleccionado.Shift})");

            this.Hide();
            FormInvoice posForm = new FormInvoice(); // Abre el Punto de Venta
            posForm.ShowDialog();

            this.Close();
        }
    }
}