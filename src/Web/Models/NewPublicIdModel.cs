using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Web.Models
{
    public class NewPublicIdModel
    {
		[Required]
		[StringLength(8)]
		[DataType(DataType.Text)]
		[Display(Name = "Public Identifier")]
		public string PublicId { get; set; }
    }
}