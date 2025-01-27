abstract class IPermissionHandler {
  Future<bool> requestCameraPermission();
  Future<bool> requestContactsPermission();
  Future<bool> requestLocationPermission();
  Future<bool> requestLocationAlwaysPermission();
  Future<bool> requestLocationWhenInUsePermission();
  Future<bool> requestMicrophonePermission();
  Future<bool> requestPhonePermission();
  Future<bool> requestSmsPermission();
  Future<bool> requestPhotosPermission();
  Future<bool> requestPhotosAddOnlyPermission();
  Future<bool> requestRemindersPermission();
  Future<bool> requestSensorsPermission();
  Future<bool> requestSpeechPermission();
  Future<bool> requestStoragePermission();
  Future<bool> requestBluetoothPermission();
  Future<bool> requestBluetoothScanPermission();
  Future<bool> requestBluetoothAdvertisePermission();
  Future<bool> requestBluetoothConnectPermission();
  Future<bool> requestNotificationPermission();
  Future<bool> requestMediaLibraryPermission();
  Future<bool> requestAppTrackingTransparencyPermission();
}
