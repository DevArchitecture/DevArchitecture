import 'package:flutter/material.dart';
import 'custom_colors.dart';

class ThemeProvider with ChangeNotifier {
  static ThemeMode _themeMode = ThemeMode.light;

  ThemeMode get themeMode => _themeMode;

  void toggleTheme() {
    _themeMode =
        _themeMode == ThemeMode.dark ? ThemeMode.light : ThemeMode.dark;
    notifyListeners();
  }

  static bool isDarkMode() => _themeMode == ThemeMode.dark;
}

final darkTheme = ThemeData(
  fontFamily: "nunito",
  drawerTheme: DrawerThemeData(backgroundColor: CustomColors.dark.getColor),
  brightness: Brightness.dark,
  primaryColor: CustomColors.primary.getColor,
  scaffoldBackgroundColor: CustomColors.dark.getColor,
  appBarTheme: AppBarTheme(
    backgroundColor: CustomColors.dark.getColor,
    foregroundColor: CustomColors.white.getColor,
  ),
  colorScheme: ColorScheme.dark(
    primary: CustomColors.primary.getColor,
    secondary: CustomColors.secondary.getColor,
    background: CustomColors.dark.getColor,
    surface: Colors.grey[850]!,
    onPrimary: CustomColors.white.getColor,
    onSecondary: CustomColors.dark.getColor,
    onBackground: CustomColors.white.getColor,
    onSurface: CustomColors.white.getColor,
  ),
  buttonTheme: ButtonThemeData(
    buttonColor: CustomColors.primary.getColor,
    textTheme: ButtonTextTheme.primary,
  ),
  textButtonTheme: TextButtonThemeData(
    style: TextButton.styleFrom(
      foregroundColor: CustomColors.primary.getColor,
    ),
  ),
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      foregroundColor: CustomColors.primary.getColor,
      backgroundColor: CustomColors.white.getColor,
    ),
  ),
  iconTheme: IconThemeData(color: CustomColors.white.getColor),
  inputDecorationTheme: InputDecorationTheme(
    border: OutlineInputBorder(),
    enabledBorder: OutlineInputBorder(
      borderSide: BorderSide(color: CustomColors.gray.getColor),
    ),
    focusedBorder: OutlineInputBorder(
      borderSide: BorderSide(color: CustomColors.primary.getColor),
    ),
    labelStyle: TextStyle(color: CustomColors.gray.getColor),
    prefixIconColor: CustomColors.gray.getColor,
  ),
  textTheme: TextTheme(
    bodySmall: TextStyle(color: CustomColors.white.getColor),
    bodyLarge: TextStyle(color: CustomColors.white.getColor),
    bodyMedium: TextStyle(color: CustomColors.white.getColor),
    headlineLarge: TextStyle(
        color: CustomColors.white.getColor,
        fontSize: 32,
        fontWeight: FontWeight.bold),
    headlineMedium: TextStyle(color: CustomColors.white.getColor, fontSize: 24),
    headlineSmall: TextStyle(color: CustomColors.white.getColor, fontSize: 18),
  ),
);

final lightTheme = ThemeData(
  fontFamily: "nunito",
  drawerTheme: DrawerThemeData(
    backgroundColor: CustomColors.white.getColor,
  ),
  brightness: Brightness.light,
  primaryColor: CustomColors.primary.getColor,
  scaffoldBackgroundColor: CustomColors.white.getColor,
  appBarTheme: AppBarTheme(
    backgroundColor: CustomColors.white.getColor,
    foregroundColor: CustomColors.dark.getColor,
  ),
  colorScheme: ColorScheme.light(
    primary: CustomColors.primary.getColor,
    secondary: CustomColors.secondary.getColor,
    background: CustomColors.white.getColor,
    surface: Colors.grey[200]!,
    onPrimary: CustomColors.white.getColor,
    onSecondary: CustomColors.dark.getColor,
    onBackground: CustomColors.dark.getColor,
    onSurface: CustomColors.dark.getColor,
  ),
  buttonTheme: ButtonThemeData(
    buttonColor: CustomColors.primary.getColor,
    textTheme: ButtonTextTheme.primary,
  ),
  textButtonTheme: TextButtonThemeData(
    style: TextButton.styleFrom(
      foregroundColor: CustomColors.white.getColor,
      backgroundColor: CustomColors.primary.getColor,
    ),
  ),
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      foregroundColor: CustomColors.primary.getColor,
      backgroundColor: CustomColors.white.getColor,
    ),
  ),
  iconTheme: IconThemeData(color: CustomColors.primary.getColor),
  inputDecorationTheme: InputDecorationTheme(
    border: OutlineInputBorder(),
    enabledBorder: OutlineInputBorder(
      borderSide: BorderSide(color: CustomColors.gray.getColor),
    ),
    focusedBorder: OutlineInputBorder(
      borderSide: BorderSide(color: CustomColors.primary.getColor),
    ),
    labelStyle: TextStyle(color: CustomColors.gray.getColor),
    prefixIconColor: CustomColors.gray.getColor,
  ),
  textTheme: TextTheme(
    bodyLarge: TextStyle(color: CustomColors.dark.getColor),
    bodyMedium: TextStyle(color: CustomColors.dark.getColor),
    bodySmall: TextStyle(color: CustomColors.dark.getColor),
    headlineLarge: TextStyle(
        color: CustomColors.dark.getColor,
        fontSize: 32,
        fontWeight: FontWeight.bold),
    headlineMedium: TextStyle(color: CustomColors.dark.getColor, fontSize: 24),
    headlineSmall: TextStyle(color: CustomColors.dark.getColor, fontSize: 18),
  ),
);
