import 'package:firebase_performance/firebase_performance.dart';
import 'i_performance_service.dart';

class FirebasePerformanceService implements IPerformanceService {
  final FirebasePerformance _performance = FirebasePerformance.instance;
  final Map<String, Trace> _traces = {};

  @override
  Future<void> initialize() async {
    await _performance.setPerformanceCollectionEnabled(true);
  }

  @override
  Future<void> startTrace(String traceName) async {
    if (!_traces.containsKey(traceName)) {
      Trace trace = _performance.newTrace(traceName);
      await trace.start();
      _traces[traceName] = trace;
    }
  }

  @override
  Future<void> stopTrace(String traceName) async {
    if (_traces.containsKey(traceName)) {
      Trace? trace = _traces[traceName];
      await trace?.stop();
      _traces.remove(traceName);
    }
  }

  @override
  Future<void> incrementMetric(
      String traceName, String metricName, int incrementBy) async {
    if (_traces.containsKey(traceName)) {
      Trace? trace = _traces[traceName];
      trace?.incrementMetric(metricName, incrementBy);
    }
  }
}
