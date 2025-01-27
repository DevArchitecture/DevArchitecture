import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../../../../bloc/base_state.dart';
import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../../../../constants/core_messages.dart';
import '../../../../constants/core_screen_texts.dart';
import '../../../../di/core_initializer.dart';
import '../../user_groups/bloc/user_group_cubit.dart';
import '../../user_groups/widgets/user_group_auto_complete.dart';
import '../user_constants/user_screen_texts.dart';

class ChangeUserGroupsDialog extends StatefulWidget {
  final int userId;

  const ChangeUserGroupsDialog({
    Key? key,
    required this.userId,
  }) : super(key: key);

  @override
  _ChangeUserGroupsDialogState createState() => _ChangeUserGroupsDialogState();
}

class _ChangeUserGroupsDialogState extends State<ChangeUserGroupsDialog> {
  List<int> _selectedGroups = [];

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => UserGroupCubit(),
      child: ExtendedBlocConsumer<UserGroupCubit, BaseState>(
        builder: (context, state) {
          var resultWidget = getResultWidgetByState(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }
          return AlertDialog(
            title: Text(UserScreenTexts.updateUserGroups),
            content: SizedBox(
              width: MediaQuery.of(context).size.width * 0.6,
              child: UserGroupAutocomplete(
                userId: widget.userId,
                onChanged: (selectedGroups) {
                  setState(() {
                    _selectedGroups = selectedGroups;
                  });
                },
              ),
            ),
            actions: [
              TextButton(
                onPressed: () => Navigator.of(context).pop(),
                child: Text(CoreScreenTexts.cancelButton),
              ),
              ElevatedButton(
                onPressed: () {
                  if (_selectedGroups.isNotEmpty) {
                    BlocProvider.of<UserGroupCubit>(context)
                        .saveUserGroupPermissions(
                            widget.userId, _selectedGroups);
                    Navigator.of(context).pop();
                  } else {
                    CoreInitializer()
                        .coreContainer
                        .screenMessage
                        .getErrorMessage(CoreMessages.atLeastOneSelection);
                  }
                },
                child: Text(CoreScreenTexts.saveButton),
              ),
            ],
          );
        },
      ),
    );
  }
}
