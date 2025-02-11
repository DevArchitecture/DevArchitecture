import 'package:flutter/material.dart';
import '/core/theme/extensions.dart';
import '../../../layouts/base_scaffold.dart';
import '../../theme/custom_colors.dart';

class ColorPalettePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Padding(
        padding: context.highestPadding,
        child: ListView(
          children: CustomColors.values.map((color) {
            return _buildColorTile(color);
          }).toList(),
        ),
      ),
    );
  }

  Widget _buildColorTile(CustomColors color) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Container(
        decoration: BoxDecoration(
          color: color.getColor,
          borderRadius: BorderRadius.circular(8.0),
        ),
        padding: const EdgeInsets.all(16.0),
        child: Text(
          color.toString().split('.').last,
          style: TextStyle(
            color: color == CustomColors.dark ? Colors.white : Colors.black,
            fontSize: 18.0,
          ),
        ),
      ),
    );
  }
}
