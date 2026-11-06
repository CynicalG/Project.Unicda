using CRUD_PERSONAS.Data;
using CRUD_PERSONAS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PERSONAS
{
    public partial class FormUsers : Form
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUD_PERSONAS;Integrated Security=True;TrustServerCertificate=True;";

        public FormUsers()
        {
            InitializeComponent();
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            if (cmbShift.Items.Count == 0)
            {
                cmbShift.Items.Add("Mañana");
                cmbShift.Items.Add("Tarde");
                cmbShift.Items.Add("Noche");
            }
            CargarCajeros();
        }

        private void CargarCajeros()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            using var db = new AppDbContext(optionsBuilder.Options);

            dgvUsers.DataSource = db.Users.Where(u => u.Role == "Cajero").ToList();
        }

        // ESTE ES EL MÉTODO QUE TIENE LA LÓGICA
        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewUser.Text) || string.IsNullOrWhiteSpace(txtNewPass.Text) || cmbShift.SelectedItem == null)
            {
                MessageBox.Show("Llene todos los campos para crear el cajero.");
                return;
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            using var db = new AppDbContext(optionsBuilder.Options);

            if (db.Users.Any(u => u.Username == txtNewUser.Text))
            {
                MessageBox.Show("Ese nombre de usuario ya existe. Elija otro.");
                return;
            }

            var nuevoCajero = new User
            {
                Username = txtNewUser.Text,
                Password = txtNewPass.Text,
                Role = "Cajero",
                Shift = cmbShift.SelectedItem.ToString()
            };

            db.Users.Add(nuevoCajero);
            db.SaveChanges();

            MessageBox.Show("Cajero creado con éxito.");

            txtNewUser.Clear();
            txtNewPass.Clear();
            cmbShift.SelectedIndex = -1;
            CargarCajeros();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vacío por ahora
        }
    }
}