import 'package:flutter/material.dart';
import 'extensions.dart';

class ResponsiveWidget extends StatelessWidget {
  final Widget mobile;
  final Widget? desktop;
  final Widget tablet;

  const ResponsiveWidget({
    super.key,
    required this.mobile,
    required this.tablet,
    this.desktop,
  });

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(builder: (context, constraints) {
      if (constraints.maxWidth >= context.maxTabletPortraitWith) {
        return desktop ?? tablet;
      } else if (constraints.maxWidth >= context.maxMobilePortraitWith &&
          constraints.maxWidth < context.maxTabletPortraitWith) {
        return tablet;
      } else {
        return mobile;
      }
    });
  }
}
