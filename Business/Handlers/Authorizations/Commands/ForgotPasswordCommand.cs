﻿using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Toolkit;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Commands
{
    [SecuredOperation]
    public class ForgotPasswordCommand : IRequest<IResult>
    {
        public string TcKimlikNo { get; set; }
        public string Email { get; set; }

        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IResult>
        {

            private readonly IUserRepository _userRepository;

            public ForgotPasswordCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            /// <summary>           
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
         
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.CitizenId == Convert.ToInt64(request.TcKimlikNo));

                if (user == null)
                    return new ErrorResult(Messages.WrongCid);
                var generatedPassword = RandomPassword.CreateRandomPassword();
                HashingHelper.CreatePasswordHash(generatedPassword, out _, out _);

                _userRepository.Update(user);

                return new SuccessResult(Messages.SendPassword + Messages.NewPassword + generatedPassword);
            }
        }
    }
}
