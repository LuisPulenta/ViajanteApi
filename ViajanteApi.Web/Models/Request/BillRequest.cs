using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TicketsApi.Web.Models.Request
{
    public class BillRequest
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ChargeDate { get; set; }
        public bool Charge { get; set; }
        public DateTime? DeliverDate { get; set; }
        public bool Deliver { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
