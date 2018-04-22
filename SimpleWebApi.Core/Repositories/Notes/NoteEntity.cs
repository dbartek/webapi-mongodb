using SimpleWebApi.Core.Repositories.Base;
using System;

namespace SimpleWebApi.Core.Repositories.Notes
{
    public class NoteEntity : BaseEntity
    {
        public Guid UserKey { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
