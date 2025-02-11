import 'package:flutter/material.dart';
import '/core/di/core_initializer.dart';

import '../../../layouts/base_scaffold.dart';

class QRCodeScannerPage extends StatefulWidget {
  @override
  _QRCodeScannerPageState createState() => _QRCodeScannerPageState();
}

class _QRCodeScannerPageState extends State<QRCodeScannerPage> {
  final GlobalKey _qrKey = GlobalKey(debugLabel: 'QR');
  final _qrScannerService = CoreInitializer()
      .coreContainer
      .qrCodeScannerService; // QRCodeScannerService'i oluşturun
  late final _screenMessage = CoreInitializer()
      .coreContainer
      .screenMessage; // ScreenMessage'ı inject edin

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Center(
        child: ElevatedButton(
          onPressed: _scanQRCode,
          child: Text('QR Kodunu Tara'),
        ),
      ),
    );
  }

  Future<void> _scanQRCode() async {
    try {
      final result = await _qrScannerService.scanQrCode(_qrKey);
      if (result != null) {
        _screenMessage.getSuccessMessage('QR kodu tarandı: $result');
      } else {
        _screenMessage.getWarningMessage('QR kodu taranamadı.');
      }
    } catch (e) {
      _screenMessage.getErrorMessage('Tarama sırasında bir hata oluştu: $e');
    }
  }
}
