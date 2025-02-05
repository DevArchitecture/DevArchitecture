import 'package:flutter/material.dart';
import '/di/constants_initializer.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:oktoast/oktoast.dart';
import 'package:provider/provider.dart';

import 'core/di/core_initializer.dart';
import 'core/di/firebase/firebase_initializer.dart';
import 'core/extensions/claim_provider.dart';
import 'core/extensions/translation_provider.dart';
import 'core/theme/theme_provider.dart';
import 'mixin/modular_mixin.dart';
import 'core/mixins/ok_toast_mixin.dart';
import 'di/business_initializer.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  initializeDateFormatting();
  await injectFirebaseUtils();
  CoreInitializer();
  BusinessInitializer();

  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => ThemeProvider()),
        ChangeNotifierProvider(create: (_) => TranslationProvider()),
        ChangeNotifierProvider(create: (_) => ClaimProvider()),
      ],
      child: OKToast(
        child: MyApp(),
      ),
    ),
  );
}

Future<void> injectFirebaseUtils() async {
  const isFirebaseEnabled = bool.fromEnvironment('FIREBASE');
  if (isFirebaseEnabled) {
    FirebaseInitializer();
  }
}

class MyApp extends StatefulWidget {
  MyApp({super.key});

  @override
  State<MyApp> createState() => _AppState();
}

class _AppState extends State<MyApp> with OKToastMixin<MyApp>, ModularMixin {
  late Future<void> _initializeTranslations;

  @override
  void initState() {
    super.initState();
    _initializeTranslations =
        Provider.of<TranslationProvider>(context, listen: false)
            .loadTranslations("tr-TR");
  }

  @override
  void dispose() {
    CoreInitializer().coreContainer.internetConnection.stopListening();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    ConstantsInitializer(context);
    return MaterialApp(
      home: Scaffold(
        body: FutureBuilder<void>(
          future: _initializeTranslations,
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return Center(child: CircularProgressIndicator());
            }

            if (snapshot.hasError) {
              print(snapshot.error);
              print(snapshot.stackTrace);
              return Center(
                child: Text(
                  "Bir hata oluştu: ${snapshot.error}",
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                    color: Colors.red,
                  ),
                ),
              );
            }

            WidgetsBinding.instance.addPostFrameCallback((_) {
              CoreInitializer()
                  .coreContainer
                  .internetConnection
                  .listenConnection(context);
            });

            return buildChild(context);
          },
        ),
      ),
    );
  }

  @override
  Widget buildChild(BuildContext context) {
    return buildModular(context);
  }
}
