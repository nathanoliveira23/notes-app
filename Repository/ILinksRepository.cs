using Notes.Models;

namespace Notes.Repository
{
    public interface ILinksRepository
    {
        public Task SaveLink(Link link);
    }
}