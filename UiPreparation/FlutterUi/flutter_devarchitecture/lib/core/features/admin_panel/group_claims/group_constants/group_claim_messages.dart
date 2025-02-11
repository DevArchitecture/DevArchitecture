import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class GroupClaimMessages extends MessageConstantsBase {
  static late String groupPermissionUpdate;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    groupPermissionUpdate = BaseConstants.translate("GroupPermissionUpdate");
  }
}
