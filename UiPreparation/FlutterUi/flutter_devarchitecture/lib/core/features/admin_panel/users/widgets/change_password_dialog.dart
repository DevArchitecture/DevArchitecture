import 'package:flutter/material.dart';
import 'package:flutter_devarchitecture/core/constants/core_screen_texts.dart';
import '../../../../constants/core_messages.dart';
import '../user_constants/user_screen_texts.dart';
import '/core/theme/extensions.dart';

class ChangeUserPasswordDialog extends StatefulWidget {
  final int userId;
  const ChangeUserPasswordDialog({Key? key, required this.userId})
      : super(key: key);

  @override
  _ChangeUserPasswordDialogState createState() =>
      _ChangeUserPasswordDialogState();
}

class _ChangeUserPasswordDialogState extends State<ChangeUserPasswordDialog> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _passwordController;
  late TextEditingController _confirmPasswordController;

  @override
  void initState() {
    super.initState();
    _passwordController = TextEditingController();
    _confirmPasswordController = TextEditingController();
  }

  @override
  void dispose() {
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(UserScreenTexts.changePassword),
      content: Form(
        key: _formKey,
        child: SingleChildScrollView(
          child: Container(
            width: context.percent40Screen,
            height: context.percent15Screen,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Expanded(
                  flex: 5,
                  child: TextFormField(
                    controller: _passwordController,
                    obscureText: true,
                    decoration:
                        InputDecoration(labelText: UserScreenTexts.newPassword),
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
                    controller: _confirmPasswordController,
                    obscureText: true,
                    decoration: InputDecoration(
                        labelText: UserScreenTexts.confirmPassword),
                    validator: (value) {
                      if (value == null || value != _passwordController.text) {
                        return CoreMessages.passwordsDoNotMatch;
                      }
                      return null;
                    },
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
            if (_formKey.currentState!.validate()) {
              Navigator.of(context).pop(_passwordController.text);
            }
          },
          child: Text(CoreScreenTexts.saveButton),
        ),
      ],
    );
  }
}
