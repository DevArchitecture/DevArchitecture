import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class UserGroupScreenTexts implements ScreenConstantsBase {
  static late String users;
  static late String selectUsers;
  static late String userGroups;
  static late String selectUserGroups;
  static void init(BuildContext context) {
    BaseConstants.init(context);

    selectUsers = BaseConstants.translate("SelectUsers");
    users = BaseConstants.translate("Users");
    userGroups = BaseConstants.translate("UserGroups");
    selectUserGroups = BaseConstants.translate("SelectUserGroups");
  }
}
