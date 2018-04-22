using System;
using System.Collections.Generic;
using SimpleWebApi.Core.Repositories.Notes;
using SimpleWebApi.Repositories.MongoDB.Base;
using MongoDB.Driver;

namespace SimpleWebApi.Repositories.MongoDB.Notes
{
    public class NotesRepository : BaseRepository<NoteEntity>, INotesRepository
    {
        protected override string CollectionName => "db.notes";

        public NotesRepository(MongoDBConfiguration mongoDBConfig) : base(mongoDBConfig)
        {
        }

        public NoteEntity Create(NoteEntity noteEntity)
        {
            EntityCollection.InsertOne(noteEntity);

            return GetById(noteEntity.Id);
        }

        public bool Delete(Guid noteId)
        {
            return EntityCollection.DeleteMany(x => x.Id == noteId).DeletedCount > 0;
        }

        public NoteEntity GetById(Guid nodeId)
        {
            return EntityCollection
                .Find(x => x.Id == nodeId)
                .FirstOrDefault();
        }

        public IEnumerable<NoteEntity> GetByUserKey(Guid userKey, int? count, int? skip)
        {
            return EntityCollection
                .Find(x => x.UserKey == userKey)
                .SortByDescending(x => x.CreatedTime)
                .Skip(skip)
                .Limit(count)
                .ToList();
        }

        public NoteEntity Update(NoteEntity noteEntity)
        {
            var update = Builders<NoteEntity>.Update
                .Set(x => x.Name, noteEntity.Name)
                .Set(x => x.Text, noteEntity.Text)
                .Set(x => x.UpdateTime, DateTime.Now);

            EntityCollection.UpdateOne(x => x.Id == noteEntity.Id, update);

            return GetById(noteEntity.Id);
        }
    }
}
