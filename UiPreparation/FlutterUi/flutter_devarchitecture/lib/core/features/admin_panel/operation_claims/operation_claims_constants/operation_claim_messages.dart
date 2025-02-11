import 'package:flutter/material.dart';

import '../../../../constants/base_constants.dart';

class OperationClaimMessages implements ScreenConstantsBase {
  static late String operationClaimInfoHover;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    operationClaimInfoHover =
        BaseConstants.translate("OperationClaimInfoHover");
  }
}
