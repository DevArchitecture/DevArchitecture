abstract class IDeviceInformation {
  Future<Map<String, dynamic>> getAndroidDeviceInfo();

  Future<Map<String, dynamic>> getIOSDeviceInfo();

  Future<Map<String, dynamic>> getWindowsDeviceInfo();

  Future<Map<String, dynamic>> getLinuxDeviceInfo();

  Future<Map<String, dynamic>> getWebDeviceInfo();

  Future<Map<String, dynamic>> getMacDeviceInfo();

  Future<Map<String, dynamic>> getPlatformBaseDeviceInfo();
}
