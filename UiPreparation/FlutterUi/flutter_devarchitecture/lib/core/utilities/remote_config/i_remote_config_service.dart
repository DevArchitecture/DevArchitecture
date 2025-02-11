abstract class IRemoteConfigService {
  Future<void> initialize();
  Future<void> fetchAndActivate();
  String getString(String key);
  int getInt(String key);
  bool getBool(String key);
  double getDouble(String key);
}
