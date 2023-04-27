using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.Domain.Entities
{
    [Table("coupon")]
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("productid")]
        public string ProductId { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("amount")]
        public int Amount { get; set; }
    }
}
