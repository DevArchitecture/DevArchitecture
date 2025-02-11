import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';

import '../../utilities/crashlytics/i_crashlytics_service.dart';

mixin CrashlyticsMixin<T extends StatefulWidget> on State<T> {
  final ICrashlyticsService _crashlyticsService =
      GetIt.instance<ICrashlyticsService>();

  @override
  void initState() {
    super.initState();
    _initializeCrashlytics();
  }

  Future<void> _initializeCrashlytics() async {
    await _crashlyticsService.initialize();
  }

  void logCrashlytics(String message) {
    _crashlyticsService.log(message);
  }

  void recordError(dynamic exception, StackTrace stack) {
    _crashlyticsService.recordError(exception, stack);
  }

  void setUserIdentifier(String identifier) {
    _crashlyticsService.setUserIdentifier(identifier);
  }
}
