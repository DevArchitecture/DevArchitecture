import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class OperationClaimScreenTexts implements ScreenConstantsBase {
  static late String operationClaimList;
  static late String updateOperationClaim;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    updateOperationClaim = BaseConstants.translate("UpdateOperationClaim");
    operationClaimList = BaseConstants.translate("OperationClaimList");
  }
}
