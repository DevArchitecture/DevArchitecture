abstract class IPushNotificationService {
  Future<void> initialize();
  Future<String?> getToken();
  Stream<String> get onMessageReceived;
}
