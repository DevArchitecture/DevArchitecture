using Core.Entities.Dtos;
using DevArchitecture.Specs.Helpers;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Core.CrossCuttingConcerns.Caching;
using Newtonsoft.Json;

namespace DevArchitecture.Specs.Api
{
    public class UsersApi
    {
        public string url;
        public string userId;
        public string jsonUser;
        private readonly IRestClient _client;
        public ICacheManager _cacheManager { get; set; }
        public UsersApi()
        {

            _client = new RestClient("https://localhost:5001/");
            _client.Timeout = -1;
            _client.Authenticator = new JwtAuthenticator(Token());
        }
        private static string Token()
        {
            //var claims = UserRepository.GetClaims(1);
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            return token;
        }
        private void AddCacheForSecure()
        {
            List<string> list = new List<string>();
            list.Add("GetUsersQuery");
            list.Add("GetUserQuery");
            list.Add("CreateUserCommand");
            list.Add("UpdateUserCommand");
            list.Add("DeleteUserCommand");
            _cacheManager.Add("UserIdForClaim=1", list);
        }
        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            AddCacheForSecure();
            var request = new RestRequest("api/Users/" + url, Method.GET).AddObject(this);
            var response = await _client.GetAsync<IEnumerable<UserDto>>(request);
            return response;
        }
        public async Task<UserDto> GetUserByIdAsync()
        {
            AddCacheForSecure();
            var request = new RestRequest("api/Users/" + url, Method.GET).AddObject(this);
            request.AddParameter("userId", userId);

            var response = await _client.GetAsync<UserDto>(request);

            return response;
        }
        public async Task<string> PostUserAsync()
        {
            AddCacheForSecure();
            var request = new RestRequest("api/Users", Method.POST).AddObject(this);
            request.AddJsonBody(jsonUser);
            var response = await _client.PostAsync<string>(request);

            return response;
        }
        public async Task<string> PutUserAsync()
        {
            AddCacheForSecure();
            var request = new RestRequest("api/Users", Method.PUT).AddObject(this);
            request.AddJsonBody(jsonUser);
            var response = await _client.PutAsync<string>(request);

            return response;
        }
        public async Task<string> DeleteUserAsync()
        {
            AddCacheForSecure();
            var request = new RestRequest("api/Users", Method.DELETE).AddObject(this);
            request.AddJsonBody(userId);
            var response = await _client.DeleteAsync<string>(request);

            return response;
        }
    }
}
