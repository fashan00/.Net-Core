using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;
using MyWebsite.Models;
using MyWebsite.Repositories;

namespace MyMVC.Controllers {
    [Route ("api/[controller]s")]
    public class UserControllerByRepo : Controller {
        private readonly IRepository<UserModel, int> _repository;

        public UserControllerByRepo (IRepository<UserModel, int> repository) {
            _repository = repository;
        }

        [HttpGet]
        public ResultModel Get (string q) {
            var result = new ResultModel ();
            result.Data = _repository.Find (x => string.IsNullOrEmpty (q) ||
                Regex.IsMatch (x.Name, q, RegexOptions.IgnoreCase));
            result.IsSuccess = true;
            return result;
        }

        [HttpGet ("{id}")]
        public ResultModel Get (int id) {
            var result = new ResultModel ();
            result.Data = _repository.FindById (id);
            result.IsSuccess = true;
            return result;
        }

        [HttpPost]
        public ResultModel Post ([FromBody] UserModel user) {
            var result = new ResultModel ();
            _repository.Create (user);
            result.Data = user.Id;
            result.IsSuccess = true;
            return result;
        }

        [HttpPut ("{id}")]
        public ResultModel Put (int id, [FromBody] UserModel user) {
            var result = new ResultModel ();
            try {
                user.Id = id;
                _repository.Update (user);
                result.IsSuccess = true;
            } catch {
                // ...
            }
            return result;
        }

        [HttpDelete ("{id}")]
        public ResultModel Delete (int id) {
            var result = new ResultModel ();
            try {
                _repository.Delete (id);
                result.IsSuccess = true;
            } catch {
                // ...
            }
            return result;
        }
    }
}