import 'package:flutter/material.dart';
import '/core/constants/base_constants.dart';

class PublicMessages extends MessageConstantsBase {
  static late String formValidationErrorMessage;
  static late String pageNotFound;
  static late String returnHomePage;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    formValidationErrorMessage =
        BaseConstants.translate("FormValidationErrorMessage");
    pageNotFound = BaseConstants.translate("PageNotFound");
    returnHomePage = BaseConstants.translate("ReturnHomePage");
  }
}
