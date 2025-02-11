import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class TranslateMessages implements ScreenConstantsBase {
  static late String translateInfoHover;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    translateInfoHover = BaseConstants.translate("TranslateInfoHover");
  }
}
