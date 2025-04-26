namespace ITI_Api.DTO.DepartmentDTO
{
    public class ReadDepartmentDTO
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public string DeptDesc { get; set; }

        public string DeptLocation { get; set; }
        public int? DeptManager { get; set; }
        public String DeptManagerName { get; set; }
    }
}
