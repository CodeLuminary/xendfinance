using System.ComponentModel.DataAnnotations;

namespace Xend_Finance.Entities
{
    public class Clients
    {
        [Key]
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
