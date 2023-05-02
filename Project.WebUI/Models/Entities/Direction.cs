namespace Project.WebUI.Models.Entities
{
    public class Direction
    {
        public int Id { get; set; }
        public string DirectionName { get; set; }
        public int DepartmentId { get; set; }  
        public virtual Department Department { get; set; }  
    }
}
