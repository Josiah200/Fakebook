using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Core.Entities
{
    public class Connection : BaseEntity
    {
		public string UserId { get; set; }
		public bool Connected { get; set; }
    }
}