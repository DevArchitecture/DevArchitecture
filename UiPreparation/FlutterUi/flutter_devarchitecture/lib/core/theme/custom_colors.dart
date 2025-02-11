import 'package:flutter/material.dart';
import 'theme_provider.dart';

enum CustomColors {
  baseDefault,
  transparent,
  primary,
  secondary,
  success,
  info,
  warning,
  danger,
  light,
  gray,
  dark,
  white,
}

extension SelectedColorExtension on CustomColors {
  Color get getColor {
    final isDarkMode = ThemeProvider.isDarkMode();

    switch (this) {
      case CustomColors.transparent:
        return Colors.transparent;
      case CustomColors.primary:
        return isDarkMode
            ? Color.fromARGB(255, 133, 117, 248)
            : Color.fromARGB(255, 91, 70, 249);
      case CustomColors.secondary:
        return isDarkMode
            ? Color.fromARGB(255, 231, 123, 255)
            : Color.fromARGB(255, 206, 58, 239);
      case CustomColors.success:
        return isDarkMode
            ? Color.fromARGB(255, 110, 233, 159)
            : Color.fromARGB(255, 42, 190, 126);
      case CustomColors.info:
        return isDarkMode
            ? Color.fromARGB(255, 135, 144, 250)
            : Color.fromARGB(255, 93, 107, 252);

      case CustomColors.warning:
        return isDarkMode
            ? Color.fromARGB(255, 253, 193, 107)
            : Color.fromARGB(255, 255, 174, 61);

      case CustomColors.danger:
        return isDarkMode
            ? Color.fromARGB(255, 255, 121, 103)
            : Color.fromARGB(255, 240, 80, 59);
      case CustomColors.light:
        return isDarkMode
            ? Color.fromARGB(255, 238, 238, 238)
            : Color.fromARGB(255, 238, 238, 238);
      case CustomColors.dark:
        return isDarkMode
            ? Color.fromARGB(255, 255, 255, 255)
            : Color.fromARGB(255, 9, 9, 9);
      case CustomColors.gray:
        return isDarkMode
            ? Color.fromARGB(255, 204, 204, 204)
            : Color.fromARGB(255, 147, 150, 157);
      case CustomColors.white:
        return isDarkMode
            ? Color.fromARGB(255, 255, 255, 255)
            : Color.fromARGB(255, 255, 255, 255);
      case CustomColors.baseDefault:
        return Color.fromARGB(255, 255, 89, 34);
    }
  }
}
