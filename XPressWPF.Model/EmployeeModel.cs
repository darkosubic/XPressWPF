namespace XPressWPF.Model
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public double? Salary { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
