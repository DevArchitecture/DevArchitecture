import 'dart:math';

import 'package:flutter/foundation.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:path_provider/path_provider.dart';
import 'package:share_plus/share_plus.dart';
import 'dart:io';
import 'package:flutter/services.dart';
import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';
import '../../di/core_initializer.dart';
import 'i_share.dart';

class PdfShare implements IPdfShare {
  @override
  Future<void> share(List<Map<String, dynamic>> data) async {
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
      final directory = await getTemporaryDirectory();
      final path = '${directory.path}/data${Random().nextInt(10000000)}.pdf';
      final file = File(path);
      await file.writeAsBytes(await pdf.save());

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
