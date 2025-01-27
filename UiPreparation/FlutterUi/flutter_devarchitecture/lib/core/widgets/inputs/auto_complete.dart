import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import '../../constants/core_messages.dart';
import '../../theme/custom_colors.dart';

class CustomAutoComplete extends StatelessWidget {
  final List<Map<String, dynamic>> options;
  final String labelText;
  final String hintText;
  final void Function(int) onChanged;
  final double contentPadding;
  final TextEditingController controller;
  final FocusNode focusNode;
  final String valueKey;
  final bool isUpperCase;
  final bool enabled;
  const CustomAutoComplete(
      {super.key,
      required this.options,
      required this.labelText,
      required this.hintText,
      required this.onChanged,
      this.enabled = true,
      this.valueKey = "name",
      this.isUpperCase = false,
      this.contentPadding = 20,
      required this.controller,
      required this.focusNode});

  @override
  Widget build(BuildContext context) {
    return RawAutocomplete<String>(
      focusNode: focusNode,
      textEditingController: controller,
      fieldViewBuilder: (context, controller, focusNode, onFieldSubmitted) {
        return TextFormField(
          enabled: enabled,
          validator: (value) {
            if (!enabled) return null;
            if ((value == null || value.isEmpty || value == "")) {
              return '$labelText ${CoreMessages.cantBeEmpty}';
            }
            for (var i = 0; i < options.length; i++) {
              if (options[i][valueKey].toString().toUpperCase() ==
                  value.toString().toUpperCase()) {
                return null;
              }
            }
            return '${CoreMessages.invalid} $labelText';
          },
          controller: controller,
          focusNode: focusNode,
          decoration: InputDecoration(
            suffixIcon: Padding(
              padding: const EdgeInsets.only(bottom: 10, left: 15),
              child: Icon(Icons.arrow_drop_down,
                  color: options.isNotEmpty
                      ? CustomColors.dark.getColor
                      : CustomColors.light.getColor),
            ),
            labelText: labelText,
            hintText: hintText,
            contentPadding: EdgeInsets.only(
              bottom: contentPadding,
            ),
          ),
        );
      },
      optionsBuilder: (TextEditingValue textEditingValue) {
        List<String> optionsNames = options.map((e) {
          return isUpperCase
              ? e[valueKey].toString().toUpperCase()
              : e[valueKey].toString();
        }).toList();
        if (textEditingValue.text.isNotEmpty) {
          return optionsNames.where((String option) {
            return option.contains(isUpperCase
                ? textEditingValue.text.toUpperCase()
                : textEditingValue.text);
          });
        }
        return optionsNames;
      },
      optionsViewOpenDirection: OptionsViewOpenDirection.down,
      onSelected: (value) {
        if (value.isEmpty || value == "") {}
        int id = 0;
        for (var i = 0; i < options.length; i++) {
          if (options[i][valueKey].toString().toUpperCase() ==
              value.toUpperCase()) {
            id = options[i]["id"];
          }
        }
        onChanged(id);
      },
      optionsViewBuilder: (context, onSelected, options) {
        return Align(
          alignment: Alignment.topLeft,
          child: Material(
            elevation: 4.0,
            child: ConstrainedBox(
              constraints: const BoxConstraints(maxHeight: 200, maxWidth: 500),
              child: ListView.builder(
                padding: EdgeInsets.zero,
                shrinkWrap: true,
                itemCount: options.length,
                itemBuilder: (BuildContext context, int index) {
                  final option = options.elementAt(index);
                  return InkWell(
                    onTap: () {
                      onSelected(option);
                    },
                    child: Builder(builder: (BuildContext context) {
                      final bool highlight =
                          AutocompleteHighlightedOption.of(context) == index;
                      if (highlight) {
                        SchedulerBinding.instance
                            .addPostFrameCallback((Duration timeStamp) {
                          Scrollable.ensureVisible(context, alignment: 0.5);
                        });
                      }
                      return Container(
                        color: highlight ? Theme.of(context).focusColor : null,
                        padding: const EdgeInsets.all(16.0),
                        child: Text(option),
                      );
                    }),
                  );
                },
              ),
            ),
          ),
        );
      },
    );
  }
}
