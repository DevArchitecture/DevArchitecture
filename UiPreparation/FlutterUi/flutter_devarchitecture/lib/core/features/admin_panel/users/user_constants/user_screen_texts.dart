import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class UserScreenTexts implements ScreenConstantsBase {
  static late String userList;
  static late String fullName;
  static late String addUser;
  static late String fullNameHint;

  static late String active;
  static late String inactive;
  static late String changePassword;
  static late String newPassword;
  static late String confirmPassword;
  static late String updateUserClaims;
  static late String updateUserGroups;
  static late String updateUsers;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    userList = BaseConstants.translate("UserList");
    fullName = BaseConstants.translate("FullName");

    addUser = BaseConstants.translate("AddUser");
    fullNameHint = BaseConstants.translate("FullNameHint");
    active = BaseConstants.translate("Active");
    inactive = BaseConstants.translate("Inactive");

    changePassword = BaseConstants.translate("ChangePassword");
    newPassword = BaseConstants.translate("NewPassword");
    confirmPassword = BaseConstants.translate("ConfirmPassword");
    updateUserClaims = BaseConstants.translate("UpdateUserClaims");
    updateUserGroups = BaseConstants.translate("UpdateUserGroups");
    updateUsers = BaseConstants.translate("UpdateUsers");
  }
}
