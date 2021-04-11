using System.ComponentModel.DataAnnotations;

namespace Fakebook.Core.Entities
{
    public class Photo : BaseEntity
    {
		public byte[] PhotoByteArray { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public string? PostId { get; set; }
		public Post? Post { get; set; }
		public bool IsProfilePicture { get; set; }
    }
}