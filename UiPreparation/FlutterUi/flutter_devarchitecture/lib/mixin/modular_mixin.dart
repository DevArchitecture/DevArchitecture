import 'dart:ui';
import 'package:flutter/material.dart';
import '../core/extensions/claim_provider.dart';
import '/core/theme/theme_provider.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:provider/provider.dart';
import '../core/constants/core_screen_texts.dart';
import '../core/extensions/translation_provider.dart';
import '../routes/app_route_module.dart';

mixin ModularMixin {
  Widget buildModular(BuildContext context) {
    return Consumer3<ThemeProvider, TranslationProvider, ClaimProvider>(
      builder:
          (context, themeProvider, translationProvider, claimProvider, child) {
        return ModularApp(
          module: AppRouteModule(),
          child: MaterialApp.router(
            title: CoreScreenTexts.appName,
            locale: translationProvider.locale,
            supportedLocales: const [
              Locale('en', 'US'),
              Locale('tr', 'TR'),
              Locale('de', 'DE'),
              Locale('es', 'ES'),
              Locale('fr', 'FR'),
              Locale('it', 'IT'),
              Locale('pt', 'PT'),
              Locale('ru', 'RU'),
              Locale('ja', 'JP'),
              Locale('zh', 'CN'),
            ],
            localizationsDelegates: const [
              GlobalMaterialLocalizations.delegate,
              GlobalWidgetsLocalizations.delegate,
              GlobalCupertinoLocalizations.delegate,
            ],
            scrollBehavior: ScrollConfiguration.of(context).copyWith(
              dragDevices: {
                PointerDeviceKind.touch,
                PointerDeviceKind.mouse,
                PointerDeviceKind.stylus,
                PointerDeviceKind.trackpad,
                PointerDeviceKind.invertedStylus,
                PointerDeviceKind.unknown,
              },
            ),
            theme: lightTheme,
            darkTheme: darkTheme,
            themeMode: themeProvider.themeMode,
            routerConfig: Modular.routerConfig,
          ),
        );
      },
    );
  }
}
