import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class TranslateScreenTexts implements ScreenConstantsBase {
  static late String language;
  static late String translateList;
  static late String codeHint;
  static late String valueHint;
  static late String code;
  static late String value;
  static late String addTranslate;
  static late String updateTranslate;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    language = BaseConstants.translate("Language");
    translateList = BaseConstants.translate("TranslateList");
    codeHint = BaseConstants.translate("CodeHint");
    valueHint = BaseConstants.translate("ValueHint");
    code = BaseConstants.translate("Code");
    value = BaseConstants.translate("Value");
    addTranslate = BaseConstants.translate("AddTranslate");
    updateTranslate = BaseConstants.translate("UpdateTranslate");
  }
}
