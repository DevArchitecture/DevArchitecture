import 'package:flutter/material.dart';
import '../../../../constants/core_messages.dart';
import '../../../../constants/core_screen_texts.dart';
import '../language_constants/language_screen_texts.dart';
import '/core/theme/extensions.dart';
import '../models/language.dart';

class UpdateLanguageDialog extends StatefulWidget {
  final Language language;

  const UpdateLanguageDialog({Key? key, required this.language})
      : super(key: key);

  @override
  _UpdateLanguageDialogState createState() => _UpdateLanguageDialogState();
}

class _UpdateLanguageDialogState extends State<UpdateLanguageDialog> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _codeController;
  late TextEditingController _nameController;

  @override
  void initState() {
    super.initState();
    _codeController = TextEditingController(text: widget.language.code);
    _nameController = TextEditingController(text: widget.language.name);
  }

  @override
  void dispose() {
    _codeController.dispose();
    _nameController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(LanguageScreenTexts.updateLanguage),
      content: Form(
        key: _formKey,
        child: Container(
          width: context.percent40Screen,
          height: context.percent15Screen,
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Expanded(
                flex: 5,
                child: TextFormField(
                  controller: _codeController,
                  decoration: const InputDecoration(labelText: 'Kod'),
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return CoreMessages.cantBeEmpty;
                    }
                    return null;
                  },
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: TextFormField(
                  controller: _nameController,
                  decoration: InputDecoration(labelText: CoreScreenTexts.name),
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return CoreMessages.cantBeEmpty;
                    }
                    return null;
                  },
                ),
              ),
            ],
          ),
        ),
      ),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: Text(CoreScreenTexts.cancel),
        ),
        ElevatedButton(
          onPressed: () {
            if (_formKey.currentState!.validate()) {
              final updatedLanguage = Language(
                id: widget.language.id,
                code: _codeController.text,
                name: _nameController.text,
              );
              Navigator.of(context).pop(updatedLanguage);
            }
          },
          child: Text(CoreScreenTexts.updateButton),
        ),
      ],
    );
  }
}
