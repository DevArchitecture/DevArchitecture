import 'package:flutter/material.dart';

abstract class IQRCodeScannerService {
  Future<String?> scanQrCode(GlobalKey qrKey);
}
