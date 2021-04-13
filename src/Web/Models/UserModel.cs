namespace Fakebook.Web.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PublicId { get; set; }
		public byte[] ProfilePicture { get; set; }
    }
}