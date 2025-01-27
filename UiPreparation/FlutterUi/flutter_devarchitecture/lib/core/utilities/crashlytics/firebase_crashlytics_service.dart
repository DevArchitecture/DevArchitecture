import 'package:firebase_crashlytics/firebase_crashlytics.dart';
import 'package:flutter/material.dart';
import 'i_crashlytics_service.dart';

class FirebaseCrashlyticsService implements ICrashlyticsService {
  final FirebaseCrashlytics _crashlytics = FirebaseCrashlytics.instance;

  @override
  Future<void> initialize() async {
    await _crashlytics.setCrashlyticsCollectionEnabled(true);
    FlutterError.onError = _crashlytics.recordFlutterError;
  }

  @override
  Future<void> log(String message) async {
    await _crashlytics.log(message);
  }

  @override
  Future<void> recordError(dynamic exception, StackTrace stack) async {
    await _crashlytics.recordError(exception, stack);
  }

  @override
  Future<void> setUserIdentifier(String identifier) async {
    await _crashlytics.setUserIdentifier(identifier);
  }
}
