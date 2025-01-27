import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class UserMessages implements ScreenConstantsBase {
  static late String userInfoHover;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    userInfoHover = BaseConstants.translate("UserInfoHover");
  }
}
