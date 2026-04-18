import 'package:flutter/material.dart';

import 'i_internet_connection.dart';

class NoopInternetConnection implements IInternetConnection {
  @override
  Future<bool> isConnected() async => true;

  @override
  Future<void> listenConnection(BuildContext context) async {}

  @override
  Future<void> stopListening() async {}
}
