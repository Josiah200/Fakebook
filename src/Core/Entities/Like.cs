namespace Fakebook.Core.Entities
{
    public class Like : BaseEntity
    {
        public string PostId { get; set; }
		public Post Post { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
    }
}