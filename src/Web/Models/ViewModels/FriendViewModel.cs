using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Fakebook.Web.Models.ViewModels
{
    public class FriendViewModel
    {
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public byte[] ProfilePicture { get; set; }
		public string PublicId { get; set; }
	}
}
    
