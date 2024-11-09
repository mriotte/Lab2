﻿using MongoDB.Driver;
using Lab2.DAL.Settings;
using Microsoft.Extensions.Options;
using Lab2.DAL;

namespace Lab2.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<StudentDto> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<StudentDto>("Students");
        }

        public async Task<List<StudentDto>> GetAsync() =>
            await _studentsCollection.Find(s => true).ToListAsync();

        public async Task<StudentDto> GetByIdAsync(string id) =>
            await _studentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(StudentDto student) =>
            await _studentsCollection.InsertOneAsync(student);

        public async Task UpdateAsync(string id, StudentDto updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(s => s.Id == id, updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(s => s.Id == id);
    }
}
