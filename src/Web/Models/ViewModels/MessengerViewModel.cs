using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Web.Models.ViewModels
{
    public class MessengerViewModel
    {
        public List<Friendship> Friends { get; set; }
		public List<List<Message>> Messages { get; set; }
    }
}