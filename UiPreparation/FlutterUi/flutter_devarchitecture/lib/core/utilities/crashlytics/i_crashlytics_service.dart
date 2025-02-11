abstract class ICrashlyticsService {
  Future<void> initialize();
  Future<void> log(String message);
  Future<void> recordError(dynamic exception, StackTrace stack);
  Future<void> setUserIdentifier(String identifier);
}
