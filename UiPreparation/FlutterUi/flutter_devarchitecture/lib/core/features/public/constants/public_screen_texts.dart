import 'package:flutter/material.dart';
import '/core/constants/base_constants.dart';

class PublicScreenTexts extends ScreenConstantsBase {
  static late String loginTitle;
  static late String loginButton;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    loginTitle = BaseConstants.translate("LoginTitle");
    loginButton = BaseConstants.translate("LoginButton");
  }
}
