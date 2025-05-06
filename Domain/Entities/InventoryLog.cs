using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InventoryLog
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int QuantityChanged { get; set; }
        public LogType LogType { get; set; }
        public DateTime LogDate { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }

    public enum LogType
    {
        Purchase,
        Sale,
        Adjustment
    }
}
