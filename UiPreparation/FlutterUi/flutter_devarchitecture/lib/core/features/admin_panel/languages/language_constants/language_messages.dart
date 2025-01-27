import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class LanguageMessages implements ScreenConstantsBase {
  static late String languageInfoHover;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    languageInfoHover = BaseConstants.translate("LanguageInfoHover");
  }
}
