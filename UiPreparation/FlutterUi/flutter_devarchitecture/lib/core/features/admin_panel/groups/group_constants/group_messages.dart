import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class GroupMessages implements ScreenConstantsBase {
  static late String groupInfoHover;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    groupInfoHover = BaseConstants.translate("GroupInfoHover");
  }
}
