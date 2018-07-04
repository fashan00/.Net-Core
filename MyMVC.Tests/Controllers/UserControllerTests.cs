using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyMVC.Controllers;
using MyMVC.Models;
using MyWebsite.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace MyWebsite.Tests.Controllers
{
    public class UserControllerTests
    {
        private IRepository<UserModel, int> _fakeRepository;
        private UserControllerByRepo _target;

        [SetUp]
        public void SetUp()
        {
            _fakeRepository = Substitute.For<IRepository<UserModel, int>>();
            _target = new UserControllerByRepo(_fakeRepository);
        }

        [Test]
        public void SearchUser()
        {
            // Arrange
            var query = "test";
            var model = new UserModel { Id = 1 };
            _fakeRepository.Find(Arg.Any<Expression<Func<UserModel, bool>>>())
                .Returns(new List<UserModel> { model });

            // Act
            var actual = _target.Get(query);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void GetUser()
        {
            // Arrange
            var model = new UserModel { Id = 1 };
            _fakeRepository.FindById(Arg.Any<int>()).Returns(model);

            // Act
            var actual = _target.Get(model.Id);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void CreateUser()
        {
            // Arrange
            var model = new UserModel();

            // Act
            var actual = _target.Post(model);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void UpdateUser()
        {
            // Arrange
            var model = new UserModel { Id = 1 };

            // Act
            var actual = _target.Put(model.Id, model);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void DeleteUser()
        {
            // Arrange
            var model = new UserModel { Id = 1 };

            // Act
            var actual = _target.Delete(model.Id);

            // Assert
            //Assert.IsTrue(actual.IsSuccess);
            Assert.Fail();
        }
    }
}