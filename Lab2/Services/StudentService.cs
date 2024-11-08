using MongoDB.Driver;
using Lab2.DAL.Settings;
using Microsoft.Extensions.Options;
using Lab2.DAL;

namespace Lab2.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<MongoStudent> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<MongoStudent>("Students");
        }

        public async Task<List<MongoStudent>> GetAsync() =>
            await _studentsCollection.Find(s => true).ToListAsync();

        public async Task<MongoStudent> GetByIdAsync(string id) =>
            await _studentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(MongoStudent student) =>
            await _studentsCollection.InsertOneAsync(student);

        public async Task UpdateAsync(string id, MongoStudent updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(s => s.Id == id, updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(s => s.Id == id);
    }
}
