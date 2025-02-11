import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_devarchitecture/core/constants/core_screen_texts.dart';
import '../../../../extensions/claimed_widget.dart';
import '../operation_claims_constants/operation_claim_messages.dart';
import '../operation_claims_constants/operation_claims_screen_texts.dart';
import '/core/theme/extensions.dart';

import '../../../../bloc/base_state.dart';
import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../../../../theme/custom_colors.dart';
import '../../../../utilities/download_management/buttons/download_buttons.dart';
import '../../../../widgets/base_widgets.dart';
import '../../../../widgets/button_widgets.dart';
import '../../../../widgets/tables/filter_table_widget.dart';
import '../../../../../layouts/base_scaffold.dart';
import '../bloc/operation_claim_cubit.dart';
import '../models/operation_claim.dart';
import '../widgets/update_operation_claim_dialog.dart';

class AdminOperationClaimPage extends StatelessWidget {
  const AdminOperationClaimPage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => OperationClaimCubit(),
      child: ExtendedBlocConsumer<OperationClaimCubit, BaseState>(
        builder: (context, state) {
          List<Map<String, dynamic>>? datas;

          if (state is BlocInitial) {
            BlocProvider.of<OperationClaimCubit>(context).getAll();
          }

          var resultWidget = getResultWidgetByStateWithScaffold(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }

          if (state is BlocSuccess<List<Map<String, dynamic>>>) {
            datas = state.result;
          } else if (state is BlocFailed) {
            final previousState =
                BlocProvider.of<OperationClaimCubit>(context).state;
            if (previousState is BlocSuccess<List<Map<String, dynamic>>>) {
              datas = previousState.result;
            } else {
              datas = [];
            }
          }

          if (datas == null) {
            return const Center(child: CircularProgressIndicator());
          }

          return buildOperationClaimTable(context, datas);
        },
      ),
    );
  }

  Widget buildOperationClaimTable(
      BuildContext context, List<Map<String, dynamic>> datas) {
    return buildBaseScaffold(
      context,
      Column(
        children: [
          Expanded(
            child: Padding(
              padding: context.defaultHorizontalPadding,
              child: buildPageTitle(
                context,
                OperationClaimScreenTexts.operationClaimList,
                subDirection: CoreScreenTexts.adminPanel,
              ),
            ),
          ),
          Expanded(
            flex: 9,
            child: FilterTableWidget(
              datas: datas,
              headers: [
                {"id": "ID"},
                {"name": CoreScreenTexts.name},
                {"alias": CoreScreenTexts.alias},
                {"description": CoreScreenTexts.description},
              ],
              color: CustomColors.warning.getColor,
              customManipulationButton: [
                updateOperationClaimButton,
              ],
              customManipulationCallback: [
                (index) {
                  var operationClaim = datas.firstWhere(
                    (element) => element['id'] == index,
                  );
                  _editOperationClaim(context, operationClaim);
                },
              ],
              infoHover: getInfoHover(
                color: CustomColors.dark.getColor,
                context,
                OperationClaimMessages.operationClaimInfoHover,
              ),
              utilityButton: DownloadButtons(
                      color: CustomColors.dark.getColor, data: datas)
                  .excelButton(context),
            ),
          ),
        ],
      ),
    );
  }

  Widget updateOperationClaimButton(
          BuildContext context, VoidCallback onPressed) =>
      ClaimedWidget(
          claimText: "UpdateOperationClaimCommand",
          child: getEditButton(context, onPressed));

  void _editOperationClaim(
      BuildContext context, Map<String, dynamic> operationClaimData) async {
    final updatedOperationClaim = await showDialog(
      context: context,
      builder: (c) => UpdateOperationClaimDialog(
        operationClaim: OperationClaim.fromMap(operationClaimData),
      ),
    );
    if (updatedOperationClaim != null) {
      BlocProvider.of<OperationClaimCubit>(context)
          .updateOperationClaim(updatedOperationClaim);
    }
  }
}
