import 'dart:async';

import 'package:flutter_modular/flutter_modular.dart';

import '../../routes/routes_constants.dart';
import '../configs/app_config.dart';
import '../di/core_initializer.dart';

class ModularAuthGuard extends RouteGuard {
  ModularAuthGuard() : super(redirectTo: RoutesConstants.loginPage);

  @override
  Future<bool> canActivate(String path, ModularRoute router) {
    return (Modular.get<AuthStore>().isLogged);
  }
}

class AuthStore {
  Future<bool> get isLogged async {
    if (appConfig.name == "dev") {
      return true;
    }
    bool isLogged =
        await CoreInitializer().coreContainer.storage.containsKey("token");
    if (await CoreInitializer()
        .coreContainer
        .storage
        .containsKey("token_expiration")) {
      isLogged = DateTime.parse(await CoreInitializer()
                  .coreContainer
                  .storage
                  .read("token_expiration") ??
              DateTime(2024).toIso8601String())
          .isAfter(DateTime.now());
    }
    return isLogged;
  }
}
