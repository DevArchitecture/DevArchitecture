import 'package:firebase_messaging/firebase_messaging.dart';
import 'i_push_notification.dart';
import 'dart:async';

class FirebaseNotificationService implements IPushNotificationService {
  final FirebaseMessaging _firebaseMessaging = FirebaseMessaging.instance;
  final StreamController<String> _messageStreamController =
      StreamController<String>.broadcast();

  @override
  Future<void> initialize() async {
    // Request permissions
    await _firebaseMessaging.requestPermission();

    // Configure message handlers
    FirebaseMessaging.onMessage.listen((RemoteMessage message) {
      if (message.notification != null) {
        _messageStreamController.add(message.notification!.body!);
      }
    });

    FirebaseMessaging.onMessageOpenedApp.listen((RemoteMessage message) {
      if (message.notification != null) {
        _messageStreamController.add(message.notification!.body!);
      }
    });
  }

  @override
  Future<String?> getToken() async {
    return await _firebaseMessaging.getToken();
  }

  @override
  Stream<String> get onMessageReceived => _messageStreamController.stream;
}
