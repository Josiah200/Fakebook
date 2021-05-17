namespace Fakebook.Core.Entities
{
    public class Like<T> : BaseEntity where T : BaseEntity
    {
        public string UserId { get; set; }
		public User User { get; set; }
		public string PostId { get; set; }
		public T Post { get; set; }
    }
}