abstract class IAnalyticsService {
  Future<void> initialize();
  Future<void> logEvent(String name, {Map<String, Object>? parameters});
  Future<void> setUserId(String userId);
  Future<void> setUserProperty(String name, String value);
}
