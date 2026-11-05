using System;
using System.Collections.Generic;

namespace CRUD_LOGIN_PEOPLE.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public int UserId { get; set; }

        public User? User { get; set; }

        public List<InvoiceDetail> InvoiceDetails { get; set; } =
            new List<InvoiceDetail>();
    }
}