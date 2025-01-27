import 'package:flutter/material.dart';
import '/core/theme/extensions.dart';
import '../../theme/custom_colors.dart';

class CustomDropdownButton extends StatefulWidget {
  final List<String> options;
  final void Function(String?) onChanged;
  final void Function(String?) getFirstValue;
  final IconData icon;

  const CustomDropdownButton({
    super.key,
    required this.options,
    required this.onChanged,
    required this.getFirstValue,
    required this.icon,
  });

  @override
  State<CustomDropdownButton> createState() => _VtDropdownButtonState();
}

class _VtDropdownButtonState extends State<CustomDropdownButton> {
  late String _firstValue;
  List<DropdownMenuItem<String>>? _dropdownItems;

  @override
  void initState() {
    super.initState();
    if (widget.options.isNotEmpty) {
      _firstValue = widget.options.first;
      widget.getFirstValue(_firstValue);
    } else {
      _firstValue = "";
    }

    // Dropdown öğelerini oluştur.
    _dropdownItems = widget.options.isNotEmpty
        ? widget.options.map((option) {
            return DropdownMenuItem(
              alignment: Alignment.topLeft,
              value: option,
              child: Text(option),
            );
          }).toList()
        : [
            const DropdownMenuItem(
              value: "",
              child: Text("Seçenek Yok"),
            ),
          ];
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: context.lowVerticalPadding,
      child: DropdownButtonFormField<String>(
        decoration: InputDecoration(
          prefixIcon: Icon(
            widget.icon,
            color: CustomColors.dark.getColor.withAlpha(150),
          ),
        ),
        style: TextStyle(
          color: CustomColors.dark.getColor,
          fontSize: 18.0,
          fontWeight: FontWeight.w300,
        ),
        isExpanded: true,
        items: _dropdownItems,
        value: _firstValue,
        onChanged: (value) {
          if (value != null) {
            setState(() {
              _firstValue = value;
            });
            widget.onChanged(value);
          }
        },
      ),
    );
  }
}
