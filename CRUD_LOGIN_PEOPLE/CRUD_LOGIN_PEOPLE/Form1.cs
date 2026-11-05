using CRUD_LOGIN_PEOPLE.Models;
using CRUD_LOGIN_PEOPLE.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_LOGIN_PEOPLE
{
    public partial class Form1 : Form
    {
        Datos datos = new Datos();
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
            dgvPersonas.DataSource = datos.Read();
        }

        private Persona CargarDatos()
        {
            return new Persona
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Correo = txtCorreo.Text.Trim()
            };
        }

        private bool CamposValidos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Campos vacíos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
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
            if (!CamposValidos()) return;

            id = 0;
            datos.Create(CargarDatos());
            MessageBox.Show("Registro guardado");
            Limpiar();
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (id <= 0)
            {
                MessageBox.Show("Debe seleccionar un registro haciendo doble clic en la tabla o buscarlo por ID.");
                return;
            }

            if (!CamposValidos()) return;

            datos.Update(CargarDatos());
            MessageBox.Show("Registro actualizado");
            Limpiar();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (id <= 0)
            {
                MessageBox.Show("Debe seleccionar un registro haciendo doble clic en la tabla o buscarlo por ID.");
                return;
            }

            var resultado = MessageBox.Show("¿Seguro que desea eliminar este registro?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes) return;

            datos.Delete(id);
            MessageBox.Show("Registro eliminado");
            Limpiar();
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int idBuscar))
            {
                MessageBox.Show("Ingrese un ID numérico válido para buscar.");
                return;
            }

            var personaEncontrada = datos.Read().FirstOrDefault(p => p.Id == idBuscar);

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
            if (e.RowIndex < 0) return;

            var row = dgvPersonas.Rows[e.RowIndex];

            id = Convert.ToInt32(row.Cells["colId"].Value);
            txtId.Text = id.ToString();
            txtNombre.Text = row.Cells["colNombre"].Value?.ToString() ?? "";
            txtApellido.Text = row.Cells["colApellido"].Value?.ToString() ?? "";
            txtCorreo.Text = row.Cells["colCorreo"].Value?.ToString() ?? "";
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            using (var invoiceForm = new FormInvoice())
            {
                invoiceForm.ShowDialog();
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void labelNombre_Click(object sender, EventArgs e) { }
        private void dgvPersonas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}