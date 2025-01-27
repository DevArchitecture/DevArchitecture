import 'package:flutter/material.dart';
import '../../routes/routes_constants.dart';
import '../constants/core_messages.dart';
import '../di/core_initializer.dart';
import 'base_state.dart';

class BlocFailedMiddleware {
  static void handleBlocFailed(BuildContext context, BaseState state) {
    if (state is BlocFailed) {
      switch (state.statusCode) {
        case 400:
          CoreInitializer()
              .coreContainer
              .screenMessage
              .getErrorMessage(state.message);
          break;
        case 401:
          CoreInitializer()
              .coreContainer
              .screenMessage
              .getErrorMessage(state.message);
          Navigator.pushReplacementNamed(context, RoutesConstants.loginPage);
          break;
        case 403:
          CoreInitializer()
              .coreContainer
              .screenMessage
              .getErrorMessage(state.message);
          Navigator.pushReplacementNamed(context, RoutesConstants.appHomePage);
          break;
        case 404:
          CoreInitializer()
              .coreContainer
              .screenMessage
              .getErrorMessage(state.message);
          Navigator.pushReplacementNamed(context, RoutesConstants.appHomePage);
          break;
        case 500:
          CoreInitializer()
              .coreContainer
              .screenMessage
              .getErrorMessage(CoreMessages.customerDefaultErrorMessage);
          Navigator.pushReplacementNamed(context, RoutesConstants.loginPage);

          break;
        default:
          CoreInitializer()
              .coreContainer
              .screenMessage
              .getErrorMessage(CoreMessages.customerDefaultErrorMessage);
          Navigator.pushReplacementNamed(context, RoutesConstants.loginPage);

          break;
      }
    }
  }
}
