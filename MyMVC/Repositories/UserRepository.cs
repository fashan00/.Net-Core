using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyMVC;
using MyMVC.Models;
using MyWebsite.Models;

namespace MyWebsite.Repositories {
    public class UserRepository : IRepository<UserModel, int> {
        private readonly MyContext _context;

        public UserRepository (MyContext context) {
            _context = context;
        }

        public int Create (UserModel entity) {
            _context.Users.Add (entity);
            _context.SaveChanges ();
            return entity.Id;
        }

        public void Update (UserModel entity) {
            var oriUser = _context.Users.Single (x => x.Id == entity.Id);
            _context.Entry (oriUser).CurrentValues.SetValues (entity);
            _context.SaveChanges ();
        }

        public void Delete (int id) {
            _context.Users.Remove (_context.Users.Single (x => x.Id == id));
            _context.SaveChanges ();
        }

        public IEnumerable<UserModel> Find (Expression<Func<UserModel, bool>> expression) {
            return _context.Users.Where (expression);
        }

        public UserModel FindById (int id) {
            return _context.Users.SingleOrDefault (x => x.Id == id);
        }
    }
}