using Core.Entities.Dtos;
using DevArchitecture.Specs.Api;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DevArchitecture.Specs.Steps
{
    [Binding]
    public class UsersSteps
    {
        private readonly UsersApi _api;
        private string _result;
        private UserDto userDto;
        private IEnumerable<UserDto> userDtos;
        public UsersSteps(UsersApi api)
        {
            _api = api;
            _api._cacheManager = new Core.CrossCuttingConcerns.Caching.Microsoft.MemoryCacheManager();
        }

        [Given(@"add ""(.*)"" to base url")]
        public void GivenAddToBaseUrl(string p0)
        {
            _api.url = p0;
        }

        [Given(@"use userId ""(.*)""")]
        public void GivenUseUserId(string p0)
        {
            _api.userId = p0;
        }

        [Given(@"use json")]
        public void GivenUseJson(string multilineText)
        {
            _api.jsonUser = multilineText;
        }

        [When(@"call the api for all users")]
        public async Task WhenCallTheApiForAllUsers()
        {
            userDtos = await _api.GetAllUserAsync();
        }
        [When(@"call the api for user")]
        public async Task WhenCallTheApiForUser()
        {
            userDto = await _api.GetUserByIdAsync();
        }

        [When(@"post the json to url")]
        public async Task WhenPostTheJsonToUrl()
        {
            _result = await _api.PostUserAsync();
        }

        [When(@"put the json to url")]
        public async Task WhenPutTheJsonToUrl()
        {
            _result = await _api.PutUserAsync();
        }
        [When(@"delete user")]
        public async Task DeleteUser()
        {
            _result = await _api.DeleteUserAsync();
        }

        [Then(@"the all users response must contain")]
        public void ThenAllUsersTheResponseMustContain(Table table)
        {
            var dataSet = table.CreateSet<UserDto>();
            userDtos.Select(u => new { u.FullName, u.Email, u.Gender }).Should().Contain(dataSet.Select(u => new { u.FullName, u.Email, u.Gender }));


        }

        [Then(@"the user response must contain")]
        public void ThenUserTheResponseMustContain(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            userDto.FullName.Should().Be((string)data.Name);
            userDto.Email.Should().Be((string)data.Email);
            userDto.Gender.Should().Be(Convert.ToInt32(data.Gender));
        }


        [Then(@"the result should be ""(.*)""")]
        public void ThenTheResultShouldBe(string result)
        {
            _result.Should().Be(result);
        }
    }
}
