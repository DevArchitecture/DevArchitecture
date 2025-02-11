import 'package:flutter/material.dart';
import '/core/theme/extensions.dart';

import '../../layouts/base_scaffold.dart';
import '../di/core_initializer.dart';
import 'base_state.dart';

void showScreenMessageByBlocStatus(BaseState state) {
  if (state is BlocFailed) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getErrorMessage(state.message);
  }
  if (state is BlocChecking) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getInfoMessage(state.message ?? "");
  }
  if (state is BlocSending) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getInfoMessage(state.message ?? "");
  }
  if (state is BlocLoading) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getInfoMessage(state.message ?? "");
  }
  if (state is BlocAdded) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getSuccessMessage(state.message!);
  }
  if (state is BlocUpdated) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getSuccessMessage(state.message!);
  }
  if (state is BlocDeleted) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getWarningMessage(state.message!);
  }
}

Widget? getResultWidgetByStateWithScaffold(
    BuildContext context, BaseState state) {
  var wid = getResultWidgetByState(context, state);
  if (wid != null) {
    return buildBaseScaffold(context, wid);
  }
  return null;
}

Widget? getResultWidgetByState(BuildContext context, BaseState state) {
  if (state is BlocLoading) {
    return Center(
        child: CoreInitializer()
            .coreContainer
            .statusAnimationAsset
            .getLoadingAnimationAsset(
                context.percent10Screen, context.percent10Screen));
  }
  if (state is BlocChecking) {
    return Center(
      child: CoreInitializer()
          .coreContainer
          .statusAnimationAsset
          .getCheckingAnimationAsset(
              context.percent10Screen, context.percent10Screen),
    );
  }
  if (state is BlocSending) {
    return Center(
      child: CoreInitializer()
          .coreContainer
          .statusAnimationAsset
          .getSendingAnimationAsset(
              context.percent10Screen, context.percent10Screen),
    );
  }
  return null;
}
