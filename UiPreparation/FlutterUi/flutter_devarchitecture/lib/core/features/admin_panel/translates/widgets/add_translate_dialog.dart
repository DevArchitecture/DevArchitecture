import 'package:flutter/material.dart';
import '../../../../constants/core_screen_texts.dart';
import '../translate_constants/translate_Screen_texts.dart';
import '/core/theme/extensions.dart';
import '../../lookups/models/lookup.dart';
import '../../languages/widgets/languages_dropdown_button.dart';
import '../models/translate.dart';
import '../../../../widgets/inputs/text_input.dart';

class AddTranslateDialog extends StatefulWidget {
  const AddTranslateDialog({Key? key}) : super(key: key);

  @override
  _AddTranslateDialogState createState() => _AddTranslateDialogState();
}

class _AddTranslateDialogState extends State<AddTranslateDialog> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _codeController;
  late TextEditingController _valueController;
  LookUp? _selectedLanguage;

  @override
  void initState() {
    super.initState();
    _codeController = TextEditingController();
    _valueController = TextEditingController();
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
      title: Text(TranslateScreenTexts.addTranslate),
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
              final newTranslate = Translate(
                id: _selectedLanguage!.id,
                langId: _selectedLanguage!.id,
                code: _codeController.text,
                value: _valueController.text,
              );
              Navigator.of(context).pop(newTranslate);
            }
          },
          child: Text(CoreScreenTexts.saveButton),
        ),
      ],
    );
  }
}
