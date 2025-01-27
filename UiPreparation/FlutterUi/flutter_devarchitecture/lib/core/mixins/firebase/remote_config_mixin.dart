import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';

import '../../utilities/remote_config/i_remote_config_service.dart';

mixin RemoteConfigMixin<T extends StatefulWidget> on State<T> {
  final IRemoteConfigService _remoteConfigService =
      GetIt.instance<IRemoteConfigService>();

  @override
  void initState() {
    super.initState();
    _initializeRemoteConfig();
  }

  Future<void> _initializeRemoteConfig() async {
    await _remoteConfigService.initialize();
  }

  String getConfigString(String key) {
    return _remoteConfigService.getString(key);
  }

  int getConfigInt(String key) {
    return _remoteConfigService.getInt(key);
  }

  bool getConfigBool(String key) {
    return _remoteConfigService.getBool(key);
  }

  double getConfigDouble(String key) {
    return _remoteConfigService.getDouble(key);
  }
}
