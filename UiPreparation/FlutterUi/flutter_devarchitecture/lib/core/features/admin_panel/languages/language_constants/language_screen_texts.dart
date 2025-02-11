import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class LanguageScreenTexts implements ScreenConstantsBase {
  static late String languageList;
  static late String code;
  static late String addLanguage;
  static late String updateLanguage;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    languageList = BaseConstants.translate("LanguageList");
    code = BaseConstants.translate("Code");
    addLanguage = BaseConstants.translate("AddLanguage");
    updateLanguage = BaseConstants.translate("UpdateLanguage");
  }
}
