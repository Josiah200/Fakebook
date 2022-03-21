namespace Fakebook.Core.Entities
{
    public class Like : BaseEntity
    {
        public string UserId { get; set; }
		public User User { get; set; }
		public string PostId { get; set; }
		public Post Post { get; set; }
		public Comment Comment { get; set; }
    }
}