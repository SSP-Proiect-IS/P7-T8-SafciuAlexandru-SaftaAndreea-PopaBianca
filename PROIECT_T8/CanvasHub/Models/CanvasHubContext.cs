using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CanvasHub.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CanvasHub.Models
{
    public class CanvasHubContext : IdentityDbContext<IdentityUser>
    {
        public CanvasHubContext(DbContextOptions<CanvasHubContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<EventInvitation> EventInvitations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
