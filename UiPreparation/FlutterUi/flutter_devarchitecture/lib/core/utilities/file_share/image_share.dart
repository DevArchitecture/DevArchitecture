import 'dart:io';
import 'dart:math';
import 'package:flutter/foundation.dart';
import 'package:image/image.dart' as img;
import 'package:path_provider/path_provider.dart';
import 'package:share_plus/share_plus.dart';
import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';
import '../../di/core_initializer.dart';
import 'i_share.dart';

class ImageShare implements IImageShare {
  @override
  Future<void> share(List<Map<String, dynamic>> data) async {
    try {
      final int startX = 10;
      final int startY = 10;
      final int rowHeight = 30;
      final int colWidth = 250;

      final width = (colWidth * data.first.keys.length) + 200;
      final height = (rowHeight * data.length) + 200;
      final image = img.Image(width: width, height: height);
      img.fill(image, color: img.ColorRgb8(255, 255, 255));

      if (data.isNotEmpty) {
        final headers = data.first.keys.toList();
        for (int i = 0; i < headers.length; i++) {
          img.drawString(
            image,
            headers[i],
            font: img.arial14,
            x: startX + i * colWidth,
            y: startY,
            color: img.ColorRgb8(0, 0, 0),
          );
        }
      }

      for (int rowIndex = 0; rowIndex < data.length; rowIndex++) {
        var row = data[rowIndex];
        var values = row.values.toList();
        for (int colIndex = 0; colIndex < values.length; colIndex++) {
          img.drawString(
            image,
            values[colIndex].toString(),
            font: img.arial14,
            x: startX + colIndex * colWidth,
            y: startY + (rowIndex + 1) * rowHeight,
            color: img.ColorRgb8(0, 0, 0),
          );
        }
      }

      final directory = await getTemporaryDirectory();
      final path = '${directory.path}/data${Random().nextInt(10000000)}.png';
      final file = File(path);
      await file.writeAsBytes(img.encodePng(image));

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
