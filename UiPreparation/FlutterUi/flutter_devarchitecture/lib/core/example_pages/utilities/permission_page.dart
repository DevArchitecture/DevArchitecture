import 'package:flutter/material.dart';
import '/core/di/core_initializer.dart';

import '../../../layouts/base_scaffold.dart';

class PermissionPage extends StatefulWidget {
  @override
  _PermissionPageState createState() => _PermissionPageState();
}

class _PermissionPageState extends State<PermissionPage> {
  late final _permissionHandler = CoreInitializer()
      .coreContainer
      .permissionHandler; // PermissionHandler burada başlatılıyor
  late final _screenMessage = CoreInitializer()
      .coreContainer
      .screenMessage; // ScreenMessage için enjekte edilen sınıf

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Center(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            children: [
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () async {
                    bool granted =
                        await _permissionHandler.requestCameraPermission();
                    _showPermissionStatus('Kamera İzni', granted);
                  },
                  child: Text('Kamera İzni İste'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () async {
                    bool granted =
                        await _permissionHandler.requestLocationPermission();
                    _showPermissionStatus('Konum İzni', granted);
                  },
                  child: Text('Konum İzni İste'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () async {
                    bool granted =
                        await _permissionHandler.requestMicrophonePermission();
                    _showPermissionStatus('Mikrofon İzni', granted);
                  },
                  child: Text('Mikrofon İzni İste'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () async {
                    bool granted =
                        await _permissionHandler.requestPhotosPermission();
                    _showPermissionStatus('Fotoğraflar İzni', granted);
                  },
                  child: Text('Fotoğraflar İzni İste'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () async {
                    bool granted =
                        await _permissionHandler.requestContactsPermission();
                    _showPermissionStatus('Kişiler İzni', granted);
                  },
                  child: Text('Kişiler İzni İste'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () async {
                    bool granted =
                        await _permissionHandler.requestBluetoothPermission();
                    _showPermissionStatus('Bluetooth İzni', granted);
                  },
                  child: Text('Bluetooth İzni İste'),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  void _showPermissionStatus(String permission, bool granted) {
    final status = granted ? 'verildi' : 'reddedildi';
    _screenMessage.getInfoMessage('$permission $status.');
  }
}
