using System;
using System.Collections.Generic;

namespace SimpleWebApi.Core.Repositories.Notes
{
    public interface INotesRepository
    {
        NoteEntity Create(NoteEntity noteEntity);
        NoteEntity GetById(Guid nodeId);
        bool Delete(Guid noteId);
        IEnumerable<NoteEntity> GetByUserKey(Guid userKey, int? count, int? skip);
        NoteEntity Update(NoteEntity noteEntity);
    }
}
