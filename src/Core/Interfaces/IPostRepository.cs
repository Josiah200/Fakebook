using System.Linq;
using FakeBook.Core.Entities;

namespace FakeBook.Core.Interfaces
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
    }
}