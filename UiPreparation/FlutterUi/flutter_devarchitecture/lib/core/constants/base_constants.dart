import 'package:flutter/material.dart';
import '../extensions/translate_provider_extension.dart';

abstract class BaseConstants {
  static late BuildContext _context;

  static void init(BuildContext context) {
    _context = context;
  }

  static String translate(String key) =>
      _context.translationProvider.translate(key);
}

abstract class MessageConstantsBase extends BaseConstants {}

abstract class ScreenConstantsBase extends BaseConstants {}

abstract class SidebarConstantsBase extends BaseConstants {}
