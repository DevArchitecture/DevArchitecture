import 'package:device_info_plus/device_info_plus.dart';
import 'package:flutter/foundation.dart';

import 'i_device_information.dart';

class DeviceInfoPlus implements IDeviceInformation {
  DeviceInfoPlugin deviceInfo = DeviceInfoPlugin();

  @override
  Future<Map<String, dynamic>> getAndroidDeviceInfo() async {
    AndroidDeviceInfo androidInfo = await deviceInfo.androidInfo;
    return androidInfo.data;
  }

  @override
  Future<Map<String, dynamic>> getIOSDeviceInfo() async {
    IosDeviceInfo iosInfo = await deviceInfo.iosInfo;
    return iosInfo.data;
  }

  @override
  Future<Map<String, dynamic>> getLinuxDeviceInfo() async {
    LinuxDeviceInfo linuxInfo = await deviceInfo.linuxInfo;
    return linuxInfo.data;
  }

  @override
  Future<Map<String, dynamic>> getMacDeviceInfo() async {
    MacOsDeviceInfo macOsInfo = await deviceInfo.macOsInfo;
    return macOsInfo.data;
  }

  @override
  Future<Map<String, dynamic>> getWebDeviceInfo() async {
    WebBrowserInfo webInfo = await deviceInfo.webBrowserInfo;
    return webInfo.data;
  }

  @override
  Future<Map<String, dynamic>> getWindowsDeviceInfo() async {
    WindowsDeviceInfo windowsInfo = await deviceInfo.windowsInfo;
    return windowsInfo.data;
  }

  @override
  Future<Map<String, dynamic>> getPlatformBaseDeviceInfo() {
    if (kIsWeb) {
      return getWebDeviceInfo();
    }

    if (defaultTargetPlatform == TargetPlatform.android) {
      return getAndroidDeviceInfo();
    }

    if (defaultTargetPlatform == TargetPlatform.iOS) {
      return getIOSDeviceInfo();
    }

    if (defaultTargetPlatform == TargetPlatform.linux) {
      return getLinuxDeviceInfo();
    }

    if (defaultTargetPlatform == TargetPlatform.macOS) {
      return getMacDeviceInfo();
    }
    return getWindowsDeviceInfo();
  }
}
