using Microsoft.EntityFrameworkCore;
using quizon.Models;

public class QuizOnContext : DbContext
{
    public DbSet<Category> categories { get; set; }
    public DbSet<Question> questions { get; set; }
    public DbSet<Option> options { get; set; }

    public QuizOnContext(DbContextOptions<QuizOnContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("categories");
            category.HasKey(c => c.id);
            category.Property(c => c.name).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Question>(question =>
        {
            question.ToTable("questions");
            question.HasKey(q => q.id);
            question.Property(q => q.title).IsRequired().HasMaxLength(255);
            question.Property(q => q.categoryId).IsRequired();
            question.HasOne(q => q.category).WithMany(c => c.questions).HasForeignKey(q => q.categoryId);
            question.HasMany(q => q.options).WithOne(o => o.question).HasForeignKey(o => o.questionId);
        });

        modelBuilder.Entity<Option>(option =>
        {
            option.ToTable("options");
            option.HasKey(o => o.id);
            option.Property(o => o.value).IsRequired().HasMaxLength(255);
            option.Property(o => o.questionId).IsRequired();
        });
    }
}