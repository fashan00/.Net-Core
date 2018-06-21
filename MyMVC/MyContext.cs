using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MyMVC.Models;
using MyWebsite.Models;

namespace MyMVC {
    // Microsoft.EntityFrameworkCore 版本 Version="2.0.1"
    public class MyContext : DbContext {
        public MyContext (DbContextOptions<MyContext> options) : base (options) { }

        public DbSet<UserModel> Users { get; set; }
    }

    public static class DatabaseFacadeExtensions {
        public static bool Exists (this DatabaseFacade source) {
            return source.GetService<IRelationalDatabaseCreator> ().Exists ();
        }
    }
}