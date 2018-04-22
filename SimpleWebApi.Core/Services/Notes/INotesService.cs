using System;
using SimpleWebApi.Core.Models.Authorization;
using SimpleWebApi.Core.Models.Notes;
using System.Collections.Generic;

namespace SimpleWebApi.Core.Services.Notes
{
    public interface INotesService
    {
        NoteResponse Create(NoteRequest model, AccessTokenData accessTokenData);
        NoteResponse Update(Guid noteId, NoteRequest model, AccessTokenData accessTokenData);
        IEnumerable<NoteResponse> GetAll(int? count, int? skip, AccessTokenData accessTokenData);
        NoteResponse Get(Guid noteId, AccessTokenData accessTokenData);
        bool Delete(Guid noteId, AccessTokenData accessTokenData);
    }
}
