import 'package:logger/logger.dart';
import 'dart:async';
import 'i_logger.dart';

class LoggerImpl implements ILogger {
  LoggerImpl()
      : _logger = Logger(
          printer: PrettyPrinter(
            methodCount: 0,
            errorMethodCount: 5,
            lineLength: 50,
            colors: true,
            printEmojis: true,
            dateTimeFormat: DateTimeFormat.onlyTime,
          ),
        );

  final Logger _logger;

  @override
  void logError(String message) {
    _logger.e(message);
  }

  @override
  void logWarning(String message) {
    _logger.w(message);
  }

  @override
  void logInfo(String message) {
    _logger.i(message);
  }

  @override
  void logSuccess(String message) {
    _logger.i('SUCCESS: $message');
  }

  @override
  void logDebug(String message) {
    _logger.d(message);
  }

  @override
  FutureOr<T> logTraceWithResultAsync<T>(
      String message, FutureOr<T> Function() action) async {
    final start = DateTime.now();
    _logger.d('TRACE Start: $message');
    final result = await action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d('TRACE End: $message, Duration: ${duration.inMilliseconds} ms');
    return result;
  }

  @override
  Future<void> logTraceAsync(
      String message, FutureOr<void> Function() action) async {
    final start = DateTime.now();
    _logger.d('TRACE Start: $message');
    await action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d('TRACE End: $message, Duration: ${duration.inMilliseconds} ms');
  }

  @override
  T logTraceWithResult<T>(String message, T Function() action) {
    final start = DateTime.now();
    _logger.d('TRACE Start: $message');
    final result = action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d('TRACE End: $message, Duration: ${duration.inMilliseconds} ms');
    return result;
  }

  @override
  void logTrace(String message, void Function() action) {
    final start = DateTime.now();
    _logger.d('TRACE Start: $message');
    action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d('TRACE End: $message, Duration: ${duration.inMilliseconds} ms');
  }
}
