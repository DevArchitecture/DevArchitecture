import 'dart:async';
import 'package:flutter/material.dart';
import 'package:internet_connection_checker_plus/internet_connection_checker_plus.dart';

import 'i_internet_connection.dart';

class InternetConnectionWithChecker implements IInternetConnection {
  StreamSubscription<InternetStatus>? _subscription;
  BuildContext? _popupContext;
  bool _isPopupVisible = false;

  Future<bool> isConnected() async {
    return await InternetConnection().hasInternetAccess;
  }

  Future<void> listenConnection(BuildContext context) async {
    if (_subscription != null) return; // Zaten başlatılmışsa tekrar başlatma

    _subscription =
        InternetConnection().onStatusChange.listen((InternetStatus status) {
      switch (status) {
        case InternetStatus.connected:
          _dismissPopup();
          break;
        case InternetStatus.disconnected:
          _showPopup(context);
          break;
      }
    });
  }

  Future<void> stopListening() async {
    await _subscription?.cancel();
    _subscription = null;
  }

  void _showPopup(BuildContext context) {
    if (_isPopupVisible) return;

    _isPopupVisible = true;
    showDialog(
      context: context,
      barrierDismissible: false,
      useRootNavigator: true,
      builder: (ctx) {
        _popupContext = ctx;
        return AlertDialog(
          title: const Text("İnternet Bağlantısı Yok"),
          content: const Text(
            "İnternet bağlantınız kesildi. Lütfen tekrar bağlanmayı deneyin.",
          ),
          actions: [
            TextButton(
              onPressed: () async {
                final isConnected = await this.isConnected();
                if (isConnected) {
                  _dismissPopup();
                }
              },
              child: const Text("Tekrar Bağlan"),
            ),
          ],
        );
      },
    );
  }

  void _dismissPopup() {
    if (_isPopupVisible && _popupContext != null) {
      Navigator.of(_popupContext!).pop();
      _popupContext = null;
      _isPopupVisible = false;
    }
  }
}
