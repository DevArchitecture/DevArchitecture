import 'dart:io';

import '/core/helpers/http_override.dart';

import 'app_config.dart';

class StagingConfig implements AppConfig {
  static final StagingConfig _singleton = StagingConfig._internal();

  factory StagingConfig() {
    HttpOverrides.global = MyHttpOverrides();
    return _singleton;
  }

  StagingConfig._internal();

  @override
  String get apiUrl =>
      'https://10.0.2.2:5001/api/v1'; // this ip for android testing. Set by backend url

  @override
  String get name => 'staging';
}
