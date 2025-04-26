namespace ITI_Api.DTO.StudentDTO
{
    public class AddStudentDTO
    {
        public int StId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StAddress { get; set; }
        public int? StAge { get; set; }
        public int DepId { get; set; }
        public int Stsuper { get; set; }
    }
}
