import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class UserClaimScreenTexts implements ScreenConstantsBase {
  static late String userClaims;
  static late String selectUserClaims;
  static void init(BuildContext context) {
    BaseConstants.init(context);
    userClaims = BaseConstants.translate("UserClaims");
    selectUserClaims = BaseConstants.translate("SelectUserClaims");
  }
}
