using SimpleWebApi.Core.Services.Notes;
using System;
using SimpleWebApi.Core.Models.Authorization;
using SimpleWebApi.Core.Models.Notes;
using SimpleWebApi.Core.Repositories.Notes;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebApi.Core.Services.Notes
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IValidator<NoteRequest> _noteModelValidator;

        public NotesService(INotesRepository notesRepository, IValidator<NoteRequest> noteModelValidator)
        {
            _notesRepository = notesRepository;
            _noteModelValidator = noteModelValidator;
        }

        public NoteResponse Create(NoteRequest model, AccessTokenData accessTokenData)
        {
            ValidateNote(model);

            var addedNote = _notesRepository.Create(new NoteEntity()
            {
                Name = model.Name,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Text = model.Text,
                UserKey = accessTokenData.UserKey
            });

            return ToNoteResponse(addedNote);
        }

        public bool Delete(Guid noteId, AccessTokenData accessTokenData)
        {
            var note = _notesRepository.GetById(noteId);

            return note?.UserKey == accessTokenData.UserKey ? _notesRepository.Delete(noteId) : false;
        }

        public NoteResponse Get(Guid noteId, AccessTokenData accessTokenData)
        {
            var note = _notesRepository.GetById(noteId);

            return note?.UserKey == accessTokenData.UserKey ? ToNoteResponse(note) : null;
        }

        public IEnumerable<NoteResponse> GetAll(int? count, int? skip, AccessTokenData accessTokenData)
        {
            return _notesRepository
                .GetByUserKey(accessTokenData.UserKey, count, skip)
                .Select(x => ToNoteResponse(x));
        }

        public NoteResponse Update(Guid noteId, NoteRequest model, AccessTokenData accessTokenData)
        {
            ValidateNote(model);

            var addedNote = _notesRepository.Update(new NoteEntity()
            {
                Id = noteId,
                Name = model.Name,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Text = model.Text,
                UserKey = accessTokenData.UserKey
            });

            return ToNoteResponse(addedNote);
        }

        private void ValidateNote(NoteRequest model)
        {
            var validationResult = _noteModelValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        private NoteResponse ToNoteResponse(NoteEntity noteEntity)
        {
            if (noteEntity == null) return null;

            return new NoteResponse
            {
                Id = noteEntity.Id,
                CreatedTime = noteEntity.CreatedTime,
                UpdateTime = noteEntity.UpdateTime,
                Name = noteEntity.Name,
                Text = noteEntity.Text
            };
        }
    }
}
