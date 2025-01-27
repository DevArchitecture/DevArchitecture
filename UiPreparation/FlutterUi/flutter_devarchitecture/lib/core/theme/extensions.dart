import 'package:flutter/material.dart';

import 'custom_colors.dart';

extension on String {
  List toArray() {
    List items = [];
    for (var i = 0; i < this.length; i++) {
      items.add(this[i]);
    }
    return items;
  }
}

extension ContextExtension on BuildContext {
  double dynamicWidth(double val) => MediaQuery.of(this).size.width * val;
  double dynamicHeight(double val) => MediaQuery.of(this).size.height * val;

  ThemeData get theme => Theme.of(this);
  bool get isDark => theme.brightness == Brightness.dark;
  bool get isLight => theme.brightness == Brightness.light;
}

extension ResponsiveExtension on BuildContext {
  bool get isMobile => MediaQuery.of(this).size.width < maxMobilePortraitWith;
  bool get isTablet =>
      MediaQuery.of(this).size.width < maxTabletPortraitWith &&
      MediaQuery.of(this).size.width >= maxMobilePortraitWith;
  bool get isDesktop => MediaQuery.of(this).size.width >= maxTabletPortraitWith;
}

extension NumberExtension on BuildContext {
  double get lowestValue => dynamicHeight(0.005);
  double get lowValue => dynamicHeight(0.01);
  double get mediumValue => dynamicHeight(0.02);
  double get highValue => dynamicHeight(0.05);

  double get lowestThickness => 0.35;
  double get lowThickness => 0.5;
  double get defaultThickness => 1;
  double get mediumThickness => 2;
  double get highThickness => 3;

  double get floatingActionButtonSmall => dynamicHeight(0.09);
  double get floatingActionButtonBig => dynamicHeight(0.12);

  double get percent5Screen => dynamicHeight(0.05);
  double get percent10Screen => dynamicHeight(0.10);
  double get percent15Screen => dynamicHeight(0.15);
  double get percent20Screen => dynamicHeight(0.20);
  double get percent25Screen => dynamicHeight(0.25);
  double get percent30Screen => dynamicHeight(0.30);
  double get percent40Screen => dynamicHeight(0.40);
  double get percent50Screen => dynamicHeight(0.50);
  double get percent60Screen => dynamicHeight(0.60);
  double get percent70Screen => dynamicHeight(0.70);
  double get percent75Screen => dynamicHeight(0.75);
  double get percent80Screen => dynamicHeight(0.80);

  double get maxMobilePortraitWith => 650;
  double get maxTabletPortraitWith => 1200;
}

extension PaddingExtension on BuildContext {
  EdgeInsets get lowestPadding => EdgeInsets.all(lowestValue);
  EdgeInsets get lowPadding => EdgeInsets.all(lowValue);
  EdgeInsets get defaultPadding => EdgeInsets.all(mediumValue);
  EdgeInsets get highPadding => EdgeInsets.all(highValue);
  EdgeInsets get highestPadding => EdgeInsets.all(highValue);

  EdgeInsets get lowHorizontalPadding =>
      EdgeInsets.symmetric(horizontal: lowValue);
  EdgeInsets get defaultHorizontalPadding =>
      EdgeInsets.symmetric(horizontal: mediumValue);
  EdgeInsets get highHorizontalPadding =>
      EdgeInsets.symmetric(horizontal: highValue);

  EdgeInsets get lowestVerticalPadding =>
      EdgeInsets.symmetric(vertical: lowestValue);
  EdgeInsets get lowVerticalPadding => EdgeInsets.symmetric(vertical: lowValue);
  EdgeInsets get defaultVerticalPadding =>
      EdgeInsets.symmetric(vertical: mediumValue);
  EdgeInsets get highVerticalPadding =>
      EdgeInsets.symmetric(vertical: highValue);

