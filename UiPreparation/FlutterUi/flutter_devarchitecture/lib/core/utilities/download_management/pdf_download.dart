import 'dart:math';

import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:file_picker/file_picker.dart';
import 'dart:io';
import 'package:flutter/services.dart';
import 'package:flutter/foundation.dart' show kDebugMode, kIsWeb;
import "package:universal_html/html.dart" as html;

import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';
import '../../di/core_initializer.dart';
import 'i_download.dart';

class PdfDownload implements IPdfDownload {
  @override
  Future<void> download(List<Map<String, dynamic>> data) async {
    try {
      final pdf = pw.Document();

      final regularFontData =
          await rootBundle.load("assets/fonts/NotoSans-Regular.ttf");
      final boldFontData =
          await rootBundle.load("assets/fonts/NotoSans-Bold.ttf");

      final regularFont = pw.Font.ttf(regularFontData);
      final boldFont = pw.Font.ttf(boldFontData);

      int columnCount = data.isNotEmpty ? data.first.keys.length : 0;
      pw.PageOrientation orientation = columnCount > 5
          ? pw.PageOrientation.landscape
          : pw.PageOrientation.portrait;

      pdf.addPage(
        pw.Page(
          orientation: orientation,
          build: (pw.Context context) {
            return pw.Column(
              crossAxisAlignment: pw.CrossAxisAlignment.start,
              children: [
                pw.Text(
                  CoreScreenTexts.dataTableTitle,
                  style: pw.TextStyle(font: boldFont, fontSize: 24),
                ),
                pw.SizedBox(height: 20),
                pw.TableHelper.fromTextArray(
                  headers: data.isNotEmpty ? data.first.keys.toList() : [],
                  data: data.map((item) => item.values.toList()).toList(),
                  border: pw.TableBorder.all(width: 2),
                  headerStyle: pw.TextStyle(
                    font: boldFont,
                    fontSize: 12,
                    color: PdfColors.white,
                  ),
                  headerDecoration: pw.BoxDecoration(
                    color: PdfColors.blue,
                  ),
                  cellStyle: pw.TextStyle(
                    font: regularFont,
                    fontSize: 10,
                  ),
                  cellHeight: 30,
                  cellAlignments: {
                    for (int i = 0; i < data.first.keys.length; i++)
                      i: pw.Alignment.centerLeft,
                  },
                ),
              ],
            );
          },
        ),
      );

      final output = await pdf.save();

      if (kIsWeb) {
        savePdfWeb(output, 'data${Random().nextInt(10000000)}.pdf');
      } else if (Platform.isAndroid || Platform.isIOS) {
        String? outputFilePath = await _getSavePathForMobileApps();
        if (outputFilePath != null) {
          final file = File(outputFilePath);
          await file.writeAsBytes(output);
        }
      } else if (Platform.isMacOS || Platform.isLinux || Platform.isWindows) {
        String? outputFilePath = await _getSavePathForDesktop();
        if (outputFilePath != null) {
          final file = File(outputFilePath);
          await file.writeAsBytes(output);
        }
      }
    } catch (e) {
      _showErrorMessage(CoreMessages.customerDefaultErrorMessage);
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
    }
  }

  void savePdfWeb(Uint8List bytes, String fileName) {
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
            '$selectedDirectory/data${Random().nextInt(10000000)}.pdf';
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
        return "$selectedDirectory/data${Random().nextInt(10000000)}.pdf";
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
