import 'package:flutter/material.dart';

import '../../../../../core/constants/base_constants.dart';

class LogMessages implements ScreenConstantsBase {
  static late String logInfoHover;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    logInfoHover = BaseConstants.translate("LogInfoHover");
  }
}
