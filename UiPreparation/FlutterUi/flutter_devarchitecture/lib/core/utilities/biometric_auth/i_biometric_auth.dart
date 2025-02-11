abstract class IBiometricAuth {
  Future<bool> canCheckBiometrics();
  Future<List<CustomBiometricType>> getAvailableBiometrics();
  Future<bool> authenticate({required String localizedReason});

  // Her bir biyometrik tür için ayrı izin kontrolü
  Future<bool> requestFingerprintPermission();
  Future<bool> requestFacePermission();
  Future<bool> requestIrisPermission();
}

enum CustomBiometricType {
  fingerprint,
  face,
  iris,
}
