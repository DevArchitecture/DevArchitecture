import 'package:flutter/material.dart';
import 'package:oktoast/oktoast.dart';

import '../../theme/custom_colors.dart';
import 'i_screen_message.dart';

class OkToastScreenMessage implements IScreenMessage {
  @override
  void getInfoMessage(String message) {
    showToast(message,
        duration: const Duration(seconds: 3), position: ToastPosition.top);
  }

  @override
  void getSuccessMessage(String message,
      {Alignment position = Alignment.topCenter}) {
    showToast(
        position: ToastPosition(align: position),
        backgroundColor: CustomColors.success.getColor,
        message,
        duration: const Duration(seconds: 2));
  }

  @override
  void getErrorMessage(String message) {
    showToast(
        backgroundColor: CustomColors.danger.getColor,
        message,
        duration: const Duration(seconds: 6));
  }

  @override
  void getWarningMessage(String message) {
    showToast(
        backgroundColor: CustomColors.warning.getColor,
        message,
        textStyle: TextStyle(color: CustomColors.dark.getColor),
        duration: const Duration(seconds: 4));
  }
}
