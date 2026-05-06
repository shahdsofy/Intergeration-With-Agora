using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intergeration_With_Agora.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<LiveSession>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ChannelName)
                    .IsRequired()
                    .HasMaxLength(100);

                // جعل الـ ChannelName فريد لمنع تداخل الجلسات
                entity.HasIndex(e => e.ChannelName)
                    .IsUnique();

              

                // العلاقة مع المدرس (User)
                entity.HasOne(d => d.Instructor)
                    .WithMany(e=>e.ManagedSessions)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.Restrict); // منع مسح المدرس لو عنده جلسات مسجلة
            });

            builder.Entity<SessionRecording>(entity =>
            {
                entity.HasKey(e => e.Id);

                // علاقة One-to-One: كل جلسة لها تسجيل واحد فقط
                entity.HasOne(e => e.Session)
                       .WithMany(s => s.Recordings) // السيشن ليها أكتر من ريكورد
                       .HasForeignKey(e => e.SessionId)
                       .OnDelete(DeleteBehavior.Cascade);

                // الـ Sid اللي جاي من أجورا لازم يكون فريد للبحث السريع في الـ Webhook
                entity.HasIndex(e => e.Sid)
                    .IsUnique()
                    .HasFilter("[Sid] IS NOT NULL"); // Index فقط للقيم غير الفارغة

                entity.Property(e => e.ResourceId)
                    .HasMaxLength(500);

                entity.Property(e => e.Sid)
                    .HasMaxLength(500);

              
            });

            
        }
        public DbSet<LiveSession> LiveSessions { get; set; }
        public DbSet<SessionRecording> SessionRecordings { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}
