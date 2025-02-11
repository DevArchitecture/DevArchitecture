import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class GroupScreenTexts implements ScreenConstantsBase {
  static late String groupList;
  static late String groupName;
  static late String addGroup;
  static late String groupNameHint;
  static late String updateGroupClaims;
  static late String updateGroup;
  static late String updateGroupUsers;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    groupList = BaseConstants.translate("GroupList");
    groupName = BaseConstants.translate("GroupName");
    groupNameHint = BaseConstants.translate("GroupNameHint");
    addGroup = BaseConstants.translate("AddGroup");
    updateGroupClaims = BaseConstants.translate("UpdateGroupClaims");
    updateGroup = BaseConstants.translate("UpdateGroup");
    updateGroupUsers = BaseConstants.translate("UpdateGroupUsers");
  }
}
