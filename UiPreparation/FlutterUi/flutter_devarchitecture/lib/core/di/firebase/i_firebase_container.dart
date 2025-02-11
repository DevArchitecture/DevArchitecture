import '/core/utilities/remote_config/i_remote_config_service.dart';

import '../../utilities/analytics/i_analytics_service.dart';
import '../../utilities/crashlytics/i_crashlytics_service.dart';
import '../../utilities/performance/i_performance_service.dart';
import '../../utilities/push_notification/i_push_notification.dart';

abstract class IFirebaseContainer {
  //* push notification
  late IPushNotificationService pushNotificationService;

  //* crashlytics
  late ICrashlyticsService crashlyticsService;

  //* analytics
  late IAnalyticsService analyticsService;

  //* remote config
  late IRemoteConfigService remoteConfigService;

  //* performance
  late IPerformanceService performanceService;

  setUp();
  void checkIfUnRegistered<T extends Object>(Function register);
}
