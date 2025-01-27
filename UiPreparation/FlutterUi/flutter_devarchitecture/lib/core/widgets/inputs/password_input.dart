import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';
import '../../theme/custom_colors.dart';
import '../../helpers/extensions.dart';

class CustomPasswordInput extends StatefulWidget {
  final TextEditingController passwordController;
  final BuildContext context;
  final bool enabled;

  const CustomPasswordInput(
      {super.key,
      required this.context,
      required this.passwordController,
      this.enabled = true});

  @override
  PasswordFieldState createState() => PasswordFieldState();
}

class PasswordFieldState extends State<CustomPasswordInput> {
  final _textFieldFocusNode = FocusNode();
  bool _obscured = true;

  void _toggleObscured() {
    setState(() {
      _obscured = !_obscured;
      if (_textFieldFocusNode.hasPrimaryFocus) {
        return;
      }
      _textFieldFocusNode.canRequestFocus = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      enabled: widget.enabled,
      validator: (value) {
        if (!widget.enabled) return null;
        if (value == null || value.isEmpty) {
          return CoreScreenTexts.password + CoreMessages.cantBeEmpty;
        }
        if (!value.isValidPassword) {
          return CoreMessages.invalidPassword;
        }
        return null;
      },
      obscuringCharacter: "*",
      controller: widget.passwordController,
      keyboardType: TextInputType.visiblePassword,
      obscureText: _obscured,
      focusNode: _textFieldFocusNode,
      inputFormatters: <TextInputFormatter>[],
      decoration: InputDecoration(
        contentPadding: const EdgeInsets.only(bottom: 20),
        labelText: CoreScreenTexts.password,
        hintText: "***********",
        filled: true,
        fillColor: Colors.transparent,
        isDense: true,
        prefixIcon: Icon(
          Icons.lock_rounded,
          size: 24,
          color: CustomColors.dark.getColor.withAlpha(150),
        ),
        suffixIcon: Padding(
          padding: const EdgeInsets.fromLTRB(0, 0, 4, 0),
          child: GestureDetector(
            onTap: _toggleObscured,
            child: Icon(
              _obscured
                  ? Icons.visibility_rounded
                  : Icons.visibility_off_rounded,
              size: 24,
              color: CustomColors.dark.getColor.withAlpha(150),
            ),
          ),
        ),
      ),
    );
  }
}
