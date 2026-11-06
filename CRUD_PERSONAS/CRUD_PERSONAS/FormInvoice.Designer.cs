using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CRUD_PERSONAS
{
    partial class FormInvoice
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbProducts = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            txtQuantity = new TextBox();
            btnAdd = new Button();
            dgvCart = new DataGridView();
            lblTotal = new Label();
            btnSaveInvoice = new Button();
            label1 = new Label();
            contextMenuStrip2 = new ContextMenuStrip(components);
            label2 = new Label();
            btnPrintTicket = new Button();
            cmbEmployee = new ComboBox();
            Cliente = new Label();
            cmbPaymentType = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            txtAmountPaid = new TextBox();
            lblChange = new Label();
            label5 = new Label();
            button1 = new Button();
            btnRemoveFromCart = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            SuspendLayout();
            // 
            // cmbProducts
            // 
            cmbProducts.BackColor = Color.FromArgb(252, 250, 249);
            cmbProducts.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducts.FormattingEnabled = true;
            cmbProducts.Location = new Point(162, 104);
            cmbProducts.Name = "cmbProducts";
            cmbProducts.Size = new Size(324, 27);
            cmbProducts.Sorted = true;
            cmbProducts.TabIndex = 15;
            cmbProducts.SelectedIndexChanged += cmbProducts_SelectedIndexChanged;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // txtQuantity
            // 
            txtQuantity.BackColor = Color.FromArgb(252, 250, 249);
            txtQuantity.Location = new Point(534, 97);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(102, 27);
            txtQuantity.TabIndex = 2;
            txtQuantity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(39, 39, 39);
            btnAdd.Font = new Font("JetBrains Mono", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.ImageAlign = ContentAlignment.TopCenter;
            btnAdd.Location = new Point(700, 58);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(308, 66);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Agregar al carrito";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.BackgroundColor = Color.FromArgb(158, 173, 200);
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.GridColor = Color.FromArgb(221, 218, 231);
            dgvCart.Location = new Point(35, 163);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidth = 51;
            dgvCart.Size = new Size(1349, 263);
            dgvCart.TabIndex = 4;
            dgvCart.CellContentClick += dgvCart_CellContentClick;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.BackColor = Color.FromArgb(0, 0, 0, 0);
            lblTotal.Font = new Font("JetBrains Mono", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.FromArgb(39, 39, 39);
            lblTotal.Location = new Point(1050, 506);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(181, 90);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "SubTotal: \r\nITIBIS: \r\nTotal: $0.00\r\n";
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            lblTotal.Click += lblTotal_Click;
            // 
            // btnSaveInvoice
            // 
            btnSaveInvoice.BackColor = Color.FromArgb(39, 39, 39);
            btnSaveInvoice.ForeColor = Color.White;
            btnSaveInvoice.Location = new Point(1050, 432);
            btnSaveInvoice.Name = "btnSaveInvoice";
            btnSaveInvoice.Size = new Size(163, 55);
            btnSaveInvoice.TabIndex = 6;
            btnSaveInvoice.Text = "Procesar Venta";
            btnSaveInvoice.UseVisualStyleBackColor = false;
            btnSaveInvoice.Click += btnSaveInvoice_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono", 13.7999992F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(519, 64);
            label1.Name = "label1";
            label1.Size = new Size(139, 30);
            label1.TabIndex = 9;
            label1.Text = "Cantidad:";
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.ImageScalingSize = new Size(20, 20);
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(61, 4);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 9);
            label2.Name = "label2";
            label2.Size = new Size(0, 19);
            label2.TabIndex = 11;
            label2.Click += label2_Click;
            // 
            // btnPrintTicket
            // 
            btnPrintTicket.BackColor = Color.FromArgb(39, 39, 39);
            btnPrintTicket.ForeColor = Color.White;
            btnPrintTicket.Location = new Point(1221, 432);
            btnPrintTicket.Name = "btnPrintTicket";
            btnPrintTicket.Size = new Size(163, 55);
            btnPrintTicket.TabIndex = 12;
            btnPrintTicket.Text = "Imprimir Ticket";
            btnPrintTicket.UseVisualStyleBackColor = false;
            btnPrintTicket.Click += btnPrintTxt_Click;
            // 
            // cmbEmployee
            // 
            cmbEmployee.FormattingEnabled = true;
            cmbEmployee.Location = new Point(149, 64);
            cmbEmployee.Name = "cmbEmployee";
            cmbEmployee.Size = new Size(337, 27);
            cmbEmployee.TabIndex = 14;
            // 
            // Cliente
            // 
            Cliente.AutoSize = true;
            Cliente.Font = new Font("JetBrains Mono", 12F);
            Cliente.Location = new Point(35, 62);
            Cliente.Name = "Cliente";
            Cliente.Size = new Size(108, 26);
            Cliente.TabIndex = 7;
            Cliente.Text = "Cliente:";
            Cliente.Click += label1_Click;
            // 
            // cmbPaymentType
            // 
            cmbPaymentType.FormattingEnabled = true;
            cmbPaymentType.Items.AddRange(new object[] { "Efectivo", "Credito" });
            cmbPaymentType.Location = new Point(36, 477);
            cmbPaymentType.Name = "cmbPaymentType";
            cmbPaymentType.Size = new Size(197, 27);
            cmbPaymentType.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("JetBrains Mono", 12F);
            label4.Location = new Point(36, 102);
            label4.Name = "label4";
            label4.Size = new Size(120, 26);
            label4.TabIndex = 16;
            label4.Text = "Producto:";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono", 10.7999992F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(35, 451);
            label3.Name = "label3";
            label3.Size = new Size(153, 23);
            label3.TabIndex = 17;
            label3.Text = "Tipo de Pago:";
            // 
            // txtAmountPaid
            // 
            txtAmountPaid.Location = new Point(279, 477);
            txtAmountPaid.Name = "txtAmountPaid";
            txtAmountPaid.Size = new Size(161, 27);
            txtAmountPaid.TabIndex = 18;
            txtAmountPaid.TextChanged += txtAmountPaid_TextChanged;
            // 
            // lblChange
            // 
            lblChange.AutoSize = true;
            lblChange.Font = new Font("JetBrains Mono", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblChange.Location = new Point(35, 543);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(192, 26);
            lblChange.TabIndex = 19;
            lblChange.Text = "Devuelta: $0.00";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("JetBrains Mono", 10.7999992F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(279, 451);
            label5.Name = "label5";
            label5.Size = new Size(153, 23);
            label5.TabIndex = 20;
            label5.Text = "Monto Pagado:";
            // 
            // button1
            // 
            button1.Location = new Point(1077, 63);
            button1.Name = "button1";
            button1.Size = new Size(8, 8);
            button1.TabIndex = 21;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnRemoveFromCart
            // 
            btnRemoveFromCart.BackColor = Color.FromArgb(39, 39, 39);
            btnRemoveFromCart.Font = new Font("JetBrains Mono", 12F);
            btnRemoveFromCart.ForeColor = Color.White;
            btnRemoveFromCart.Location = new Point(1033, 58);
            btnRemoveFromCart.Name = "btnRemoveFromCart";
            btnRemoveFromCart.Size = new Size(308, 66);
            btnRemoveFromCart.TabIndex = 22;
            btnRemoveFromCart.Text = "Eliminar del carrtio";
            btnRemoveFromCart.UseVisualStyleBackColor = false;
            btnRemoveFromCart.Click += btnRemoveFromCart_Click;
            // 
            // FormInvoice
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(225, 232, 224);
            ClientSize = new Size(1411, 605);
            Controls.Add(btnRemoveFromCart);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(lblChange);
            Controls.Add(txtAmountPaid);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(cmbPaymentType);
            Controls.Add(cmbEmployee);
            Controls.Add(btnPrintTicket);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Cliente);
            Controls.Add(btnSaveInvoice);
            Controls.Add(lblTotal);
            Controls.Add(dgvCart);
            Controls.Add(btnAdd);
            Controls.Add(txtQuantity);
            Controls.Add(cmbProducts);
            Font = new Font("JetBrains Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FormInvoice";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Punto de Venta";
            Load += FormInvoice_Load;
            KeyDown += FormInvoice_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbProducts;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox txtQuantity;
        private Button btnAdd;
        private DataGridView dgvCart;
        private Label lblTotal;
        private Label label1;
        private ContextMenuStrip contextMenuStrip2;
        private Label label2;
        private ComboBox cmbEmployee;
        private Label Cliente;
        private ComboBox cmbPaymentType;
        private Label label4;
        private Label label3;
        private TextBox txtAmountPaid;
        private Label lblChange;
        private Label label5;
        private Button btnSaveInvoice;
        private Button btnPrintTicket;
        private Button button1;
        private Button btnRemoveFromCart;
    }
}