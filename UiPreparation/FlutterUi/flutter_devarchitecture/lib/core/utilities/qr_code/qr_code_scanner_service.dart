import 'dart:async';
import 'package:flutter/material.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';
import 'i_qr_code_scanner_service.dart';

class QRCodeScannerService implements IQRCodeScannerService {
  @override
  Future<String?> scanQrCode(GlobalKey qrKey) async {
    final completer = Completer<String?>();
    QRViewController? controller;

    final qrView = QRView(
      key: qrKey,
      onQRViewCreated: (qrController) {
        controller = qrController;
        try {
          controller!.scannedDataStream.first.then((barcode) {
            completer.complete(barcode.code);
            Navigator.of(qrKey.currentContext!).pop();
          });
        } catch (e) {
          completer.completeError(e);
        }
      },
      overlay: QrScannerOverlayShape(
        borderColor: Colors.red,
        borderRadius: 10,
        borderLength: 30,
        borderWidth: 10,
        cutOutSize: MediaQuery.of(qrKey.currentContext!).size.width * 0.8,
      ),
    );

    await showDialog<void>(
      context: qrKey.currentContext!,
      builder: (context) => Dialog(
        child: SizedBox(
          width: 300,
          height: 300,
          child: qrView,
        ),
      ),
    );

    controller?.dispose();

    return completer.future;
  }
}
