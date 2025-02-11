import 'dart:io';
import 'dart:math';
import 'dart:typed_data';
import 'package:csv/csv.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/foundation.dart' show kDebugMode, kIsWeb;
import "package:universal_html/html.dart" as html;

import '../../constants/core_messages.dart';
import 'i_download.dart';
import '/core/di/core_initializer.dart';

class CsvDownload implements ICsvDownload {
  @override
  Future<void> download(List<Map<String, dynamic>> data) async {
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

      if (kIsWeb) {
        saveCsvWeb(csvData, 'data${Random().nextInt(10000000)}.csv');
      } else if (Platform.isAndroid || Platform.isIOS) {
        String? outputFilePath = await _getSavePathForMobileApps();
        if (outputFilePath != null) {
          final file = File(outputFilePath);
          await file.writeAsString(csvData);
        }
      } else if (Platform.isMacOS || Platform.isLinux || Platform.isWindows) {
        String? outputFilePath = await _getSavePathForDesktop();
        if (outputFilePath != null) {
          final file = File(outputFilePath);
          await file.writeAsString(csvData);
        }
      }
    } catch (e) {
      _showErrorMessage(CoreMessages.customerDefaultErrorMessage);
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
    }
  }

  void saveCsvWeb(String csvData, String fileName) {
    final bytes = Uint8List.fromList(csvData.codeUnits);
    final blob = html.Blob([bytes]);
    final url = html.Url.createObjectUrlFromBlob(blob);
    html.AnchorElement(href: url)
      ..setAttribute("download", fileName)
      ..click();
    html.Url.revokeObjectUrl(url);
  }

  Future<String?> _getSavePathForMobileApps() async {
    try {
      String? selectedDirectory = await FilePicker.platform.getDirectoryPath(
        dialogTitle: CoreMessages.selectOutputFileMessage,
      );

      if (selectedDirectory != null) {
        String outputFilePath =
            '$selectedDirectory/data${Random().nextInt(10000000)}.csv';
        return outputFilePath;
      } else {
        return null;
      }
    } catch (e) {
      _showErrorMessage(CoreMessages.customerDefaultErrorMessage);
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
      return null;
    }
  }

  Future<String?> _getSavePathForDesktop() async {
    try {
      String? selectedDirectory = await FilePicker.platform.getDirectoryPath();
      if (selectedDirectory != null) {
        return "$selectedDirectory/data${Random().nextInt(10000000)}.csv";
      }
      return null;
    } catch (e) {
      _showErrorMessage(CoreMessages.customerDefaultErrorMessage);
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
      return null;
    }
  }

  void _showErrorMessage(String message) {
    CoreInitializer().coreContainer.screenMessage.getErrorMessage(message);
  }
}
