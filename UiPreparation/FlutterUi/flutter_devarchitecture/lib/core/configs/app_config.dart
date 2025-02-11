import 'dev_config.dart';
import 'prod_config.dart';
import 'staging_config.dart';

abstract class AppConfig {
  String get apiUrl;
  String get name;
}

//? flutter build web --dart-define ENV=staging --web-renderer html --release
//? flutter build apk --dart-define ENV=prod --release
//? flutter build windows --dart-define ENV=prod --release
//? flutter build macos --dart-define ENV=prod --dart-define=FIREBASE=false --verbose --target-arch=arm64 --release
//? flutter build macos --dart-define ENV=prod --dart-define=FIREBASE=false --verbose --target-arch=x86_64 --release

//? flutter run --dart-define ENV=dev
//? flutter run --dart-define ENV=staging
//? flutter run --dart-define ENV=prod
//? flutter run --dart-define ENV=prod --dart-define=FIREBASE=false

AppConfig getConfig() {
  const environmentParameter = String.fromEnvironment('ENV');
  switch (environmentParameter) {
    case 'dev':
      return DevConfig();
    case 'staging':
      return StagingConfig();
    case 'prod':
      return ProdConfig();
    default:
      return DevConfig();
  }
}

var appConfig = getConfig();
