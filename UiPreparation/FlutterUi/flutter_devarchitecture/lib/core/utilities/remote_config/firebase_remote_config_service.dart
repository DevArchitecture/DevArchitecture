import 'package:firebase_remote_config/firebase_remote_config.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter_devarchitecture/core/constants/core_messages.dart';
import '../../di/core_initializer.dart';
import 'i_remote_config_service.dart';

class FirebaseRemoteConfigService implements IRemoteConfigService {
  final FirebaseRemoteConfig _remoteConfig = FirebaseRemoteConfig.instance;

  @override
  Future<void> initialize() async {
    await _remoteConfig.setConfigSettings(
      RemoteConfigSettings(
        fetchTimeout: const Duration(seconds: 10),
        minimumFetchInterval: const Duration(hours: 1),
      ),
    );
    await fetchAndActivate();
  }

  @override
  Future<void> fetchAndActivate() async {
    try {
      await _remoteConfig.fetchAndActivate();
    } catch (e) {
      CoreInitializer()
          .coreContainer
          .screenMessage
          .getErrorMessage(CoreMessages.customerDefaultErrorMessage);
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
    }
  }

  @override
  String getString(String key) {
    return _remoteConfig.getString(key);
  }

  @override
  int getInt(String key) {
    return _remoteConfig.getInt(key);
  }

  @override
  bool getBool(String key) {
    return _remoteConfig.getBool(key);
  }

  @override
  double getDouble(String key) {
    return _remoteConfig.getDouble(key);
  }
}
