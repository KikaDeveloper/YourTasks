using SQLite;

namespace YourTasks.Models
{
    [Table("Projects")]
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("project_name")]
        public string? Name { get; set; }
        [Column("ellipse_color")]
        public string? EllipseColor { get; set; }
        [Column("description")]
        public string? Description { get; set; }
    }
}