import 'package:flutter/material.dart';
import '../../../../constants/core_screen_texts.dart';
import '../translate_constants/translate_Screen_texts.dart';
import '/core/theme/extensions.dart';
import '../../lookups/models/lookup.dart';
import '../../languages/widgets/languages_dropdown_button.dart';
import '../models/translate.dart';
import '../../../../widgets/inputs/text_input.dart';

class UpdateTranslateDialog extends StatefulWidget {
  final Translate translate;

  const UpdateTranslateDialog({Key? key, required this.translate})
      : super(key: key);

  @override
  _UpdateTranslateDialogState createState() => _UpdateTranslateDialogState();
}

class _UpdateTranslateDialogState extends State<UpdateTranslateDialog> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _codeController;
  late TextEditingController _valueController;
  LookUp? _selectedLanguage;

  @override
  void initState() {
    super.initState();
    _codeController = TextEditingController(text: widget.translate.code);
    _valueController = TextEditingController(text: widget.translate.value);
    _selectedLanguage = LookUp(
      id: widget.translate.langId,
      label: '',
    );
  }

  @override
  void dispose() {
    _codeController.dispose();
    _valueController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(TranslateScreenTexts.updateTranslate),
      content: Form(
        key: _formKey,
        child: SingleChildScrollView(
          child: Container(
            width: context.percent40Screen,
            height: context.percent25Screen,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Expanded(
                  flex: 5,
                  child: LanguageDropdownButton(
                    onChanged: (selectedLanguage) {
                      _selectedLanguage = selectedLanguage;
                    },
                    getInitialValue: (initialValue) {
                      _selectedLanguage = initialValue;
                    },
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomTextInput(
                    controller: _codeController,
                    labelText: TranslateScreenTexts.code,
                    hintText: TranslateScreenTexts.codeHint,
                    min: 1,
                    max: 50,
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomTextInput(
                    controller: _valueController,
                    labelText: TranslateScreenTexts.value,
                    hintText: TranslateScreenTexts.valueHint,
                    min: 1,
                    max: 100,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: Text(CoreScreenTexts.cancelButton),
        ),
        ElevatedButton(
          onPressed: () {
            if (_formKey.currentState!.validate() &&
                _selectedLanguage != null) {
              final updatedTranslate = Translate(
                id: int.parse(_selectedLanguage!.id.toString()),
                langId: int.parse(_selectedLanguage!.id.toString()),
                code: _codeController.text,
                value: _valueController.text,
              );
              Navigator.of(context).pop(updatedTranslate);
            }
          },
          child: Text(CoreScreenTexts.updateButton),
        ),
      ],
    );
  }
}
