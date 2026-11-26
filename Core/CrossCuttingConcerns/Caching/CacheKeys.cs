namespace Core.CrossCuttingConcerns.Caching
{
    public static class CacheKeys
    {
        public static string UserIdForClaim => "UserIdForClaim";
        public static string RevokedTokenForJti => "RevokedTokenForJti";
    }
}