import 'dart:io';
import 'dart:math';
import 'package:csv/csv.dart';
import 'package:flutter/foundation.dart';
import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';
import '/core/utilities/file_share/i_share.dart';
import 'package:path_provider/path_provider.dart';
import 'package:share_plus/share_plus.dart';
import '/core/di/core_initializer.dart';

class CsvShare implements ICsvShare {
  @override
  Future<void> share(List<Map<String, dynamic>> data) async {
    try {
      List<List<dynamic>> rows = [];

      if (data.isNotEmpty) {
        List<String> headers = data.first.keys.toList();
        rows.add(headers);
      }

      for (var row in data) {
        List<dynamic> rowData = row.values.toList();
        rows.add(rowData);
      }

      String csvData = const ListToCsvConverter().convert(rows);

      final directory = await getTemporaryDirectory();
      final path = '${directory.path}/data${Random().nextInt(10000000)}.csv';
      final file = File(path);
      await file.writeAsString(csvData);

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
