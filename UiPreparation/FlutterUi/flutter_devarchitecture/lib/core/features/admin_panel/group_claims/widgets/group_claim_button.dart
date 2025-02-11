import 'package:flutter/material.dart';

import '../../../../theme/custom_colors.dart';
import '../../../../extensions/claimed_widget.dart';
import '../group_constants/group_claim_messages.dart';

Widget updateGroupClaimButton(BuildContext context, VoidCallback onPressed) =>
    ClaimedWidget(
        claimText: "UpdateGroupClaimCommand",
        child: Tooltip(
          message: GroupClaimMessages.groupPermissionUpdate,
          child: IconButton(
              hoverColor: CustomColors.transparent.getColor,
              onPressed: onPressed,
              icon: Icon(
                Icons.security_rounded,
                color: CustomColors.primary.getColor,
              )),
        ));
