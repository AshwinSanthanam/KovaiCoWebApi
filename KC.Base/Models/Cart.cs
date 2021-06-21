namespace KC.Base.Models
{
    public class Cart : BaseEntity
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
