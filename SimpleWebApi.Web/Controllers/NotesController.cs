using SimpleWebApi.Core.Models.Notes;
using SimpleWebApi.Core.Services.Notes;
using SimpleWebApi.Web.Attributes;
using SimpleWebApi.Web.Extensions;
using System;
using System.Web.Http;

namespace SimpleWebApi.Web.Controllers
{
    [JwtAuthorize]
    [ErrorFilter]
    [RoutePrefix("api/notes")]
    public class NotesController : ApiController
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        /// <summary>
        /// Create new user note
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Created note</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] NoteRequest model)
        {
            return Ok(_notesService.Create(model, User.GetTokenData()));
        }

        /// <summary>
        /// Update user note
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="model"></param>
        /// <returns>Updated note</returns>
        [HttpPut]
        [Route("{noteId}")]
        public IHttpActionResult Update(Guid noteId, [FromBody] NoteRequest model)
        {
            return Ok(_notesService.Update(noteId, model, User.GetTokenData()));
        }

        /// <summary>
        /// Return user note by id
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns>User note</returns>
        [HttpGet]
        [Route("{noteId}")]
        public IHttpActionResult Get(Guid noteId)
        {
            var note = _notesService.Get(noteId, User.GetTokenData());

            if(note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        /// <summary>
        /// Return user notes
        /// </summary>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns>User notes</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int? count = null, int? skip = null)
        {
            return Json(_notesService.GetAll(count, skip, User.GetTokenData()));
        }

        /// <summary>
        /// Delete user note with id
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{noteId}")]
        public IHttpActionResult Delete(Guid noteId)
        {
            if(_notesService.Delete(noteId, User.GetTokenData()))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
