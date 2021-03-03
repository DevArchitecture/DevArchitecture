﻿using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

namespace Business.Handlers.Users.Commands
{

	public class UpdateUserCommand : IRequest<IResult>
	{

		public int UserId { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string MobilePhones { get; set; }
		public string Address { get; set; }
		public string Notes { get; set; }

		public class UpdateAnimalCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
		{
			private readonly IUserRepository _userRepository;

			public UpdateAnimalCommandHandler(IUserRepository userRepository)
			{
				_userRepository = userRepository;
			}


            [SecuredOperation(Priority = 1)]
			[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
			public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
			{
				var isUserExists = await _userRepository.GetAsync(u => u.UserId == request.UserId);

				isUserExists.FullName = request.FullName;
				isUserExists.Email = request.Email;
				isUserExists.MobilePhones = request.MobilePhones;
				isUserExists.Address = request.Address;
				isUserExists.Notes = request.Notes;

				_userRepository.Update(isUserExists);
				await _userRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}

}
