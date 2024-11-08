using Lab2.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Lab2.Services
{
    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<StudentDto> _studentsCollection;

        public MongoService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<StudentDto>("Students");
        }

        public async Task CreateAsync(StudentDto studentDto)
        {
            await _studentsCollection.InsertOneAsync(studentDto);
        }

        public async Task<List<StudentDto>> GetAllAsync()
        {
            return await _studentsCollection.Find(student => true).ToListAsync();
        }

        public async Task<StudentDto> GetByIdAsync(string id)
        {
            return await _studentsCollection.Find(student => student.Id == id).FirstOrDefaultAsync();
        }
    }
}
