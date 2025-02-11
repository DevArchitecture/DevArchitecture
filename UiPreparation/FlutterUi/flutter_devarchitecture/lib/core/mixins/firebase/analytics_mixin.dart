import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';

import '../../utilities/analytics/i_analytics_service.dart';

mixin AnalyticsMixin<T extends StatefulWidget> on State<T> {
  final IAnalyticsService _analyticsService =
      GetIt.instance<IAnalyticsService>();

  @override
  void initState() {
    super.initState();
    _initializeAnalytics();
  }

  Future<void> _initializeAnalytics() async {
    await _analyticsService.initialize();
  }

  void logEvent(String name, {Map<String, Object>? parameters}) {
    _analyticsService.logEvent(name, parameters: parameters);
  }

  void setUserId(String userId) {
    _analyticsService.setUserId(userId);
  }

  void setUserProperty(String name, String value) {
    _analyticsService.setUserProperty(name, value);
  }
}
