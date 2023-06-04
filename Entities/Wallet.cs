using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xend_Finance.Entities
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public string Currency { get; set; }
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Clients Clients { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
