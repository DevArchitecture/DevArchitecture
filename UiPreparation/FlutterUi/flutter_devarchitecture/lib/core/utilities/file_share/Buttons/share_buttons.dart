import 'package:flutter/material.dart';
import '../../../constants/core_screen_texts.dart';
import '../../../di/core_initializer.dart';

class ShareButtons {
  final List<Map<String, dynamic>> data;

  ShareButtons({required this.data});

  IconButton pdfButton(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.picture_as_pdf),
      tooltip: CoreScreenTexts.pdfShareTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.pdfShare.share(data);
      },
    );
  }

  IconButton excelButton(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.table_chart),
      tooltip: CoreScreenTexts.excelShareTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.excelShare.share(data);
      },
    );
  }

  IconButton txtButton(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.description),
      tooltip: CoreScreenTexts.txtShareTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.txtShare.share(data);
      },
    );
  }

  IconButton jsonButton(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.code),
      tooltip: CoreScreenTexts.jsonShareTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.jsonShare.share(data);
      },
    );
  }

  IconButton xmlButton(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.code),
      tooltip: CoreScreenTexts.xmlShareTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.xmlShare.share(data);
      },
    );
  }

  IconButton imageButton(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.image),
      tooltip: CoreScreenTexts.imageShareTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.imageShare.share(data);
      },
    );
  }

  IconButton csvButton(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.table_chart),
      tooltip: CoreScreenTexts.csvShareTooltip,
      onPressed: () {
        CoreInitializer().coreContainer.csvShare.share(data);
      },
    );
  }
}
