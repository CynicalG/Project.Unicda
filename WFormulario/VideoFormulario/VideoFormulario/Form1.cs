using System;
using System.Data;
using System.Windows.Forms;
using VideoFormulario.Datos;
using VideoFormulario.Modelo;

namespace VideoFormulario
{
    public partial class Form1 : Form
    {
        DataTable tabla;
        Usuario dato = new Usuario();

        public Form1()
        {
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            tabla = new DataTable();

            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Edad");

            grilla.DataSource = tabla;
        }

        private void Guardar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Escribe un nombre.");
                txtNombre.Focus();
                return;
            }

            if (!int.TryParse(txtEdad.Text, out int edad) || edad < 0)
            {
                MessageBox.Show("La edad debe ser un número válido.");
                txtEdad.Focus();
                return;
            }

            UsuarioModel modelo = new UsuarioModel
            {
                Nombre = txtNombre.Text,
                Edad = edad
            };

            dato.Guardar(modelo);

            Consultar();
            Limpiar();

            MessageBox.Show("Usuario guardado correctamente.");
        }

        private void Consultar()
        {
            tabla.Rows.Clear();

            foreach (var item in dato.Consultar())
            {
                tabla.Rows.Add(item.Nombre, item.Edad);
            }
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtEdad.Clear();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            dato.Limpiar();
            tabla.Rows.Clear();

            MessageBox.Show("Datos eliminados correctamente.");
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

