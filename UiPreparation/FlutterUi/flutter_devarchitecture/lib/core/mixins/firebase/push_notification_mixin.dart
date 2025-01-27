import 'package:flutter/material.dart';
import '../../di/firebase/firebase_initializer.dart';

mixin PushNotificationMixin<T extends StatefulWidget> on State<T> {
  final _pushNotificationService =
      FirebaseInitializer().firebaseContainer.pushNotificationService;
  late Stream<String> _messageStream;

  @override
  void initState() {
    super.initState();
    _initializePushNotifications();
  }

  void _initializePushNotifications() {
    _pushNotificationService.initialize();
    _messageStream = _pushNotificationService.onMessageReceived;
    _messageStream.listen((String message) {
      onMessageReceived(message);
    });
  }

  void onMessageReceived(String message);
}
