import 'package:flutter/material.dart';

abstract class IInternetConnection {
  Future<bool> isConnected();
  Future<void> listenConnection(BuildContext context);
  Future<void> stopListening();
}
