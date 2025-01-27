import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_devarchitecture/core/constants/core_screen_texts.dart';
import '../../../../constants/core_messages.dart';
import '../group_constants/group_screen_texts.dart';
import '/core/di/core_initializer.dart';
import '../../user_groups/bloc/user_group_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../../user_groups/widgets/group_users_auto_complete.dart';

class UpdateGroupUsersDialog extends StatefulWidget {
  final int groupId;

  const UpdateGroupUsersDialog({Key? key, required this.groupId})
      : super(key: key);

  @override
  _UpdateGroupUsersDialogState createState() => _UpdateGroupUsersDialogState();
}

class _UpdateGroupUsersDialogState extends State<UpdateGroupUsersDialog> {
  List<int> _selectedUserIds = [];

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
            title: Text(GroupScreenTexts.updateGroupUsers),
            content: SizedBox(
              width: MediaQuery.of(context).size.width * 0.6,
              child: GroupUsersAutocomplete(
                groupId: widget.groupId,
                onChanged: (selectedUserIds) {
                  setState(() {
                    _selectedUserIds = selectedUserIds;
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
                  if (_selectedUserIds.isNotEmpty) {
                    BlocProvider.of<UserGroupCubit>(context)
                        .saveGroupUsers(widget.groupId, _selectedUserIds);
                    Navigator.of(context).pop();
                  } else {
                    CoreInitializer()
                        .coreContainer
                        .screenMessage
                        .getInfoMessage(CoreMessages.atLeastOneSelection);
                  }
                },
                child: Text(CoreScreenTexts.updateButton),
              ),
            ],
          );
        },
      ),
    );
  }
}
