using CRUD_PERSONAS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_PERSONAS
{
    public partial class FormReports : Form
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUD_PERSONAS;Integrated Security=True;TrustServerCertificate=True;";

        public FormReports()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un reporte de la lista.");
                return;
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            using var db = new AppDbContext(optionsBuilder.Options);

            string reporteSeleccionado = cmbReportType.SelectedItem.ToString();

            switch (reporteSeleccionado)
            {
                case "Cuadre del Día":
                    var cuadre = db.Invoices
                        .Where(i => i.Date.Date == DateTime.Today)
                        .GroupBy(i => 1)
                        .Select(g => new
                        {
                            Fecha = DateTime.Today.ToShortDateString(),
                            TotalFacturas = g.Count(),
                            SubTotal = g.Sum(i => i.SubTotal),
                            TotalITBIS = g.Sum(i => i.ItbisAmount),
                            TotalVentas = g.Sum(i => i.TotalToPay)
                        }).ToList();
                    dgvReports.DataSource = cuadre;
                    break;

                case "Ventas por Cajero":
                    var ventasCajero = db.Invoices
                        .GroupBy(i => i.UserId)
                        .Select(g => new
                        {
                            CajeroID = g.Key,
                            TotalVentas = g.Sum(i => i.TotalToPay),
                            CantidadFacturas = g.Count()
                        }).ToList();
                    dgvReports.DataSource = ventasCajero;
                    break;

                case "Inventario en Stock":
                    var inventario = db.Products
                        .Select(p => new
                        {
                            ID = p.Id,
                            Producto = p.Name,
                            Precio = p.Price,
                            Stock_Disponible = p.Stock
                        }).ToList();
                    dgvReports.DataSource = inventario;
                    break;

                case "Facturas Crédito/Efectivo":
                    var facturasTipo = db.Invoices
                        .GroupBy(i => i.PaymentType)
                        .Select(g => new
                        {
                            TipoPago = g.Key,
                            Cantidad = g.Count(),
                            MontoTotal = g.Sum(i => i.TotalToPay)
                        }).ToList();
                    dgvReports.DataSource = facturasTipo;
                    break;

                case "Top 5 Clientes":
                    var topClientes = db.Invoices
                        .GroupBy(i => i.CustomerName)
                        .Select(g => new
                        {
                            Cliente = g.Key,
                            TotalComprado = g.Sum(i => i.TotalToPay),
                            VecesQueCompro = g.Count()
                        })
                        .OrderByDescending(x => x.TotalComprado)
                        .Take(5)
                        .ToList();
                    dgvReports.DataSource = topClientes;
                    break;
            }

            // Llamamos al método para embellecer la tabla después de cargar cualquier reporte
            AjustarColumnas();
        }

        // ESTE MÉTODO CAMBIA LOS NOMBRES Y DISTRIBUYE LAS COLUMNAS
        private void AjustarColumnas()
        {
            // 1. Hacemos que las columnas ocupen todo el ancho de la pantalla
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. Cambiamos los nombres feos por nombres bonitos
            if (dgvReports.Columns["ID"] != null) dgvReports.Columns["ID"].HeaderText = "Código";
            if (dgvReports.Columns["Producto"] != null) dgvReports.Columns["Producto"].HeaderText = "Nombre del Producto";
            if (dgvReports.Columns["Precio"] != null) dgvReports.Columns["Precio"].HeaderText = "Precio (RD$)";
            if (dgvReports.Columns["Stock_Disponible"] != null) dgvReports.Columns["Stock_Disponible"].HeaderText = "Stock Disponible";

            if (dgvReports.Columns["TotalFacturas"] != null) dgvReports.Columns["TotalFacturas"].HeaderText = "Cant. Facturas";
            if (dgvReports.Columns["SubTotal"] != null) dgvReports.Columns["SubTotal"].HeaderText = "SubTotal (RD$)";
            if (dgvReports.Columns["TotalITBIS"] != null) dgvReports.Columns["TotalITBIS"].HeaderText = "Total ITBIS (RD$)";
            if (dgvReports.Columns["TotalVentas"] != null) dgvReports.Columns["TotalVentas"].HeaderText = "Total Ventas (RD$)";

            if (dgvReports.Columns["CajeroID"] != null) dgvReports.Columns["CajeroID"].HeaderText = "Código Cajero";
            if (dgvReports.Columns["CantidadFacturas"] != null) dgvReports.Columns["CantidadFacturas"].HeaderText = "Facturas Emitidas";

            if (dgvReports.Columns["TipoPago"] != null) dgvReports.Columns["TipoPago"].HeaderText = "Tipo de Pago";
            if (dgvReports.Columns["Cantidad"] != null) dgvReports.Columns["Cantidad"].HeaderText = "Cantidad";
            if (dgvReports.Columns["MontoTotal"] != null) dgvReports.Columns["MontoTotal"].HeaderText = "Monto Total (RD$)";

            if (dgvReports.Columns["Cliente"] != null) dgvReports.Columns["Cliente"].HeaderText = "Cliente";
            if (dgvReports.Columns["TotalComprado"] != null) dgvReports.Columns["TotalComprado"].HeaderText = "Total Comprado (RD$)";
            if (dgvReports.Columns["VecesQueCompro"] != null) dgvReports.Columns["VecesQueCompro"].HeaderText = "Veces que Compró";
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            // Vacío
        }

        private void dgvReports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vacío
        }
    }
}