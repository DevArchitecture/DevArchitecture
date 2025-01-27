import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../../../constants/core_screen_texts.dart';
import '/core/widgets/button_widgets.dart';
import '../../../../../core/bloc/base_state.dart';
import '../../../../../core/bloc/bloc_consumer_extension.dart';
import '../../../../../core/bloc/bloc_helper.dart';
import '../../../../../core/theme/extensions.dart';
import '../../../../../core/theme/custom_colors.dart';
import '../../../../../core/utilities/download_management/buttons/download_buttons.dart';
import '../../../../../core/widgets/base_widgets.dart';
import '../../../../../core/widgets/tables/filter_table_widget.dart';
import '../../../../../layouts/base_scaffold.dart';
import 'bloc/log_cubit.dart';
import 'log_messages.dart';
import 'log_screen_texts.dart';
import 'models/log_dto.dart';

class AdminLogPage extends StatelessWidget {
  const AdminLogPage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => LogCubit(),
      child: ExtendedBlocConsumer<LogCubit, BaseState>(
        builder: (context, state) {
          List<Map<String, dynamic>>? datas;

          if (state is BlocInitial) {
            BlocProvider.of<LogCubit>(context).getLogs();
          }

          var resultWidget = getResultWidgetByStateWithScaffold(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }

          if (state is BlocSuccess<List<LogDto>>) {
            datas = state.result!.map((e) => e.toMap()).toList();
          } else if (state is BlocFailed) {
            final previousState = BlocProvider.of<LogCubit>(context).state;
            if (previousState is BlocSuccess<List<LogDto>>) {
              datas = previousState.result!.map((e) => e.toMap()).toList();
            } else {
              datas = [];
            }
          }

          if (datas == null) {
            return const Center(child: CircularProgressIndicator());
          }

          return buildLogTable(context, datas);
        },
      ),
    );
  }

  Widget buildLogTable(BuildContext context, List<Map<String, dynamic>> datas) {
    return buildBaseScaffold(
      context,
      Column(
        children: [
          Expanded(
            child: Padding(
              padding: context.defaultHorizontalPadding,
              child: buildPageTitle(
                context,
                LogScreenTexts.logList,
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
                {"level": LogScreenTexts.level},
                {"exceptionMessage": LogScreenTexts.exceptionMessage},
                {"timeStamp": LogScreenTexts.timeStamp},
                {"user": LogScreenTexts.user},
                {"value": LogScreenTexts.value},
                {"type": LogScreenTexts.type},
              ],
              color: CustomColors.success.getColor,
              customManipulationButton: const [],
              customManipulationCallback: [],
              infoHover: getInfoHover(context, LogMessages.logInfoHover),
              utilityButton: DownloadButtons(data: datas).excelButton(context),
            ),
          ),
        ],
      ),
    );
  }
}
