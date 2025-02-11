import 'package:flutter/material.dart';

class CustomDataTableSource extends DataTableSource {
  final List<DataRow> dataRows;
  CustomDataTableSource(this.dataRows);

  @override
  DataRow? getRow(int index) {
    assert(index >= 0);
    if (index >= dataRows.length) return null;
    final DataRow dataRow = dataRows[index];
    return DataRow.byIndex(index: index, cells: dataRow.cells);
  }

  @override
  bool get isRowCountApproximate => false;

  @override
  int get rowCount => dataRows.length;

  @override
  int get selectedRowCount => 0;
}
