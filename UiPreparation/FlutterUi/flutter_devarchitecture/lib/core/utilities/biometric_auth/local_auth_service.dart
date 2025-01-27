import 'package:local_auth/local_auth.dart';
import '/core/utilities/biometric_auth/i_biometric_auth.dart';

class LocalAuthService implements IBiometricAuth {
  final LocalAuthentication _localAuthentication = LocalAuthentication();

  @override
  Future<bool> canCheckBiometrics() async {
    try {
      return await _localAuthentication.canCheckBiometrics;
    } catch (e) {
      return false;
    }
  }

  @override
  Future<List<CustomBiometricType>> getAvailableBiometrics() async {
    try {
      List<CustomBiometricType> biometricTypes = [];
      List<BiometricType> availableBiometrics =
          await _localAuthentication.getAvailableBiometrics();
      for (var biometric in availableBiometrics) {
        switch (biometric) {
          case BiometricType.fingerprint:
            biometricTypes.add(CustomBiometricType.fingerprint);
            break;
          case BiometricType.face:
            biometricTypes.add(CustomBiometricType.face);
            break;
          case BiometricType.iris:
            biometricTypes.add(CustomBiometricType.iris);
            break;
          default:
            break;
        }
      }
      return biometricTypes;
    } catch (e) {
      return <CustomBiometricType>[];
    }
  }

  @override
  Future<bool> authenticate({required String localizedReason}) async {
    try {
      return await _localAuthentication.authenticate(
        localizedReason: localizedReason,
        options: const AuthenticationOptions(
          useErrorDialogs: true,
          stickyAuth: true,
        ),
      );
    } catch (e) {
      return false;
    }
  }

  @override
  Future<bool> requestFingerprintPermission() async {
    return await _requestBiometricPermission(CustomBiometricType.fingerprint);
  }

  @override
  Future<bool> requestFacePermission() async {
    return await _requestBiometricPermission(CustomBiometricType.face);
  }

  @override
  Future<bool> requestIrisPermission() async {
    return await _requestBiometricPermission(CustomBiometricType.iris);
  }

  Future<bool> _requestBiometricPermission(
      CustomBiometricType biometricType) async {
    try {
      final availableBiometrics = await getAvailableBiometrics();
      return availableBiometrics.contains(biometricType);
    } catch (e) {
      return false;
    }
  }
}
