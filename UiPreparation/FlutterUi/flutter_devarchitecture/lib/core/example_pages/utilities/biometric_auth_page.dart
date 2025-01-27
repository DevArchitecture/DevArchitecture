import 'package:flutter/material.dart';

import '../../di/core_initializer.dart';
import '../../../layouts/base_scaffold.dart';

class BiometricAuthPage extends StatefulWidget {
  @override
  _BiometricAuthPageState createState() => _BiometricAuthPageState();
}

class _BiometricAuthPageState extends State<BiometricAuthPage> {
  late final biometricAuth = CoreInitializer().coreContainer.biometricAuth;
  String _fingerprintStatus = 'Unknown';
  String _faceStatus = 'Unknown';
  String _irisStatus = 'Unknown';
  String _authResult = 'Not Authenticated';

  Future<void> _checkFingerprintPermission() async {
    final hasPermission = await biometricAuth.requestFingerprintPermission();
    if (!hasPermission) {
      setState(() {
        _fingerprintStatus = 'Fingerprint not available or permission denied';
      });
    } else {
      setState(() {
        _fingerprintStatus = 'Fingerprint available and permission granted';
      });
      await _authenticateWithFingerprint();
    }
  }

  Future<void> _checkFacePermission() async {
    final hasPermission = await biometricAuth.requestFacePermission();
    if (!hasPermission) {
      setState(() {
        _faceStatus = 'Face recognition not available or permission denied';
      });
    } else {
      setState(() {
        _faceStatus = 'Face recognition available and permission granted';
      });
      await _authenticateWithFace();
    }
  }

  Future<void> _checkIrisPermission() async {
    final hasPermission = await biometricAuth.requestIrisPermission();
    if (!hasPermission) {
      setState(() {
        _irisStatus = 'Iris recognition not available or permission denied';
      });
    } else {
      setState(() {
        _irisStatus = 'Iris recognition available and permission granted';
      });
      await _authenticateWithIris();
    }
  }

  Future<void> _authenticateWithFingerprint() async {
    final authenticated = await biometricAuth.authenticate(
      localizedReason: 'Please authenticate using your fingerprint',
    );
    setState(() {
      _authResult = authenticated
          ? 'Fingerprint authenticated successfully'
          : 'Failed to authenticate with fingerprint';
    });
  }

  Future<void> _authenticateWithFace() async {
    final authenticated = await biometricAuth.authenticate(
      localizedReason: 'Please authenticate using your face',
    );
    setState(() {
      _authResult = authenticated
          ? 'Face authenticated successfully'
          : 'Failed to authenticate with face';
    });
  }

  Future<void> _authenticateWithIris() async {
    final authenticated = await biometricAuth.authenticate(
      localizedReason: 'Please authenticate using your iris',
    );
    setState(() {
      _authResult = authenticated
          ? 'Iris authenticated successfully'
          : 'Failed to authenticate with iris';
    });
  }

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            ElevatedButton(
              onPressed: _checkFingerprintPermission,
              child: Text('Check Fingerprint Permission & Authenticate'),
            ),
            Text('Fingerprint Status: $_fingerprintStatus'),
            SizedBox(height: 16),
            ElevatedButton(
              onPressed: _checkFacePermission,
              child: Text('Check Face Recognition Permission & Authenticate'),
            ),
            Text('Face Status: $_faceStatus'),
            SizedBox(height: 16),
            ElevatedButton(
              onPressed: _checkIrisPermission,
              child: Text('Check Iris Recognition Permission & Authenticate'),
            ),
            Text('Iris Status: $_irisStatus'),
            SizedBox(height: 16),
            Text('Authentication Result: $_authResult'),
          ],
        ),
      ),
      isDrawer: true, // Eğer Drawer kullanılacaksa true bırakabilirsiniz
    );
  }
}
