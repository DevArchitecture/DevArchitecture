import 'i_permission_handler.dart';
import 'package:permission_handler/permission_handler.dart';

class PermissionHandler implements IPermissionHandler {
  @override
  Future<bool> requestCameraPermission() async {
    return await _requestPermission(Permission.camera);
  }

  @override
  Future<bool> requestContactsPermission() async {
    return await _requestPermission(Permission.contacts);
  }

  @override
  Future<bool> requestLocationPermission() async {
    return await _requestPermission(Permission.location);
  }

  @override
  Future<bool> requestLocationAlwaysPermission() async {
    return await _requestPermission(Permission.locationAlways);
  }

  @override
  Future<bool> requestLocationWhenInUsePermission() async {
    return await _requestPermission(Permission.locationWhenInUse);
  }

  @override
  Future<bool> requestMicrophonePermission() async {
    return await _requestPermission(Permission.microphone);
  }

  @override
  Future<bool> requestPhonePermission() async {
    return await _requestPermission(Permission.phone);
  }

  @override
  Future<bool> requestSmsPermission() async {
    return await _requestPermission(Permission.sms);
  }

  @override
  Future<bool> requestPhotosPermission() async {
    return await _requestPermission(Permission.photos);
  }

  @override
  Future<bool> requestPhotosAddOnlyPermission() async {
    return await _requestPermission(Permission.photosAddOnly);
  }

  @override
  Future<bool> requestRemindersPermission() async {
    return await _requestPermission(Permission.reminders);
  }

  @override
  Future<bool> requestSensorsPermission() async {
    return await _requestPermission(Permission.sensors);
  }

  @override
  Future<bool> requestSpeechPermission() async {
    return await _requestPermission(Permission.speech);
  }

  @override
  Future<bool> requestStoragePermission() async {
    return await _requestPermission(Permission.storage);
  }

  @override
  Future<bool> requestBluetoothPermission() async {
    return await _requestPermission(Permission.bluetooth);
  }

  @override
  Future<bool> requestBluetoothScanPermission() async {
    return await _requestPermission(Permission.bluetoothScan);
  }

  @override
  Future<bool> requestBluetoothAdvertisePermission() async {
    return await _requestPermission(Permission.bluetoothAdvertise);
  }

  @override
  Future<bool> requestBluetoothConnectPermission() async {
    return await _requestPermission(Permission.bluetoothConnect);
  }

  @override
  Future<bool> requestNotificationPermission() async {
    return await _requestPermission(Permission.notification);
  }

  @override
  Future<bool> requestMediaLibraryPermission() async {
    return await _requestPermission(Permission.mediaLibrary);
  }

  @override
  Future<bool> requestAppTrackingTransparencyPermission() async {
    return await _requestPermission(Permission.appTrackingTransparency);
  }

  Future<bool> _requestPermission(Permission permission) async {
    if (await permission.isGranted) {
      return true;
    } else {
      final result = await permission.request();
      return result.isGranted;
    }
  }
}
