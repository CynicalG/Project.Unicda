using System;
using CRUD_PEOPLE.Models;
using CRUD_PEOPLE.Services;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PEOPLE
{
    public partial class Form1 : Form
    {
        Datos datos = new Datos();
        Persona persona = new Persona();
        int id = 0;

        public Form1()
        {
            InitializeComponent();

            // Evitar columnas duplicadas
            dgvPersonas.AutoGenerateColumns = false;
            colId.DataPropertyName = "Id";
            colNombre.DataPropertyName = "Nombre";
            colApellido.DataPropertyName = "Apellido";
            colCorreo.DataPropertyName = "Correo";

            // EL ID VUELVE A SER EDITABLE PARA PODER BUSCAR
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

        // BOTÓN GUARDAR (Con validación)
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        // BOTÓN ACTUALIZAR
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {

        }

        // BOTÓN LIMPIAR
        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            Limpiar();
        }

        // DOBLE CLIC EN LA TABLA
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

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}