using CRUD_PERSONAS.Data;
using CRUD_PERSONAS.Models;
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

namespace CRUD_PERSONAS
{
    public partial class FormInvoice : Form
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUD_PERSONAS;Integrated Security=True;TrustServerCertificate=True;";
        private List<InvoiceDetail> cart = new List<InvoiceDetail>();

        private decimal subTotal = 0;
        private decimal itbisAmount = 0;
        private string numeroFacturaActual = "";
        private decimal totalToPay = 0;

        public FormInvoice()
        {
            InitializeComponent();
            this.KeyPreview = true;
            if (cmbPaymentType.Items.Count > 0) cmbPaymentType.SelectedIndex = 0;
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

            cmbProducts.DataSource = db.Products.ToList();
            cmbProducts.DisplayMember = "Name";
            cmbProducts.ValueMember = "Id";

            var clientes = db.Personas.ToList();
            cmbEmployee.DataSource = clientes;
            cmbEmployee.DisplayMember = "NombreCompleto";
            cmbEmployee.ValueMember = "Id";
            cmbEmployee.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEmployee.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem == null || string.IsNullOrWhiteSpace(txtQuantity.Text)) return;

            var selectedProduct = (Product)cmbProducts.SelectedItem;

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.");
                return;
            }

            if (quantity > selectedProduct.Stock)
            {
                MessageBox.Show($"No hay suficiente stock. Solo quedan {selectedProduct.Stock} unidades.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            selectedProduct.Stock -= quantity;
            RefreshCartGrid();
        }

        private void RefreshCartGrid()
        {
            var cartDisplay = cart.Select(c => new
            {
                Producto = c.Product?.Name ?? "Desconocido",
                Cantidad = c.Quantity,
                Precio = c.UnitPrice,
                Total = c.SubTotal
            }).ToList();

            dgvCart.DataSource = null;
            dgvCart.DataSource = cartDisplay;
            if (dgvCart.Columns["Producto"] != null) dgvCart.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            subTotal = cart.Sum(c => c.SubTotal);
            itbisAmount = subTotal * 0.18m;
            totalToPay = subTotal + itbisAmount;

            lblTotal.Text = $"SubTotal: ${subTotal:F2}\nITBIS (18%): ${itbisAmount:F2}\nTOTAL: ${totalToPay:F2}";

            CalcularDevuelta();
        }

        private void CalcularDevuelta()
        {
            if (cmbPaymentType.Text == "Efectivo" || cmbPaymentType.Text == "Contado")
            {
                if (decimal.TryParse(txtAmountPaid.Text, out decimal paid))
                {
                    decimal change = paid - totalToPay;
                    lblChange.Text = $"Devuelta: ${change:F2}";
                }
                else
                {
                    lblChange.Text = "Devuelta: $0.00";
                }
            }
            else
            {
                lblChange.Text = "Devuelta: N/A (Crédito)";
            }
        }

        private void txtAmountPaid_TextChanged(object sender, EventArgs e)
        {
            CalcularDevuelta();
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0) { MessageBox.Show("El carrito está vacío."); return; }
            if (cmbEmployee.SelectedItem == null) { MessageBox.Show("Seleccione un cliente."); return; }

            var clienteSeleccionado = (Persona)cmbEmployee.SelectedItem;
            string nombreCliente = clienteSeleccionado.NombreCompleto;

            decimal amountPaid = 0;
            decimal changeDue = 0;

            if (cmbPaymentType.Text == "Efectivo" || cmbPaymentType.Text == "Contado")
            {
                if (!decimal.TryParse(txtAmountPaid.Text, out amountPaid) || amountPaid < totalToPay)
                {
                    MessageBox.Show("El monto pagado es inválido o insuficiente.", "Error de Pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                changeDue = amountPaid - totalToPay;
            }

            try
            {
                using var db = GetDbContext();

                int nextId = db.Invoices.Any() ? db.Invoices.Max(i => i.Id) + 1 : 1;
                string invoiceNumber = nextId.ToString("D5");
                numeroFacturaActual = invoiceNumber;
                var invoice = new Invoice
                {
                    InvoiceNumber = invoiceNumber,
                    Date = DateTime.Now,
                    PaymentType = cmbPaymentType.Text,
                    SubTotal = subTotal,
                    ItbisAmount = itbisAmount,
                    TotalToPay = totalToPay,
                    AmountPaid = amountPaid,
                    ChangeDue = changeDue,
                    UserId = FormLogin.CurrentUserId,
                    CustomerName = nombreCliente
                };

                db.Invoices.Add(invoice);
                db.SaveChanges();

                foreach (var item in cart)
                {
                    var dbDetail = new InvoiceDetail
                    {
                        InvoiceId = invoice.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        SubTotal = item.SubTotal
                    };
                    db.InvoiceDetails.Add(dbDetail);

                    var productoBD = db.Products.Find(item.ProductId);
                    if (productoBD != null) productoBD.Stock -= item.Quantity;
                }

                db.SaveChanges();

                MessageBox.Show($"Factura #{invoiceNumber} generada.\nCajero: {FormLogin.CurrentUserName}", "Venta Exitosa");

                // PREGUNTAR SI DESEA IMPRIMIR
                DialogResult result = MessageBox.Show("¿Desea imprimir la factura ahora?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    PreguntarFormatoImpresion();
                }

                cart.Clear();
                txtAmountPaid.Clear();
                lblChange.Text = "Devuelta: $0.00";
                cmbEmployee.SelectedIndex = -1;
                RefreshCartGrid();
                FormInvoice_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        // BOTÓN "IMPRIMIR TICKET" - Pregunta si quieres PDF o TXT
        private void btnPrintTxt_Click(object sender, EventArgs e)
        {
            PreguntarFormatoImpresion();
        }

        // Lógica para preguntar el formato
        private void PreguntarFormatoImpresion()
        {
            if (cart.Count == 0) { MessageBox.Show("El carrito está vacío."); return; }

            DialogResult formato = MessageBox.Show("¿En qué formato desea guardar la factura?\n\n(Sí = PDF, No = TXT)", "Formato de Impresión", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (formato == DialogResult.Yes)
            {
                PrintToPdf();
            }
            else if (formato == DialogResult.No)
            {
                PrintToTxt();
            }
        }

        // ATAJO F5 - También pregunta
        private void FormInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                PreguntarFormatoImpresion();
            }
        }

        private void PrintToTxt()
        {
            if (cart.Count == 0) { MessageBox.Show("El carrito está vacío."); return; }

            string cliente = ((Persona)cmbEmployee.SelectedItem)?.NombreCompleto ?? "Cliente Final";

            System.Text.StringBuilder receipt = new System.Text.StringBuilder();

            receipt.AppendLine("==========================================================");
            receipt.AppendLine("                     MR. ROBOT S.A.                       ");
            receipt.AppendLine("                    RNC: 1-01-00000-1                     ");
            receipt.AppendLine("==========================================================");
            receipt.AppendLine($"FACTURA NO: {numeroFacturaActual}");
            receipt.AppendLine($"FECHA: {DateTime.Now:dd/MM/yyyy}   HORA: {DateTime.Now:HH:mm:ss}");
            receipt.AppendLine($"CAJERO: {FormLogin.CurrentUserName} (Turno: {FormLogin.CurrentShift})");
            receipt.AppendLine($"CLIENTE: {cliente}");
            receipt.AppendLine($"TIPO: {cmbPaymentType.Text.ToUpper()}");
            receipt.AppendLine("----------------------------------------------------------");
            receipt.AppendLine("Cant. Producto            Valor     Total");
            receipt.AppendLine("----------------------------------------------------------");

            foreach (var item in cart)
            {
                string nombre = item.Product?.Name ?? "Prod";
                if (nombre.Length > 15) nombre = nombre.Substring(0, 15);
                string cant = item.Quantity.ToString().PadRight(5);
                string valor = item.UnitPrice.ToString("N2").PadLeft(10);
                string total = item.SubTotal.ToString("N2").PadLeft(10);

                receipt.AppendLine($"{cant}{nombre.PadRight(18)}{valor}{total}");
            }

            receipt.AppendLine("----------------------------------------------------------");
            receipt.AppendLine($"SUBTOTAL:".PadRight(40) + subTotal.ToString("N2").PadLeft(15));
            receipt.AppendLine($"ITBIS (18%):".PadRight(40) + itbisAmount.ToString("N2").PadLeft(15));
            receipt.AppendLine($"TOTAL A PAGAR:".PadRight(40) + totalToPay.ToString("N2").PadLeft(15));

            if (cmbPaymentType.Text == "Efectivo" || cmbPaymentType.Text == "Contado")
            {
                receipt.AppendLine($"MONTO PAGADO:".PadRight(40) + txtAmountPaid.Text.PadLeft(15));
                receipt.AppendLine($"DEVUELTA:".PadRight(40) + lblChange.Text.Replace("Devuelta: $", "").PadLeft(15));
            }
            else
            {
                receipt.AppendLine($"REGISTRADO EN CUENTAS POR COBRAR".PadLeft(55));
            }

            receipt.AppendLine("==========================================================");
            receipt.AppendLine("               ¡GRACIAS POR SU COMPRA!                    ");
            receipt.AppendLine("==========================================================");

            string customFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Facturas_POS");
            Directory.CreateDirectory(customFolder);
            string filePath = Path.Combine(customFolder, $"Factura_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            File.WriteAllText(filePath, receipt.ToString());
            MessageBox.Show($"Factura TXT guardada en:\n{filePath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PrintToPdf()
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Facturas_PDF");
                Directory.CreateDirectory(folder);
                string path = Path.Combine(folder, $"Factura_{DateTime.Now:HHmmss}.pdf");

                string cliente = ((Persona)cmbEmployee.SelectedItem)?.NombreCompleto ?? "Cliente Final";

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);

                        page.Header().Column(col =>
                        {
                            col.Item().Text("MR. ROBOT S.A.").FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);
                            col.Item().Text("RNC: 1-01-00000-1").FontSize(10);
                            col.Item().Text($"FACTURA NO: {numeroFacturaActual}").FontSize(14).SemiBold();
                            col.Item().PaddingTop(10).Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
                            col.Item().Text($"Cajero: {FormLogin.CurrentUserName} | Turno: {FormLogin.CurrentShift}");
                            col.Item().Text($"Cliente: {cliente}");
                            col.Item().Text($"Tipo de Pago: {cmbPaymentType.Text}");
                        });

                        page.Content().PaddingVertical(20).Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c => {
                                    c.RelativeColumn(3); c.RelativeColumn(1); c.RelativeColumn(2); c.RelativeColumn(2);
                                });
                                table.Header(h => {
                                    h.Cell().BorderBottom(1).Text("Producto").SemiBold();
                                    h.Cell().BorderBottom(1).AlignRight().Text("Cant").SemiBold();
                                    h.Cell().BorderBottom(1).AlignRight().Text("Precio").SemiBold();
                                    h.Cell().BorderBottom(1).AlignRight().Text("Total").SemiBold();
                                });
                                foreach (var item in cart)
                                {
                                    table.Cell().Text(item.Product?.Name ?? "");
                                    table.Cell().AlignRight().Text(item.Quantity.ToString());
                                    table.Cell().AlignRight().Text($"${item.UnitPrice:F2}");
                                    table.Cell().AlignRight().Text($"${item.SubTotal:F2}");
                                }
                            });
                            col.Item().AlignRight().PaddingTop(10).Text($"SubTotal: ${subTotal:F2}");
                            col.Item().AlignRight().Text($"ITBIS (18%): ${itbisAmount:F2}");
                            col.Item().AlignRight().Text($"TOTAL A PAGAR: ${totalToPay:F2}").FontSize(14).SemiBold();

                            if (cmbPaymentType.Text == "Efectivo" || cmbPaymentType.Text == "Contado")
                            {
                                col.Item().AlignRight().PaddingTop(10).Text($"Monto Pagado: {txtAmountPaid.Text}");
                                col.Item().AlignRight().Text($"Devuelta: {lblChange.Text.Replace("Devuelta: ", "")}");
                            }
                        });
                    });
                }).GeneratePdf(path);

                MessageBox.Show($"PDF generado en:\n{path}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Error PDF: " + ex.Message); }
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto de la tabla para eliminarlo.");
                return;
            }

            string nombreProducto = dgvCart.CurrentRow.Cells["Producto"].Value.ToString();
            var itemToRemove = cart.FirstOrDefault(c => c.Product != null && c.Product.Name == nombreProducto);

            if (itemToRemove != null)
            {
                if (itemToRemove.Product != null)
                {
                    itemToRemove.Product.Stock += itemToRemove.Quantity;
                }
                cart.Remove(itemToRemove);
                RefreshCartGrid();
            }
        }

        // Métodos vacíos para que el diseñador no chille
        private void FormInvoice_Load_1(object sender, EventArgs e) { }
        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void lblTotal_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtQuantity_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
    }
}