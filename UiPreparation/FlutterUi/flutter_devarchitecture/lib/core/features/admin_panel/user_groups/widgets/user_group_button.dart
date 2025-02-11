import 'package:flutter/material.dart';

import '../../../../theme/custom_colors.dart';
import '../../../../extensions/claimed_widget.dart';
import '../user_group_constants/user_group_messages.dart';

Widget updateUserGroupButton(BuildContext context, VoidCallback onPressed) =>
    ClaimedWidget(
        claimText: "UpdateUserGroupByGroupIdCommand",
        child: Tooltip(
          message: UserGroupMessages.groupUpdate,
          child: IconButton(
              hoverColor: CustomColors.transparent.getColor,
              onPressed: onPressed,
              icon: Icon(
                Icons.group_rounded,
                color: CustomColors.secondary.getColor,
              )),
        ));
