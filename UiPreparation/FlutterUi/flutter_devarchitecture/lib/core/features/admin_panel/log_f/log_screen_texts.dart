import 'package:flutter/material.dart';

import '../../../../../core/constants/base_constants.dart';

class LogScreenTexts implements ScreenConstantsBase {
  static late String logList;
  static late String level;
  static late String exceptionMessage;
  static late String timeStamp;
  static late String user;
  static late String value;
  static late String type;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    logList = BaseConstants.translate("LogList");
    level = BaseConstants.translate("Level");
    exceptionMessage = BaseConstants.translate("ExceptionMessage");
    timeStamp = BaseConstants.translate("TimeStamp");
    user = BaseConstants.translate("User");
    value = BaseConstants.translate("Value");
    type = BaseConstants.translate("Type");
  }
}
