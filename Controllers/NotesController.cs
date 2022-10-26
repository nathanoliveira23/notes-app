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
        private readonly ILogger<NotesController> _logger;
        public NotesController(INotesRepository notesRepository, 
                        ILogger<NotesController> logger, 
                        IUserRepository userRepository) : base(userRepository)
        {
            _notesRepository = notesRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateNote(CreateNoteDTO createNoteDTO)
        {
            User user = ReadToken();
            try
            {
                Note note = new()
                {
                    Title = createNoteDTO.Title,
                    Description = createNoteDTO.Description,
                    Tags = createNoteDTO.Tags,
                    Links = createNoteDTO.Links,
                    User = user,
                };
                
                _notesRepository.SaveNote(note);

                return Created(string.Empty, note);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            User user = ReadToken();
            var noteId = _notesRepository.GetNoteId(id);

            if (noteId == null)
                return NotFound(new AppError("Não foi possível encontrar a nota informada."));
            try
            {
                _notesRepository.RemoveNote(noteId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante a remoção da nota: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao remover a nota.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}