using System;
using System.Collections.Generic;

namespace CRUD_PERSONAS.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty; // Ej: "00001"
        public DateTime Date { get; set; } = DateTime.Now;

        // Datos del POS
        public string PaymentType { get; set; } = "Efectivo"; // "Efectivo" o "Credito"
        public decimal SubTotal { get; set; }
        public decimal ItbisAmount { get; set; }
        public decimal TotalToPay { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal ChangeDue { get; set; }

        // Relaciones
        public int UserId { get; set; } // El cajero que facturó
        public User? User { get; set; }
        public string CustomerName { get; set; } = string.Empty; // Nombre del cliente

        public List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}