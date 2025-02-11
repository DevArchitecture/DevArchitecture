import 'package:flutter/material.dart';

import '../../../../theme/custom_colors.dart';
import '../../../../extensions/claimed_widget.dart';
import '../user_claim_constants/user_claim_messages.dart';

Widget getChangePasswordButton(BuildContext context, VoidCallback onPressed) =>
    ClaimedWidget(
        claimText: "UserChangePasswordCommand",
        child: Tooltip(
          message: UserClaimMessages.passwordChange,
          child: IconButton(
              hoverColor: CustomColors.transparent.getColor,
              onPressed: onPressed,
              icon: Icon(
                Icons.password,
                color: CustomColors.primary.getColor,
              )),
        ));

Widget getChangePermissionButton(
        BuildContext context, VoidCallback onPressed) =>
    ClaimedWidget(
        claimText: "UpdateUserClaimCommand",
        child: Tooltip(
          message: UserClaimMessages.permissionChange,
          child: IconButton(
              hoverColor: CustomColors.transparent.getColor,
              onPressed: onPressed,
              icon: Icon(
                Icons.safety_check_rounded,
                color: CustomColors.secondary.getColor,
              )),
        ));

Widget getChangeGroupButton(BuildContext context, VoidCallback onPressed) =>
    ClaimedWidget(
        claimText: "UpdateGroupClaimCommand",
        child: Tooltip(
            message: UserClaimMessages.groupChange,
            child: IconButton(
                hoverColor: CustomColors.transparent.getColor,
                onPressed: onPressed,
                icon: Icon(
                  Icons.group,
                  color: CustomColors.success.getColor,
                ))));
