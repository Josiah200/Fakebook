using System.Linq;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
    }
}