import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../group_constants/group_claim_screen_texts.dart';
import '../../../../../../core/bloc/base_state.dart';
import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../bloc/group_claim_cubit.dart';
import '../../lookups/models/lookup.dart';
import '../../lookups/widgets/lookup_multi_select_auto_complete.dart';

class GroupClaimAutocomplete extends StatefulWidget {
  final int groupId;
  final Function(List<int>) onChanged;
  final bool isAllSelected;

  const GroupClaimAutocomplete({
    Key? key,
    required this.groupId,
    required this.onChanged,
    this.isAllSelected = false,
  }) : super(key: key);

  @override
  _GroupClaimAutocompleteState createState() => _GroupClaimAutocompleteState();
}

class _GroupClaimAutocompleteState extends State<GroupClaimAutocomplete> {
  final TextEditingController _controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => GroupClaimCubit(),
      child: ExtendedBlocConsumer<GroupClaimCubit, BaseState>(
        builder: (context, state) {
          if (state is BlocInitial) {
            BlocProvider.of<GroupClaimCubit>(context)
                .getGroupClaimsByGroupId(widget.groupId);
          }
          var resultWidget = getResultWidgetByState(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }
          if (state is BlocSuccess<List<LookUp>>) {
            final options = state.result!
                .map((claim) => {'id': claim.id, 'label': claim.label})
                .toList();
            final List<int> selectedIds = state.result!
                .where((e) => e.isSelected == true)
                .toList()
                .map((e) => int.parse(e.id))
                .toList();
            return LookupMultiSelectAutocomplete(
              options: options,
              valueKey: 'label',
              labelText: GroupClaimScreenTexts.groupClaims,
              hintText: GroupClaimScreenTexts.selectGroupClaim,
              selectedIds: selectedIds,
              isAllSelected: widget.isAllSelected,
              controller: _controller,
              focusNode: _focusNode,
              onChanged: (selectedIds) {
                setState(() {
                  widget.onChanged(selectedIds);
                });
              },
            );
          }
          return const SizedBox.shrink();
        },
      ),
    );
  }
}
