import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class GroupClaimScreenTexts extends MessageConstantsBase {
  static late String groupClaims;
  static late String selectGroupClaim;
  static void init(BuildContext context) {
    BaseConstants.init(context);
    groupClaims = BaseConstants.translate("GroupClaims");
    selectGroupClaim = BaseConstants.translate("SelectGroupClaim");
  }
}
