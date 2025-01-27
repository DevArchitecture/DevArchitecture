import 'package:flutter/material.dart';

abstract class ITables {
  Widget getBasicTable(BuildContext context, List<Map<String, dynamic>> headers,
      List<Map<String, dynamic>> cells, Color headerColor,
      {bool isBordered = false});

  Widget getTableWithCustomManipulationButtons(
      BuildContext context,
      List<Map<String, dynamic>> headers,
      List<Map<String, dynamic>> cells,
      Color headerColor,
      List<Widget Function(BuildContext context, void Function())>
          customManipulationButton,
      List<ValueSetter<int>> customManipulationCallback,
      {bool isBordered = false,
      Widget? infoHover,
      Widget? addButton,
      Widget? utilityButton});
}
