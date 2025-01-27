import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class UserGroupMessages implements ScreenConstantsBase {
  static late String groupUpdate;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    groupUpdate = BaseConstants.translate("GroupUpdate");
  }
}
