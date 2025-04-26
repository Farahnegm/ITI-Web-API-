namespace ITI_Api.DTO.InstructorDTO
{
    public class AddInstructorDTO
    {
        public int InsId { get; set; }
        public string InsName { get; set; }
        public string InsDegree { get; set; }
        public decimal? Salary { get; set; }
        public int? DeptId { get; set; }
    }
}
