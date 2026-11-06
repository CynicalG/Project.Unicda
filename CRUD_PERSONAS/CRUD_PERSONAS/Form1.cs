using CRUD_PERSONAS.Models;
using CRUD_PERSONAS.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PERSONAS
{
    public partial class Form1 : Form
    {
        Datos datos = new Datos();
        Persona persona = new Persona();
        int id = 0;

        public Form1()
        {
            InitializeComponent();


            dgvPersonas.AutoGenerateColumns = false;
            colId.DataPropertyName = "Id";
            colNombre.DataPropertyName = "Nombre";
            colApellido.DataPropertyName = "Apellido";
            colCorreo.DataPropertyName = "Correo";


            txtId.ReadOnly = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void CargarGrid()
        {
            var ls = datos.Read();
            dgvPersonas.DataSource = ls;
        }

        private void CargarDatos()
        {
            persona.Id = id;
            persona.Nombre = txtNombre.Text;
            persona.Apellido = txtApellido.Text;
            persona.Correo = txtCorreo.Text;
        }

        private void Limpiar()
        {
            id = 0;
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtNombre.Focus();
            CargarGrid();
        }


        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Por favor, llene todos los campos antes de guardar.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarDatos();
            datos.Create(persona);
            MessageBox.Show("Registro guardado");
            Limpiar();
        }


        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (id > 0)
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    MessageBox.Show("Los campos no pueden quedar vacíos.");
                    return;
                }

                CargarDatos();
                datos.Update(persona);
                MessageBox.Show("Registro actualizado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro haciendo doble clic en la tabla o buscarlo por ID.");
            }
        }


        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (id > 0)
            {
                datos.Delete(id);
                MessageBox.Show("Registro eliminado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro haciendo doble clic en la tabla o buscarlo por ID.");
            }
        }


        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            Limpiar();
        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID para buscar.");
                return;
            }

            int idBuscar = Convert.ToInt32(txtId.Text);
            var lista = datos.Read();
            var personaEncontrada = lista.FirstOrDefault(p => p.Id == idBuscar);

            if (personaEncontrada != null)
            {

                id = personaEncontrada.Id;

                txtNombre.Text = personaEncontrada.Nombre;
                txtApellido.Text = personaEncontrada.Apellido;
                txtCorreo.Text = personaEncontrada.Correo;
            }
            else
            {
                MessageBox.Show("No se encontró ninguna persona con ese ID.");
            }
        }


        private void dgvPersonas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                id = Convert.ToInt32(dgvPersonas.CurrentRow.Cells["colId"].Value);
                txtId.Text = id.ToString();
                txtNombre.Text = dgvPersonas.CurrentRow.Cells["colNombre"].Value.ToString();
                txtApellido.Text = dgvPersonas.CurrentRow.Cells["colApellido"].Value.ToString();
                txtCorreo.Text = dgvPersonas.CurrentRow.Cells["colCorreo"].Value.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void labelNombre_Click(object sender, EventArgs e) { }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            FormInvoice invoiceForm = new FormInvoice();
            invoiceForm.ShowDialog();
        }

        private void dgvPersonas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {

            FormUsers usersForm = new FormUsers();
            usersForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            FormReports reportForm = new FormReports();
            reportForm.ShowDialog();
        }
    }
}