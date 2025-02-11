import 'package:flutter/material.dart';
import '../../../../constants/core_screen_texts.dart';
import '../group_constants/group_screen_texts.dart';
import '../models/group.dart';
import '../../../../widgets/inputs/text_input.dart';

class AddGroupDialog extends StatefulWidget {
  const AddGroupDialog({Key? key}) : super(key: key);

  @override
  _AddGroupDialogState createState() => _AddGroupDialogState();
}

class _AddGroupDialogState extends State<AddGroupDialog> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _groupNameController;

  @override
  void initState() {
    super.initState();
    _groupNameController = TextEditingController();
  }

  @override
  void dispose() {
    _groupNameController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(GroupScreenTexts.addGroup),
      content: Form(
        key: _formKey,
        child: CustomTextInput(
          controller: _groupNameController,
          labelText: GroupScreenTexts.groupName,
          hintText: GroupScreenTexts.groupNameHint,
          min: 3,
          max: 50,
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
              final newGroup = Group(
                id: 0,
                groupName: _groupNameController.text,
              );
              Navigator.of(context).pop(newGroup);
            }
          },
          child: Text(CoreScreenTexts.saveButton),
        ),
      ],
    );
  }
}
