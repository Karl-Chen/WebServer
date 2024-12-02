namespace MyController.Models
{
    public class ProjectType
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Picture { get; set; }
    }
}
