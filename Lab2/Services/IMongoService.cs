namespace Lab2.Services
{
    public interface IMongoService
    {
        Task CreateAsync(StudentDto studentDto);
        Task<List<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(string id);
    }
}
