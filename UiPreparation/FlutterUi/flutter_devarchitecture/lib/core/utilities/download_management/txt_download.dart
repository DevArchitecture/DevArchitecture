import 'dart:io';
import 'dart:math';
import 'dart:typed_data';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/foundation.dart' show kDebugMode, kIsWeb;
import "package:universal_html/html.dart" as html;
import '../../constants/core_messages.dart';
import 'i_download.dart';
import '/core/di/core_initializer.dart';

class TxtDownload implements ITxtDownload {
  @override
  Future<void> download(List<Map<String, dynamic>> data) async {
    try {
      final StringBuffer buffer = StringBuffer();
      for (var row in data) {
        buffer.writeln(row.entries
            .map((entry) => '${entry.key}: ${entry.value}')
            .join(', '));
      }
      final textData = buffer.toString();

      if (kIsWeb) {
        saveTxtWeb(textData, 'data${Random().nextInt(10000000)}.txt');
      } else if (Platform.isAndroid || Platform.isIOS) {
        String? outputFilePath = await _getSavePathForMobileApps();
        if (outputFilePath != null) {
          final file = File(outputFilePath);
          await file.writeAsString(textData);
        }
      } else if (Platform.isMacOS || Platform.isLinux || Platform.isWindows) {
        String? outputFilePath = await _getSavePathForDesktop();
        if (outputFilePath != null) {
          final file = File(outputFilePath);
          await file.writeAsString(textData);
        }
      }
    } catch (e) {
      _showErrorMessage(CoreMessages.customerDefaultErrorMessage);
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
    }
  }

  void saveTxtWeb(String textData, String fileName) {
    final bytes = Uint8List.fromList(textData.codeUnits);
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
            '$selectedDirectory/data${Random().nextInt(10000000)}.txt';
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
        return "$selectedDirectory/data${Random().nextInt(10000000)}.txt";
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
