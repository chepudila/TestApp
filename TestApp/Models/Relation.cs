namespace TestApp.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public int SupervisorId { get; set; }
        public Employee Supervisor { get; set; }
        public int SubordinateId { get; set; }
        public Employee Subordinate { get; set; }
    }
}
