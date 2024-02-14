using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamRadioStudy.Common.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<TranslatedQuestion> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TranslatedQuestion>()
            .HasKey(q => q.QuestionId);

        modelBuilder.Entity<TranslatedQuestion>()
            .OwnsOne(q => q.EnglishQuestion, q =>
            {
                q.Property(p => p.QuestionText).HasColumnName("EnglishQuestionText");
                q.Property(p => p.CorrectAnswer).HasColumnName("EnglishCorrectAnswer");
                q.Property(p => p.IncorrectAnswers).HasColumnName("EnglishIncorrectAnswers");
            });

        modelBuilder.Entity<TranslatedQuestion>()
            .OwnsOne(q => q.FrenchQuestion, q =>
            {
                q.Property(p => p.QuestionText).HasColumnName("FrenchQuestionText");
                q.Property(p => p.CorrectAnswer).HasColumnName("FrenchCorrectAnswer");
                q.Property(p => p.IncorrectAnswers).HasColumnName("FrenchIncorrectAnswers");
            });
    }
}
