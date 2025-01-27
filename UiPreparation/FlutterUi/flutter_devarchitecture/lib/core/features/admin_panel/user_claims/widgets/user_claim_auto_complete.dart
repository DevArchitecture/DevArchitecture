import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_devarchitecture/core/features/admin_panel/user_claims/user_claim_constants/user_claim_screen_texts.dart';
import '../../../../../../core/bloc/base_state.dart';
import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../../lookups/widgets/lookup_multi_select_auto_complete.dart';
import '../bloc/user_claim_cubit.dart';
import '../../lookups/models/lookup.dart';

class UserClaimAutocomplete extends StatefulWidget {
  final int userId;
  final Function(List<int>) onChanged;
  final bool isAllSelected;

  const UserClaimAutocomplete({
    Key? key,
    required this.userId,
    required this.onChanged,
    this.isAllSelected = false,
  }) : super(key: key);

  @override
  _UserClaimAutocompleteState createState() => _UserClaimAutocompleteState();
}

class _UserClaimAutocompleteState extends State<UserClaimAutocomplete> {
  final TextEditingController _controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => UserClaimCubit(),
      child: ExtendedBlocConsumer<UserClaimCubit, BaseState>(
        builder: (context, state) {
          if (state is BlocInitial) {
            BlocProvider.of<UserClaimCubit>(context)
                .getUserClaimsByUserId(widget.userId);
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
              isAllSelected: widget.isAllSelected,
              valueKey: "label",
              selectedIds: selectedIds,
              options: options,
              labelText: UserClaimScreenTexts.userClaims,
              hintText: UserClaimScreenTexts.selectUserClaims,
              onChanged: widget.onChanged,
              controller: _controller,
              focusNode: _focusNode,
            );
          }
          return const SizedBox.shrink();
        },
      ),
    );
  }
}
