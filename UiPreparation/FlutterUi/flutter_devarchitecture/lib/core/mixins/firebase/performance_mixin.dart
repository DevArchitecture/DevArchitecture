import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';

import '../../utilities/performance/i_performance_service.dart';

mixin PerformanceMixin<T extends StatefulWidget> on State<T> {
  final IPerformanceService _performanceService =
      GetIt.instance<IPerformanceService>();

  @override
  void initState() {
    super.initState();
    _initializePerformance();
  }

  Future<void> _initializePerformance() async {
    await _performanceService.initialize();
  }

  Future<void> startTrace(String traceName) {
    return _performanceService.startTrace(traceName);
  }

  Future<void> stopTrace(String traceName) {
    return _performanceService.stopTrace(traceName);
  }

  Future<void> incrementMetric(
      String traceName, String metricName, int incrementBy) {
    return _performanceService.incrementMetric(
        traceName, metricName, incrementBy);
  }
}
