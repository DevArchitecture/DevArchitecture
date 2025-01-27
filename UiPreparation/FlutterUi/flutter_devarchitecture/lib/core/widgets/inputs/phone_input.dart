import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';
import '../../helpers/extensions.dart';

class CustomPhoneInput extends TextFormField {
  CustomPhoneInput({
    super.key,
    super.onChanged,
    required super.controller,
    super.enabled = true,
  }) : super(
          validator: (value) {
            if (!enabled!) return null;
            if (value == null || value.isEmpty) {
              return CoreScreenTexts.phoneNumber + CoreMessages.cantBeEmpty;
            }
            if (!value.isValidPhone) {
              return CoreMessages.invalidPhone;
            }
            return null;
          },
          inputFormatters: <TextInputFormatter>[],
          decoration: InputDecoration(
              labelText: CoreScreenTexts.phoneNumber,
              hintText: '+90 999 999 99 99',
              contentPadding: EdgeInsets.only(bottom: 20)),
        );
}
