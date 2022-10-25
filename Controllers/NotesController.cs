using Microsoft.AspNetCore.Mvc;
using Notes.DTOs.NoteDTOs;
using Notes.Models;
using Notes.Repository;
using Notes.Utils;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : BaseController
    {
        private readonly INotesRepository _notesRepository;
        private readonly ITagsRepository _tagsRepository;
        private readonly ILinksRepository _linksRepository;
        private readonly ILogger<NotesController> _logger;
        public NotesController(INotesRepository notesRepository, 
                        ILogger<NotesController> logger, 
                        IUserRepository userRepository,
                        ITagsRepository tagsRepository,
                        ILinksRepository linksRepository) : base(userRepository)
        {
            _notesRepository = notesRepository;
            _tagsRepository = tagsRepository;
            _linksRepository = linksRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateNote(CreateNoteDTO createNoteDTO)
        {
            User user = ReadToken();
            try
            {
                // Note note = new()
                // {
                //    Title = createNoteDTO.Title,
                //    Description = createNoteDTO.Description,
                //    User = user
                // };

                // Tag tag = new()
                // {
                //     Name = createNoteDTO.Tags.ToString(),
                //     User = user,
                //     Note = _notesRepository.GetNoteId(note.Id),
                // };

                // Link link = new()
                // {
                //     Note = _notesRepository.GetNoteId(note.Id),
                //     Text = createNoteDTO.Links.ToString()
                // };

                // _notesRepository.SaveNote(note);
                // _tagsRepository.SaveTag(tag);
                // _linksRepository.SaveLink(link);

                // return Created(string.Empty, note);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante a criação da nota: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao criar a nota.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}