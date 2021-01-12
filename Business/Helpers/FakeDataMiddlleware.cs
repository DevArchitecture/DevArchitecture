using Core.Utilities.IoC;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Business.Handlers.Languages.Commands;
using Business.Handlers.Translates.Commands;

namespace Business.Helpers
{
	public static class FakeDataMiddlleware
	{
		public async static Task UseDbFakeDataCreator(this IApplicationBuilder app)
		{
			var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

			await mediator.Send(new CreateLanguageCommand { Code = "tr-TR", Name = "Türkçe" });
			await mediator.Send(new CreateLanguageCommand { Code = "en-EN", Name = "English" });

			await mediator.Send(new CreateTranslateCommand{ LangId = 1, Code = "LOGIN", Value = "Giriş TR" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "email", Value = "E posta" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "password", Value = "Şifre" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "update", Value = "Güncelle" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "delete", Value = "Sil" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "users_Groups", Value = "Kullanıcının Grupları" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "users_claims", Value = "Kullanıcının Yetkileri" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "create", Value = "Yeni" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "Users", Value = "Kullanıcılar" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "Groups", Value = "Gruplar" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "LOGIN", Value = "Login EN" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "email", Value = "Email" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "password", Value = "Password" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "update", Value = "Update" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "delete", Value = "Delete" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "users_Groups", Value = "User's Groups" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "users_claims", Value = "User's Claims" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "create", Value = "Create" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "Users", Value = "Users" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "Groups", Value = "Groups" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "Operation Claim", Value = "Operasyon Yetkileri" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "Operation Claim", Value = "Operation Claim" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "Languages", Value = "Diller" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "Languages", Value = "Languages" });
			await mediator.Send(new CreateTranslateCommand { LangId = 1, Code = "Translate Words", Value = "Dil Çevirileri" });
			await mediator.Send(new CreateTranslateCommand { LangId = 2, Code = "Translate Words", Value = "Translate Words" });
		}
	}
}
