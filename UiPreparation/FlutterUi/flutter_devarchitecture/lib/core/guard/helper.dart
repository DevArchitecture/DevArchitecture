import 'package:flutter_modular/flutter_modular.dart';

import 'auth_guard.dart';

Future<bool> isLoggedIn() {
  return Modular.get<AuthStore>().isLogged;
}
