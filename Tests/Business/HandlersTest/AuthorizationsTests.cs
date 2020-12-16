
using Business.Constants;
using Business.Handlers.Authorizations.Commands;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using SennedjemTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Authorizations.Commands.ForgotPasswordCommand;
using static Business.Handlers.Authorizations.Commands.LoginUserQuery;
using static Business.Handlers.Authorizations.Commands.RegisterUserCommand;

namespace SennedjemTests.Business.HandlersTest
{
    [TestFixture]
    public class AuthorizationsTests
    {
        Mock<IUserRepository> _userRepository;
        Mock<ITokenHelper> _tokenHelper;

        LoginUserQueryHandler loginUserQueryHandler;
        LoginUserQuery loginUserQuery;
        RegisterUserCommandHandler registerUserCommandHandler;
        RegisterUserCommand command;
        ForgotPasswordCommandHandler forgotPasswordCommandHandler;
        ForgotPasswordCommand forgotPasswordCommand;

        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _tokenHelper = new Mock<ITokenHelper>();
            loginUserQueryHandler = new LoginUserQueryHandler(_userRepository.Object, _tokenHelper.Object);
            registerUserCommandHandler = new RegisterUserCommandHandler(_userRepository.Object);
            forgotPasswordCommandHandler = new ForgotPasswordCommandHandler(_userRepository.Object);
        }
        [Test]
        public async Task Handler_Login()
        {
            var user = DataHelper.GetUser("test");
            HashingHelper.CreatePasswordHash("123456", out byte[] passwordSalt, out byte[] passwordHash);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _userRepository.
                Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).Returns(() => Task.FromResult(user));


            _userRepository.Setup(x => x.GetClaims(It.IsAny<int>()))
                .Returns(new List<OperationClaim>() { new OperationClaim() { Id = 1, Name = "test" } });
            loginUserQuery = new LoginUserQuery
            {
                Email = user.Email,
                Password = "123456"
            };

            var result = await loginUserQueryHandler.Handle(loginUserQuery, new System.Threading.CancellationToken());

            Assert.That(result.Success, Is.True);

        }

        [Test]
        public async Task Handler_Register()
        {
            var registerUser = new User();
            registerUser.Email = "test@test.com";
            registerUser.FullName = "test test";
            command = new RegisterUserCommand
            {
                Email = registerUser.Email,
                FullName = registerUser.FullName,
                Password = "123456"
            };
            var result = await registerUserCommandHandler.Handle(command, new System.Threading.CancellationToken());

            Assert.That(result.Message, Is.EqualTo(Messages.Added));
        }
        [Test]
        public async Task Handler_ForgotPassword()
        {
            var user = DataHelper.GetUser("test");
            _userRepository.
                  Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).Returns(() => Task.FromResult(user));
            forgotPasswordCommand = new ForgotPasswordCommand
            {
                Email = user.Email,
                TCKimlikNo = Convert.ToString(user.CitizenId)
            };
            var result = await forgotPasswordCommandHandler.Handle(forgotPasswordCommand, new System.Threading.CancellationToken());
            Assert.That(result.Success, Is.True);
        }
    }
}
