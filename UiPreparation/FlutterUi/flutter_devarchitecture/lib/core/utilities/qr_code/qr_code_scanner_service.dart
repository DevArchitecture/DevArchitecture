import 'dart:async';
import 'package:flutter/material.dart';
import 'i_qr_code_scanner_service.dart';

class QRCodeScannerService implements IQRCodeScannerService {
  @override
  Future<String?> scanQrCode(GlobalKey qrKey) async {
    // Web-first temporary fallback: keeps app bootable without native scanner plugin.
    final context = qrKey.currentContext;
    if (context == null) {
      return null;
    }

    await showDialog<void>(
      context: context,
      builder: (_) => const AlertDialog(
        title: Text('QR Scanner'),
        content: Text('QR scanner is not enabled in this build.'),
      ),
    );

    return Future<String?>.value(null);
  }
}
