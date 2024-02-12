using ExIdentity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskReal> Tasks { get; set; }

        public DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<TaskReal>()
                .HasOne(x => x.Board)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUser();
            builder.Entity<IdentityUser>()
                .HasData(TestUser);

            SeedBoards();
            builder.Entity<Board>()
                .HasData(OpenBoard, InProgressBoard, DoneBoard);

            builder.Entity<TaskReal>()
                .HasData(
                    new TaskReal()
                    {
                        Id = 1,
                        Title = "OK TEST",
                        Description = "im noting",
                        CreatedOn = DateTime.Now.AddDays(-200),
                        OwnerId = TestUser.Id,
                        BoardId = OpenBoard.Id
                    }
                    ,
                    new TaskReal()
                    {
                        Id = 2,
                        Title = "OK TEST",
                        Description = "im noting",
                        CreatedOn = DateTime.Now.AddDays(-20),
                        OwnerId = TestUser.Id,
                        BoardId = OpenBoard.Id

                    }
                    ,
                    new TaskReal()
                    {

                        Id = 3,
                        Title = "OK TEST",
                        Description = "im noting",
                        CreatedOn = DateTime.Now.AddDays(200),
                        OwnerId = TestUser.Id,
                        BoardId = InProgressBoard.Id
                    }
                    ,
                    new TaskReal()
                    {
                        Id = 4,
                        Title = "OK TEST",
                        Description = "im noting",
                        CreatedOn = DateTime.Now.AddDays(-500),
                        OwnerId = TestUser.Id,
                        BoardId = DoneBoard.Id

                    }
                );


            base.OnModelCreating(builder);
        }

        private void SeedBoards()
        {
            OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open",
            };

            InProgressBoard = new Board()
            {
                Id = 2,
                Name = "In progress"
            };

            DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };

        }

        private void SeedUser()
        {
            var hash = new PasswordHasher<IdentityUser>();

            TestUser = new IdentityUser()
            {
                UserName = "Test@gmail.com",
                NormalizedUserName = "test@gmail.com"
            };

            TestUser.PasswordHash = hash.HashPassword(TestUser, "123456");
        }

        private IdentityUser TestUser { get; set; }

        private Board OpenBoard { get; set; }

        private Board InProgressBoard { get; set; }

        private Board DoneBoard { get; set; }


    }
}
