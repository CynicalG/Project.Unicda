using CRUD_LOGIN_PEOPLE.Data;
using CRUD_LOGIN_PEOPLE.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CRUD_LOGIN_PEOPLE
{
    public partial class FormInvoice : Form
    {
        private readonly string _connectionString =
            "Server=localhost;Database=CRUD_LOGIN_PEOPLE;Integrated Security=True;TrustServerCertificate=True;";

        private List<InvoiceDetail> cart = new List<InvoiceDetail>();
        private decimal totalAmount = 0;

        public FormInvoice()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private AppDbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new AppDbContext(optionsBuilder.Options);
        }

        private void FormInvoice_Load(object sender, EventArgs e)
        {
            using var db = GetDbContext();

            var products = db.Products.ToList();

            cmbProducts.DataSource = products;
            cmbProducts.DisplayMember = "Name";
            cmbProducts.ValueMember = "Id";

            var empleados = db.Personas.ToList();

            cmbEmployee.DataSource = empleados;
            cmbEmployee.DisplayMember = "NombreCompleto";
            cmbEmployee.ValueMember = "Id";

            cmbEmployee.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEmployee.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtQuantity.Text))
                return;

            var selectedProduct = (Product)cmbProducts.SelectedItem;

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.");
                return;
            }

            int cantidadEnCarrito = cart
                .Where(c => c.ProductId == selectedProduct.Id)
                .Sum(c => c.Quantity);

            if (quantity + cantidadEnCarrito > selectedProduct.Stock)
            {
                MessageBox.Show(
                    $"No hay suficiente stock. Solo quedan {selectedProduct.Stock - cantidadEnCarrito} unidades disponibles.",
                    "Stock Insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var detail = new InvoiceDetail
            {
                ProductId = selectedProduct.Id,
                Product = selectedProduct,
                Quantity = quantity,
                UnitPrice = selectedProduct.Price,
                SubTotal = selectedProduct.Price * quantity
            };

            cart.Add(detail);

            RefreshCartGrid();
        }

        private void RefreshCartGrid()
        {
            var cartDisplay = cart.Select(c => new
            {
                Producto = c.Product.Name,
                Cantidad = c.Quantity,
                Precio = c.UnitPrice,
                Total = c.SubTotal
            }).ToList();

            dgvCart.DataSource = null;
            dgvCart.DataSource = cartDisplay;

            if (dgvCart.Columns["Producto"] != null)
                dgvCart.Columns["Producto"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;

            totalAmount = cart.Sum(c => c.SubTotal);

            lblTotal.Text = $"Total: ${totalAmount:F2}";
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.");
                return;
            }

            if (cmbEmployee.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un empleado para asignar los equipos.");
                return;
            }

            var empleadoSeleccionado = (Persona)cmbEmployee.SelectedItem;

            string nombreCompletoEmpleado =
                $"{empleadoSeleccionado.Nombre} {empleadoSeleccionado.Apellido}";

            try
            {
                using var db = GetDbContext();
                using var transaction = db.Database.BeginTransaction();

                var invoice = new Invoice
                {
                    Date = DateTime.Now,
                    TotalAmount = totalAmount,
                    UserId = 1,
                    CustomerName = nombreCompletoEmpleado
                };

                db.Invoices.Add(invoice);
                db.SaveChanges();

                foreach (var item in cart)
                {
                    var productoBD = db.Products.Find(item.ProductId);

                    if (productoBD == null || productoBD.Stock < item.Quantity)
                    {
                        throw new Exception(
                            $"Stock insuficiente para el producto {item.Product.Name}.");
                    }

                    var dbDetail = new InvoiceDetail
                    {
                        InvoiceId = invoice.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        SubTotal = item.SubTotal
                    };

                    db.InvoiceDetails.Add(dbDetail);

                    productoBD.Stock -= item.Quantity;
                }

                db.SaveChanges();
                transaction.Commit();

                MessageBox.Show(
                    $"Asignación #{invoice.Id} guardada con éxito para {nombreCompletoEmpleado}.");

                DialogResult result = MessageBox.Show(
                    "¿Desea guardar el reporte ahora?",
                    "Guardar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    PrintToTxt();

                cart.Clear();
                cmbEmployee.SelectedIndex = -1;
                RefreshCartGrid();

                FormInvoice_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void FormInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                DialogResult result = MessageBox.Show(
                    "¿Desea guardar como TXT?\n\n(Pulse 'No' para generar PDF)",
                    "Imprimir (F5)",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    PrintToTxt();
                else if (result == DialogResult.No)
                    PrintToPdf();
            }
        }

        private void PrintToTxt()
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Agregue un equipo al carrito primero.");
                return;
            }

            string nombreEmpleado = "Sin empleado";

            if (cmbEmployee.SelectedItem != null)
            {
                var emp = (Persona)cmbEmployee.SelectedItem;
                nombreEmpleado = $"{emp.Nombre} {emp.Apellido}";
            }

            System.Text.StringBuilder receipt =
                new System.Text.StringBuilder();

            receipt.AppendLine("==========================================================================");
            receipt.AppendLine("                             MR. ROBOT S.A.                               ");
            receipt.AppendLine("                        DEPARTAMENTO DE IT / SOPORTE                      ");
            receipt.AppendLine("==========================================================================");
            receipt.AppendLine($"Fecha de Asignación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            receipt.AppendLine($"Empleado Asignado:   {nombreEmpleado}");
            receipt.AppendLine("--------------------------------------------------------------------------");

            receipt.AppendLine(
                "Equipo / Artículo".PadRight(25) +
                "Cant.".PadLeft(6) +
                "Val. Unitario".PadLeft(16) +
                "Val. Total".PadLeft(16));

            receipt.AppendLine("--------------------------------------------------------------------------");

            foreach (var item in cart)
            {
                string nombreProducto = item.Product.Name;

                if (nombreProducto.Length > 24)
                    nombreProducto = nombreProducto.Substring(0, 21) + "...";

                string qtyStr = item.Quantity.ToString().PadLeft(6);
                string priceStr = item.UnitPrice.ToString("N2").PadLeft(16);
                string totalStr = item.SubTotal.ToString("N2").PadLeft(16);

                receipt.AppendLine(
                    nombreProducto.PadRight(25) +
                    qtyStr +
                    priceStr +
                    totalStr);
            }

            receipt.AppendLine("--------------------------------------------------------------------------");

            receipt.AppendLine(
                "VALOR TOTAL DE EQUIPOS:".PadRight(36) +
                totalAmount.ToString("N2").PadLeft(25));

            receipt.AppendLine("==========================================================================");
            receipt.AppendLine();
            receipt.AppendLine("                       FIRMA DE RECIBIDO CONFORME                         ");
            receipt.AppendLine("                  _____________________________                           ");
            receipt.AppendLine($"                  {nombreEmpleado}");
            receipt.AppendLine("                  (Empleado)                                              ");
            receipt.AppendLine("==========================================================================");

            string customFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Asignaciones_Equipos");

            Directory.CreateDirectory(customFolder);

            string filePath = Path.Combine(
                customFolder,
                $"Asignacion_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            File.WriteAllText(filePath, receipt.ToString());

            MessageBox.Show(
                $"Reporte TXT guardado en:\n{filePath}",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                private void PrintToPdf()
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Agregue un equipo al carrito primero.");
                return;
            }

            string nombreEmpleado = "Sin empleado";

            if (cmbEmployee.SelectedItem != null)
            {
                var emp = (Persona)cmbEmployee.SelectedItem;
                nombreEmpleado = $"{emp.Nombre} {emp.Apellido}";
            }

            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                string customFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Asignaciones_Equipos");

                Directory.CreateDirectory(customFolder);

                string filePath = Path.Combine(
                    customFolder,
                    $"Asignacion_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);

                        page.Header().Element(header =>
                        {
                            header.Column(col =>
                            {
                                col.Item().Text("MR. ROBOT S.A.")
                                    .FontSize(20)
                                    .SemiBold()
                                    .FontColor(Colors.Blue.Darken2);

                                col.Item().Text("REPORTE DE ASIGNACIÓN DE EQUIPOS")
                                    .FontSize(14)
                                    .FontColor(Colors.Grey.Darken1);

                                col.Item().PaddingTop(10)
                                    .Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");

                                col.Item().Text($"Empleado: {nombreEmpleado}")
                                    .SemiBold();
                            });
                        });

                        page.Content().PaddingVertical(20).Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(2);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(1).Padding(5)
                                        .Text("Equipo / Artículo").SemiBold();

                                    header.Cell().BorderBottom(1).Padding(5)
                                        .AlignRight().Text("Cant.").SemiBold();

                                    header.Cell().BorderBottom(1).Padding(5)
                                        .AlignRight().Text("Valor Unit.").SemiBold();

                                    header.Cell().BorderBottom(1).Padding(5)
                                        .AlignRight().Text("Valor Total").SemiBold();
                                });

                                foreach (var item in cart)
                                {
                                    table.Cell().BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .Text(item.Product.Name);

                                    table.Cell().BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignRight()
                                        .Text(item.Quantity.ToString());

                                    table.Cell().BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignRight()
                                        .Text($"${item.UnitPrice:F2}");

                                    table.Cell().BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignRight()
                                        .Text($"${item.SubTotal:F2}");
                                }
                            });

                            col.Item().AlignRight().PaddingTop(10)
                                .Text($"VALOR TOTAL: ${totalAmount:F2}")
                                .FontSize(14)
                                .SemiBold()
                                .FontColor(Colors.Blue.Darken2);

                            col.Item().PaddingTop(50)
                                .Text("_____________________________________");

                            col.Item().Text(nombreEmpleado);

                            col.Item().Text("Firma del Empleado")
                                .FontColor(Colors.Grey.Medium);
                        });
                    });
                }).GeneratePdf(filePath);

                MessageBox.Show(
                    $"Reporte PDF generado con éxito en:\n{filePath}",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
            }
        }

        private void btnPrintTxt_Click(object sender, EventArgs e)
        {
            PrintToTxt();
        }

        private void btnPrintPdf_Click_1(object sender, EventArgs e)
        {
            PrintToPdf();
        }

        private void FormInvoice_Load_1(object sender, EventArgs e) { }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void lblTotal_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e) { }

        private void txtQuantity_TextChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }
    }
}