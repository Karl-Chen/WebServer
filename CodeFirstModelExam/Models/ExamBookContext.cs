using Microsoft.EntityFrameworkCore;

namespace CodeFirstModelExam.Models
{
    public class ExamBookContext : DbContext
    {
        public ExamBookContext(DbContextOptions<ExamBookContext> options) : base(options) { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Rebook> Rebooks { get; set; }
    }
}
