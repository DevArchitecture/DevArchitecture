import 'app_config.dart';

class ProdConfig implements AppConfig {
  static final ProdConfig _singleton = ProdConfig._internal();

  factory ProdConfig() {
    return _singleton;
  }
  ProdConfig._internal();

  @override
  String get apiUrl => 'https://localhost:5001/api/v1'; // Set by backend url

  @override
  String get name => 'prod';
}
