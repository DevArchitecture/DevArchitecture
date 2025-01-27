import 'package:flutter/material.dart';
import '../../../../core/theme/custom_colors.dart';
import '../../../../core/widgets/base_widgets.dart';
import '../../../../core/theme/extensions.dart';

Padding buildUsedSpaceCardWidget(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
        context,
        Icons.storage,
        color: CustomColors.warning.getColor,
        "49/50 GB",
        "Used Space",
        footer: "Get More Space"),
  );
}

Padding buildFollowersCardWidget(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
        context,
        Icons.person,
        color: CustomColors.secondary.getColor,
        "+245",
        "Followers",
        footer: "Just Updated"),
  );
}

Padding BuildFixedIssuesCardWidget(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
        context,
        Icons.warning_amber_rounded,
        color: CustomColors.danger.getColor,
        "75",
        "FixedIssues",
        footer: "Tracked From Github"),
  );
}

Padding buildRevenueCardWidget(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
        context,
        Icons.store_mall_directory,
        color: CustomColors.success.getColor,
        "\$34,245",
        "Revenue",
        footer: "Last 24 Hours"),
  );
}
