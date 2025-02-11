import 'dart:convert';
import 'dart:math';
import 'package:flutter/foundation.dart';
import '/core/constants/core_messages.dart';
import '/core/constants/core_screen_texts.dart';
import 'package:path_provider/path_provider.dart';
import 'package:share_plus/share_plus.dart';
import 'dart:io';
import '../../di/core_initializer.dart';
import 'i_share.dart';

class JsonShare implements IJsonShare {
  @override
  Future<void> share(List<Map<String, dynamic>> data) async {
    try {
      final jsonString = jsonEncode(data);
      final directory = await getTemporaryDirectory();
      final path = '${directory.path}/data${Random().nextInt(10000000)}.json';
      final file = File(path);
      await file.writeAsString(jsonString);
      await Share.shareXFiles(
        [XFile(path)],
        text: CoreScreenTexts.shareTitle,
      );
    } catch (e) {
      _showErrorMessage(CoreMessages.customerDefaultErrorMessage);
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
    }
  }

  void _showErrorMessage(String message) {
    CoreInitializer().coreContainer.screenMessage.getErrorMessage(message);
  }
}
