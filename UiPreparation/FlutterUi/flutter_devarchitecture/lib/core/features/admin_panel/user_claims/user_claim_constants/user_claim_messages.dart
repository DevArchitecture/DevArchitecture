import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class UserClaimMessages implements ScreenConstantsBase {
  static late String passwordChange;
  static late String permissionChange;
  static late String groupChange;
  static void init(BuildContext context) {
    BaseConstants.init(context);

    passwordChange = BaseConstants.translate("PasswordChange");
    permissionChange = BaseConstants.translate("PermissionChange");
    groupChange = BaseConstants.translate("GroupChange");
  }
}
