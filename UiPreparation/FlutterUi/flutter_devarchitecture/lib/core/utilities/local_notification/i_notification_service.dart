import 'package:flutter/material.dart';

abstract class INotificationService {
  void showNotification(BuildContext context, String message, String title);
  void showNotificationWithSound(
      BuildContext context, String message, String title);
  void showNotificationWithVibration(
      BuildContext context, String message, String title);
  void showNotificationWithSoundAndVibration(
      BuildContext context, String message, String title);
  void schedulePeriodicNotification(
      BuildContext context, String message, String title, Duration interval);
}
