import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'translation_provider.dart';

extension TranslationProviderExtension on BuildContext {
  TranslationProvider get translationProvider =>
      Provider.of<TranslationProvider>(this, listen: true);
}
