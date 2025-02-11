import 'package:flutter/material.dart';
import 'package:flutter_devarchitecture/core/constants/core_screen_texts.dart';
import '../user_constants/user_screen_texts.dart';
import '/core/theme/extensions.dart';
import '../../../../widgets/inputs/date_input.dart';
import '../../../../widgets/inputs/dropdown_button.dart';
import '../../../../widgets/inputs/email_input.dart';
import '../../../../widgets/inputs/phone_input.dart';
import '../../../../widgets/inputs/text_input.dart';
import '../models/user.dart';

class AddUserDialog extends StatefulWidget {
  const AddUserDialog({Key? key}) : super(key: key);

  @override
  _AddUserDialogState createState() => _AddUserDialogState();
}

class _AddUserDialogState extends State<AddUserDialog> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _emailController;
  late TextEditingController _fullNameController;
  late TextEditingController _statusController;
  late TextEditingController _mobilePhonesController;
  late TextEditingController _addressController;
  late TextEditingController _notesController;
  late TextEditingController _dateController;

  @override
  void initState() {
    super.initState();
    _emailController = TextEditingController();
    _fullNameController = TextEditingController();
    _statusController = TextEditingController();
    _mobilePhonesController = TextEditingController();
    _addressController = TextEditingController();
    _notesController = TextEditingController();
    _dateController = TextEditingController();
  }

  @override
  void dispose() {
    _emailController.dispose();
    _fullNameController.dispose();
    _statusController.dispose();
    _mobilePhonesController.dispose();
    _addressController.dispose();
    _notesController.dispose();
    _dateController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(UserScreenTexts.addUser),
      content: Form(
        key: _formKey,
        child: SingleChildScrollView(
          child: Container(
            width: context.percent60Screen,
            height: context.percent60Screen,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Expanded(
                  flex: 5,
                  child: CustomEmailInput(
                    controller: _emailController,
                    context: context,
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomTextInput(
                    controller: _fullNameController,
                    labelText: UserScreenTexts.fullName,
                    hintText: UserScreenTexts.fullNameHint,
                    min: 3,
                    max: 50,
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomDropdownButton(
                    options: [UserScreenTexts.active, UserScreenTexts.inactive],
                    onChanged: (value) {
                      _statusController.text = value ?? UserScreenTexts.active;
                    },
                    getFirstValue: (value) {
                      _statusController.text = value ?? UserScreenTexts.active;
                    },
                    icon: Icons.list,
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomPhoneInput(
                    controller: _mobilePhonesController,
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomTextInput(
                    controller: _addressController,
                    labelText: CoreScreenTexts.address,
                    hintText: CoreScreenTexts.addressHint,
                    min: 10,
                    max: 100,
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomTextInput(
                    controller: _notesController,
                    labelText: CoreScreenTexts.notes,
                    hintText: CoreScreenTexts.notesHint,
                    min: 0,
                    max: 200,
                  ),
                ),
                const Spacer(),
                Expanded(
                  flex: 5,
                  child: CustomDateInput(
                    dateController: _dateController,
                    onDateChanged: (value) {
                      _dateController.text = value.toIso8601String();
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
          child: Text(CoreScreenTexts.cancel),
        ),
        ElevatedButton(
          onPressed: () {
            if (_formKey.currentState!.validate()) {
              final newUser = User(
                id: 0,
                userId: 0,
                email: _emailController.text,
                fullName: _fullNameController.text,
                status: _statusController.text == UserScreenTexts.active
                    ? true
                    : false,
                mobilePhones: _mobilePhonesController.text,
                address: _addressController.text,
                notes: _notesController.text,
              );
              Navigator.of(context).pop(newUser);
            }
          },
          child: Text(CoreScreenTexts.saveButton),
        ),
      ],
    );
  }
}
