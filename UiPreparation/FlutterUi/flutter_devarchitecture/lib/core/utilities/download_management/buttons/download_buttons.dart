import 'package:flutter/material.dart';
import '../../../constants/core_screen_texts.dart';
import '../../../di/core_initializer.dart';
import '../../../theme/custom_colors.dart';

class DownloadButtons {
  final List<Map<String, dynamic>> data;
  Color? color;

  DownloadButtons({
    this.color,
    required this.data,
  }) {
    color ??= CustomColors.white.getColor;
  }

  IconButton pdfButton(BuildContext context) {
    return IconButton(
      color: color,
      icon: Icon(Icons.picture_as_pdf),
      tooltip: CoreScreenTexts.pdfDownloadTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.pdfDownload.download(data);
      },
    );
  }

  IconButton excelButton(BuildContext context) {
    return IconButton(
      color: color,
      icon: Icon(Icons.table_chart),
      tooltip: CoreScreenTexts.excelDownloadTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.excelDownload.download(data);
      },
    );
  }

  IconButton txtButton(BuildContext context) {
    return IconButton(
      color: color,
      icon: Icon(Icons.description),
      tooltip: CoreScreenTexts.txtDownloadTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.txtDownload.download(data);
      },
    );
  }

  IconButton jsonButton(BuildContext context) {
    return IconButton(
      color: color,
      icon: Icon(Icons.code),
      tooltip: CoreScreenTexts.jsonDownloadTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.jsonDownload.download(data);
      },
    );
  }

  IconButton xmlButton(BuildContext context) {
    return IconButton(
      color: color,
      icon: Icon(Icons.code),
      tooltip: CoreScreenTexts.xmlDownloadTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.xmlDownload.download(data);
      },
    );
  }

  IconButton imageButton(BuildContext context) {
    return IconButton(
      color: color,
      icon: Icon(Icons.image),
      tooltip: CoreScreenTexts.imageDownloadTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.imageDownload.download(data);
      },
    );
  }

  IconButton csvButton(BuildContext context) {
    return IconButton(
      color: color,
      icon: Icon(Icons.table_chart),
      tooltip: CoreScreenTexts.csvDownloadTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.csvDownload.download(data);
      },
    );
  }
}
