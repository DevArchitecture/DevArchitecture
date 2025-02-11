import 'package:flutter/material.dart';
import '../../constants/core_screen_texts.dart';
import '/core/widgets/inputs/text_input.dart';
import '../../di/core_initializer.dart';

class FilterTableWidget extends StatefulWidget {
  final List<Map<String, dynamic>> datas;
  final List<Map<String, dynamic>> headers;
  final Color color;
  final List<Widget Function(BuildContext, void Function())>
      customManipulationButton;
  final List<void Function(int)> customManipulationCallback;
  final Widget? infoHover;
  final Widget? addButton;
  final Widget? utilityButton;

  const FilterTableWidget({
    super.key,
    required this.datas,
    required this.headers,
    required this.color,
    required this.customManipulationButton,
    required this.customManipulationCallback,
    this.infoHover,
    this.addButton,
    this.utilityButton,
  });

  @override
  State<FilterTableWidget> createState() => _FilterTableWidgetState();
}

class _FilterTableWidgetState extends State<FilterTableWidget> {
  List<Map<String, dynamic>> filteredData = [];
  final TextEditingController controller = TextEditingController();

  @override
  void initState() {
    _reformatNullToEmptyString();
    filteredData = widget.datas;
    super.initState();
  }

  @override
  void didUpdateWidget(covariant FilterTableWidget oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.datas != widget.datas) {
      setState(() {
        filteredData = widget.datas;
      });
    }
  }

  void _reformatNullToEmptyString() {
    for (var data in widget.datas) {
      for (var header in widget.headers) {
        final key = header.keys.first;
        if (!data.containsKey(key) || data[key] == null) {
          data[key] = "";
        }
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Column(children: [
      Expanded(
          child: Row(
        children: [
          const Spacer(
            flex: 10,
          ),
          Expanded(
            flex: 2,
            child: CustomTextInput(
              labelText: CoreScreenTexts.search,
              hintText: "",
              min: 3,
              max: 15,
              controller: controller,
              enabled: true,
              onChanged: (value) {
                if (mounted) {
                  setState(() {
                    filteredData = [];
                    if (value == "" || value.isEmpty) {
                      filteredData = widget.datas;
                      return;
                    }
                    for (var i = 0; i < widget.datas.length; i++) {
                      var key = "";
                      for (var element in widget.datas[i].values
                          .map((e) => e.toString())
                          .toList()) {
                        key = key + element.toString().toLowerCase() + " ";
                      }
                      if (key.contains(value.toLowerCase())) {
                        filteredData.add(widget.datas[i]);
                      }
                    }
                  });
                }
              },
            ),
          ),
          const Spacer(),
        ],
      )),
      Expanded(
          flex: 5,
          child: CoreInitializer()
              .coreContainer
              .dataTable
              .getTableWithCustomManipulationButtons(
                  context,
                  widget.headers,
                  filteredData,
                  widget.color,
                  widget.customManipulationButton,
                  widget.customManipulationCallback,
                  infoHover: widget.infoHover,
                  addButton: widget.addButton,
                  utilityButton: widget.utilityButton)),
      const Spacer(),
    ]);
  }
}
