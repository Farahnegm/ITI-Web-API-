namespace ITI_Api.DTO.StudentDTO
{
    public class ReadStudentDTO
    {
        public int StId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StAddress { get; set; }
        public int? StAge { get; set; }
        public string DepName { get; set; }
    }
}
