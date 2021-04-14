namespace Core.Utilities.Security.Jwt
{
    using Core.Entities.Concrete;

    public interface ITokenHelper
    {
        TAccessToken CreateToken<TAccessToken>(User user)
          where TAccessToken : IAccessToken, new();
    }
}
