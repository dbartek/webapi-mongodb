using System;

namespace SimpleWebApi.Core.Models.Notes
{
    public class NoteResponse : NoteRequest
    {
        public Guid? Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
