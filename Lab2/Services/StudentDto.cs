namespace Lab2.Services
{
    public class StudentDto
    {
        public string Id { get; set; }  // For MongoDB
        public int StudentId { get; set; }  // For SQL
        public string Name { get; set; }
        public DateTime Birth { get; set; }
    }


}
