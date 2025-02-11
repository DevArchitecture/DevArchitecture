import 'dart:async';

abstract class ILogger {
  void logError(String message);
  void logWarning(String message);
  void logInfo(String message);
  void logSuccess(String message);
  void logDebug(String message);
  FutureOr<T> logTraceWithResultAsync<T>(
      String message, FutureOr<T> Function() action);
  Future<void> logTraceAsync(String message, FutureOr<void> Function() action);
  T logTraceWithResult<T>(String message, T Function() action);
  void logTrace(String message, void Function() action);
}