  EdgeInsets get lowLeftPadding => EdgeInsets.only(left: lowValue);
  EdgeInsets get lowestLeftPadding => EdgeInsets.only(left: lowestValue);
  EdgeInsets get defaultLeftPadding => EdgeInsets.only(left: mediumValue);
  EdgeInsets get highLeftPadding => EdgeInsets.only(left: highValue);

  EdgeInsets get lowestBottomPadding => EdgeInsets.only(bottom: lowestValue);
  EdgeInsets get lowBottomPadding => EdgeInsets.only(bottom: lowValue);
  EdgeInsets get defaultBottomPadding => EdgeInsets.only(bottom: mediumValue);
  EdgeInsets get highBottomPadding => EdgeInsets.only(bottom: highValue);

  EdgeInsets get lowestTopPadding => EdgeInsets.only(top: lowestValue);
  EdgeInsets get lowTopPadding => EdgeInsets.only(top: lowValue);
  EdgeInsets get defaultTopPadding => EdgeInsets.only(top: mediumValue);
  EdgeInsets get highTopPadding => EdgeInsets.only(top: highValue);

  EdgeInsets get lowestRightPadding => EdgeInsets.only(right: lowestValue);
  EdgeInsets get lowRightPadding => EdgeInsets.only(right: lowValue);
  EdgeInsets get defaultRightPadding => EdgeInsets.only(right: mediumValue);
  EdgeInsets get highRightPadding => EdgeInsets.only(right: highValue);
}

extension MarginExtension on BuildContext {
  EdgeInsets get lowestMargin => EdgeInsets.all(lowestValue);
  EdgeInsets get lowMargin => EdgeInsets.all(lowValue);
  EdgeInsets get defaultMargin => EdgeInsets.all(mediumValue);
  EdgeInsets get highMargin => EdgeInsets.all(highValue);

  EdgeInsets get lowHorizontalMargin =>
      EdgeInsets.symmetric(horizontal: lowValue);
  EdgeInsets get defaultHorizontalMargin =>
      EdgeInsets.symmetric(horizontal: mediumValue);
  EdgeInsets get highHorizontalMargin =>
      EdgeInsets.symmetric(horizontal: highValue);

  EdgeInsets get lowVerticalMargin => EdgeInsets.symmetric(vertical: lowValue);
  EdgeInsets get defaultVerticalMargin =>
      EdgeInsets.symmetric(vertical: mediumValue);
  EdgeInsets get highVerticalMargin =>
      EdgeInsets.symmetric(vertical: highValue);
}

extension BorderExtension on BuildContext {
  BorderRadius get defaultBorderRadius => BorderRadius.circular(mediumValue);
  BorderRadius get lowBorderRadius => BorderRadius.circular(lowValue);
  BorderRadius get lowestBorderRadius => BorderRadius.circular(lowestValue);
  ShapeBorder get shapeBorder => RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(lowValue),
        side: BorderSide(
          color: CustomColors.dark.getColor.withOpacity(.1),
          width: 1,
        ),
      );

  BorderRadius get leftSideBorderRadius => BorderRadius.only(
      topLeft: Radius.circular(lowValue),
      bottomLeft: Radius.circular(lowValue));

  BorderRadius get rightSideBorderRadius => BorderRadius.only(
      topRight: Radius.circular(lowValue),
      bottomRight: Radius.circular(lowValue));
}

extension EmptyWidget on BuildContext {
  Widget get emptyWidgetHighHeight => SizedBox(
        height: highValue,
      );
  Widget get emptyWidgetHighWidth => SizedBox(
        width: highValue,
      );
  Widget get emptyWidgetDefaultHeight => SizedBox(
        height: mediumValue,
      );
  Widget get emptyWidgetDefaultWidth => SizedBox(
        width: mediumValue,
      );
  Widget get emptyWidgetLowHeight => SizedBox(
        height: lowValue,
      );
  Widget get emptyWidgetLowWidth => SizedBox(
        width: lowValue,
      );

  Widget get emptyWidget => const SizedBox(
        width: 0,
        height: 0,
      );
}
