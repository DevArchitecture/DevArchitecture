import 'package:firebase_analytics/firebase_analytics.dart';
import 'i_analytics_service.dart';

class FirebaseAnalyticsService implements IAnalyticsService {
  final FirebaseAnalytics _analytics = FirebaseAnalytics.instance;

  @override
  Future<void> initialize() async {
    // Firebase Analytics initialization, if any custom setup is needed
  }

  @override
  Future<void> logEvent(String name, {Map<String, Object>? parameters}) async {
    await _analytics.logEvent(name: name, parameters: parameters);
  }

  @override
  Future<void> setUserId(String userId) async {
    await _analytics.setUserId(id: userId);
  }

  @override
  Future<void> setUserProperty(String name, String value) async {
    await _analytics.setUserProperty(name: name, value: value);
  }
}
