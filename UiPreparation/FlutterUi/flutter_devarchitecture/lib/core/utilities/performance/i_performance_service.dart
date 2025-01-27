abstract class IPerformanceService {
  Future<void> initialize();
  Future<void> startTrace(String traceName);
  Future<void> stopTrace(String traceName);
  Future<void> incrementMetric(
      String traceName, String metricName, int incrementBy);
}
