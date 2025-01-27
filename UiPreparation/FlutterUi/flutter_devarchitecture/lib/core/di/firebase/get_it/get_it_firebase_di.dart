import '/core/di/firebase/i_firebase_container.dart';
import '/core/utilities/analytics/firebase_analytics_service.dart';
import '/core/utilities/performance/i_performance_service.dart';
import 'package:get_it/get_it.dart';

import '../../../utilities/analytics/i_analytics_service.dart';
import '../../../utilities/crashlytics/firebase_crashlytics_service.dart';
import '../../../utilities/crashlytics/i_crashlytics_service.dart';
import '../../../utilities/performance/firebase_performance_service.dart';
import '../../../utilities/push_notification/firebase_notification_service.dart';
import '../../../utilities/push_notification/i_push_notification.dart';
import '../../../utilities/remote_config/firebase_remote_config_service.dart';
import '../../../utilities/remote_config/i_remote_config_service.dart';

class GetItFirebaseContainer implements IFirebaseContainer {
  late GetIt _getIt;
  void init() {
    _getIt = GetIt.instance;
    setUp();
  }

  GetItFirebaseContainer() {
    init();
  }

  // Utilities -> firebase
  @override
  late IPushNotificationService pushNotificationService;

  @override
  late ICrashlyticsService crashlyticsService;

  @override
  late IAnalyticsService analyticsService;

  @override
  late IRemoteConfigService remoteConfigService;

  @override
  late IPerformanceService performanceService;

  @override
  setUp() {
    //* firebase -> push notification
    checkIfUnRegistered<IPushNotificationService>((() {
      pushNotificationService =
          _getIt.registerSingleton<IPushNotificationService>(
              FirebaseNotificationService());
    }));

    //* firebase -> crashlytics
    checkIfUnRegistered<ICrashlyticsService>((() {
      crashlyticsService = _getIt
          .registerSingleton<ICrashlyticsService>(FirebaseCrashlyticsService());
    }));

    //* firebase -> analytics
    checkIfUnRegistered<IAnalyticsService>((() {
      analyticsService = _getIt
          .registerSingleton<IAnalyticsService>(FirebaseAnalyticsService());
    }));

    //* firebase -> remote config
    checkIfUnRegistered<IRemoteConfigService>((() {
      remoteConfigService = _getIt.registerSingleton<IRemoteConfigService>(
          FirebaseRemoteConfigService());
    }));

    //* firebase -> performance
    checkIfUnRegistered<IPerformanceService>((() {
      performanceService = _getIt
          .registerSingleton<IPerformanceService>(FirebasePerformanceService());
    }));
  }

  @override
  void checkIfUnRegistered<T extends Object>(Function register) {
    if (!_getIt.isRegistered<T>()) {
      register.call();
    }
  }
}
