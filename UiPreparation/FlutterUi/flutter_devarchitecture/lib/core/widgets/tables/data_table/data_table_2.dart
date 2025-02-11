import 'package:data_table_2/data_table_2.dart';
import 'package:flutter/material.dart';
import '../../../constants/core_messages.dart';
import '../../../theme/extensions.dart';
import '../../../theme/custom_colors.dart';
import '../i_tables.dart';
import 'custom_data_table_source.dart';

class DataTables implements ITables {
  @override
  Widget getBasicTable(BuildContext context, List<Map<String, dynamic>> headers,
      List<Map<String, dynamic>> cells, Color headerColor,
      {bool isBordered = false}) {
    cells.sort((a, b) => a.values.first.compareTo(b.values.first));
    var dataColumns = <DataColumn2>[];
    for (int i = 0; i < headers.length; i++) {
      dataColumns.add(DataColumn2(
          size: i == 0 ? ColumnSize.L : ColumnSize.S,
          label: Text(headers[i].values.first,
              style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 12,
                  color: headerColor.computeLuminance() > 0.5
                      ? Colors.black
                      : Colors.white))));
    }
    return Padding(
      padding: context.highHorizontalPadding,
      child: SizedBox(
        child: DataTable2(
            isHorizontalScrollBarVisible: true,
            isVerticalScrollBarVisible: true,
            columnSpacing: 10,
            horizontalMargin: 12,
            bottomMargin: 10,
            minWidth: 1000,
            border: isBordered
                ? TableBorder(
                    horizontalInside: BorderSide(
                        color: CustomColors.gray.getColor, width: 0.4),
                    verticalInside: BorderSide(
                        color: CustomColors.gray.getColor, width: 0.4),
                    borderRadius: BorderRadius.circular(5),
                    bottom: BorderSide(
                      color: CustomColors.gray.getColor,
                      width: 0.4,
                    ),
                    top: BorderSide(
                        color: CustomColors.gray.getColor, width: 0.4),
                    left: BorderSide(
                        color: CustomColors.gray.getColor, width: 0.4),
                    right: BorderSide(
                        color: CustomColors.gray.getColor, width: 0.4),
                  )
                : null,
            showCheckboxColumn: false,
            showBottomBorder: false,
            headingRowDecoration: BoxDecoration(
              color: headerColor,
              borderRadius: BorderRadius.circular(10),
            ),
            headingRowColor: WidgetStateProperty.all(headerColor),
            headingTextStyle: TextStyle(
              color: CustomColors.dark.getColor,
              fontWeight: FontWeight.bold,
              fontSize: 14,
            ),
            dividerThickness: 0.5,
            columns: dataColumns,
            rows: List<DataRow>.generate(cells.length, (index) {
              Map<String, dynamic> reformatedCell =
                  _reformatCell(headers, cells[index]);
              return DataRow(
                cells: _getDataCells(index, reformatedCell, headers.length),
              );
            })),
      ),
    );
  }

  @override
  Widget getTableWithCustomManipulationButtons(
    BuildContext context,
    List<Map<String, dynamic>> headers,
    List<Map<String, dynamic>> cells,
    Color headerColor,
    List<Widget Function(BuildContext context, void Function())>
        customManipulationButton,
    List<ValueSetter<int>> customManipulationCallback, {
    bool isBordered = false,
    Widget? infoHover,
    Widget? addButton,
    Widget? utilityButton,
  }) {
    cells.sort((a, b) => a.values.first.compareTo(b.values.first));
    var dataColumns = <DataColumn2>[];
    for (int i = 0; i < headers.length; i++) {
      if (i == 0) {
        dataColumns.add(DataColumn2(
            label: Text(
              style: TextStyle(
                  color: headerColor.computeLuminance() >= 0.5
                      ? Colors.black
                      : Colors.white),
              headers[i].values.first.length > 11 && headers.length > 5
                  ? "${headers[i].values.first.toString().substring(0, 8)}..."
                  : headers[i].values.first.toString(),
            ),
            fixedWidth: 96));
        continue;
      }
      dataColumns.add(DataColumn2(
          label: Text(
        style: TextStyle(
            color: headerColor.computeLuminance() > 0.5
                ? Colors.black
                : Colors.white),
        headers[i].values.first.length > 11 && headers.length > 5
            ? "${headers[i].values.first.toString().substring(0, 8)}..."
            : headers[i].values.first.toString(),
      )));
    }
    var headerButtonCount = infoHover != null ? 1 : 0;
    headerButtonCount += addButton != null ? 1 : 0;
    headerButtonCount += utilityButton != null ? 1 : 0;

    var rowButtonCount = customManipulationButton.length;

    if (rowButtonCount == 0) {
      if (infoHover != null) {
        dataColumns.add(DataColumn2(label: infoHover, fixedWidth: 32));
      }
      if (addButton != null) {
        dataColumns.add(DataColumn2(label: addButton, fixedWidth: 32));
      }
      if (utilityButton != null) {
        dataColumns.add(DataColumn2(label: utilityButton, fixedWidth: 32));
      }
    } else {
      for (var i = 0; i < rowButtonCount - headerButtonCount; i++) {
        dataColumns.add(const DataColumn2(label: Text(" "), fixedWidth: 32));
      }
      if (infoHover != null) {
        dataColumns.add(DataColumn2(label: infoHover, fixedWidth: 32));
      }
      if (addButton != null) {
        dataColumns.add(DataColumn2(label: addButton, fixedWidth: 32));
      }
      if (utilityButton != null) {
        dataColumns.add(DataColumn2(label: utilityButton, fixedWidth: 32));
      }
    }
    return Padding(
        padding: context.highHorizontalPadding,
        child: SizedBox(
            child: PaginatedDataTable2(
          isHorizontalScrollBarVisible: true,
          isVerticalScrollBarVisible: true,
          minWidth: 1000,
          headingRowColor:
              WidgetStateColor.resolveWith((states) => Colors.grey[200]!),
          horizontalMargin: 20,
          checkboxHorizontalMargin: 12,
          columnSpacing: 0,
          wrapInCard: false,
          renderEmptyRowsInTheEnd: false,
          availableRowsPerPage: const [2, 5, 10, 30, 100],
          empty: Center(
              child: Container(
                  padding: const EdgeInsets.all(20),
                  child: Text(CoreMessages.noDataAvailable))),
          border: isBordered
              ? TableBorder(
                  horizontalInside:
                      BorderSide(color: CustomColors.gray.getColor, width: 0.4),
                  verticalInside:
                      BorderSide(color: CustomColors.gray.getColor, width: 0.4),
                  borderRadius: BorderRadius.circular(5),
                  bottom: BorderSide(
                    color: CustomColors.gray.getColor,
                    width: 0.4,
                  ),
                  top:
                      BorderSide(color: CustomColors.gray.getColor, width: 0.4),
                  left:
                      BorderSide(color: CustomColors.gray.getColor, width: 0.4),
                  right:
                      BorderSide(color: CustomColors.gray.getColor, width: 0.4),
                )
              : null,
          showCheckboxColumn: false,
          headingRowDecoration: BoxDecoration(
            color: headerColor,
            borderRadius: BorderRadius.circular(10),
          ),
          headingTextStyle: TextStyle(
            color: CustomColors.dark.getColor,
            fontWeight: FontWeight.bold,
            fontSize: 16,
          ),
          dividerThickness: 0.5,
          columns: dataColumns,
          source: _buildDataTableSource(
              context,
              headers,
              cells,
              customManipulationButton,
              customManipulationCallback,
              rowButtonCount,
              headerButtonCount),
        )));
  }

  List<DataCell> _getDataCells(
      index, Map<String, dynamic> objList, int headerCount) {
    List<DataCell> dataCells = [];
    objList.forEach((_, v) => dataCells.add(DataCell(
        onTap: () {},
        Text(
            v.toString().length > 11 && headerCount > 5
                ? "${v.toString().substring(0, 11)}..."
                : v.toString(),
            style:
                const TextStyle(fontWeight: FontWeight.bold, fontSize: 12)))));
    return dataCells;
  }

  Map<String, dynamic> _reformatCell(
      List<Map<String, dynamic>> table, Map<String, dynamic> cell) {
    Map<String, dynamic> reformatedCell = {};
    for (int i = 0; i < table.length; i++) {
      reformatedCell[table[i].keys.first] = cell[table[i].keys.first];
    }
    return reformatedCell;
  }

  DataTableSource _buildDataTableSource(
    BuildContext context,
    List<Map<String, dynamic>> headers,
    List<Map<String, dynamic>> cells,
    List<Widget Function(BuildContext context, void Function())>
        customManipulationButton,
    List<ValueSetter<int>> customManipulationCallback,
    int rowButtonCount,
    int headerButtonCount,
  ) {
    var dataRows = List<DataRow>.generate(cells.length, (index) {
      Map<String, dynamic> reformatedCell =
          _reformatCell(headers, cells[index]);
      var resultCells = _getDataCells(index, reformatedCell, headers.length);
      if (rowButtonCount == 0) {
        for (var i = 0; i < headerButtonCount; i++) {
          resultCells.add(const DataCell(Text("")));
        }
      } else if (rowButtonCount <= 3) {
        for (var i = 0; i < headerButtonCount - rowButtonCount; i++) {
          resultCells.add(const DataCell(Text("")));
        }
        for (int j = 0; j < customManipulationButton.length; j++) {
          resultCells.add(DataCell(customManipulationButton[j](context, () {
            customManipulationCallback[j](cells[index]["id"]);
          })));
        }
      } else {
        for (int j = 0; j < customManipulationButton.length; j++) {
          resultCells.add(DataCell(customManipulationButton[j](context, () {
            customManipulationCallback[j](cells[index]["id"]);
          })));
        }
      }

      return DataRow(cells: resultCells);
    });
    var dataSource = CustomDataTableSource(dataRows);
    return dataSource;
  }
}
