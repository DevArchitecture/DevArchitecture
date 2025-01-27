import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_devarchitecture/core/constants/core_messages.dart';

class SingleSplitInput extends StatefulWidget {
  final String format;
  final String labelText;
  final List<TextEditingController> controllers;
  final List<FocusNode> confirmFocusNodes;
  final bool enabled;

  final List<String> hintTexts;
  final void Function(dynamic) onChange;
  const SingleSplitInput(
      {super.key,
      required this.format,
      required this.labelText,
      required this.controllers,
      required this.onChange,
      required this.hintTexts,
      required this.confirmFocusNodes,
      this.enabled = true});

  @override
  State<SingleSplitInput> createState() => _SplitInputState();
}

class _SplitInputState extends State<SingleSplitInput> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        ...cells(),
      ],
    );
  }

  cells() {
    List<Widget> cellList = [];
    final listOfCells = widget.format.split(' ');
    for (String element in listOfCells) {
      final type = element[0];
      final flex = int.parse(element.substring(1, element.length));
      final index = listOfCells.indexOf(element);
      cellList.add(
        Expanded(
          flex: flex,
          child: Padding(
            padding: const EdgeInsets.only(right: 8.0),
            child: TextFormField(
              enabled: widget.enabled,
              enableInteractiveSelection: true,
              focusNode: widget.confirmFocusNodes[index],
              onChanged: (value) {
                widget.onChange(value);
                if (value.length == flex) {
                  if (index == listOfCells.length - 1) {
                    return;
                  }
                  widget.confirmFocusNodes[index].nextFocus();
                }
                if (value.isEmpty) {
                  if (index == 0) {
                    return;
                  }
                  widget.confirmFocusNodes[index].previousFocus();
                }
              },
              validator: (value) {
                if (!widget.enabled) return null;
                if (value == null || value.isEmpty) {}
                if (value == "" || value == null) {
                  return '${widget.labelText} ${CoreMessages.cantBeEmpty}';
                }
                if (value.length > flex) {
                  return '${widget.labelText} ${CoreMessages.maximum} $flex';
                }
                return null;
              },
              decoration: InputDecoration(
                contentPadding: const EdgeInsets.only(bottom: 10),
                labelText: index == 0 ? widget.labelText : "",
                hintText: widget.hintTexts[index],
              ),
              controller: widget.controllers[index],
              textInputAction: TextInputAction.next,
              textAlign: TextAlign.center,
              inputFormatters: <TextInputFormatter>[
                FilteringTextInputFormatter.allow(
                  type == 's' ? RegExp(r'^[a-zA-Z]+$') : RegExp(r"^\d+$"),
                )
              ],
              keyboardType:
                  type == 's' ? TextInputType.text : TextInputType.number,
              maxLength: flex,
            ),
          ),
        ),
      );
    }
    return cellList;
  }
}
