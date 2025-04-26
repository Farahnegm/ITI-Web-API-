using AutoMapper;

namespace ITI_Api.DTO.StudentDTO
{
    public class UpdateStudentDTO
    {
        public int StId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StAddress { get; set; }
        public int? StAge { get; set; }
        public int? DepID { get; set; }
        public int? StSuper { get; set; }
    }
}
