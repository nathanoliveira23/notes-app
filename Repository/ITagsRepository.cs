using Notes.Models;

namespace Notes.Repository
{
    public interface ITagsRepository
    {
        public Task SaveTag(Tag tag);
    }
}