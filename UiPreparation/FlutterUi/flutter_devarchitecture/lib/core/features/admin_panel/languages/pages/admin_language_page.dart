import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_devarchitecture/core/constants/core_screen_texts.dart';
import '../../../../bloc/base_state.dart';
import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../../../../theme/extensions.dart';
import '../../../../utilities/download_management/buttons/download_buttons.dart';
import '../../../../widgets/confirmation_dialog.dart';
import '../../../../extensions/claimed_widget.dart';
import '../bloc/language_cubit.dart';
import '../language_constants/language_messages.dart';
import '../language_constants/language_screen_texts.dart';
import '../models/language.dart';
import '../widgets/add_language_dialog.dart';
import '../widgets/update_language_dialog.dart';
import '../../../../../layouts/base_scaffold.dart';
import '../../../../theme/custom_colors.dart';
import '../../../../widgets/base_widgets.dart';
import '../../../../widgets/button_widgets.dart';
import '../../../../widgets/tables/filter_table_widget.dart';

class AdminLanguagePage extends StatelessWidget {
  const AdminLanguagePage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => LanguageCubit(),
      child: ExtendedBlocConsumer<LanguageCubit, BaseState>(
        builder: (context, state) {
          List<Map<String, dynamic>>? datas;

          if (state is BlocInitial) {
            BlocProvider.of<LanguageCubit>(context).getAll();
          }

          var resultWidget = getResultWidgetByStateWithScaffold(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }

          if (state is BlocSuccess<List<Map<String, dynamic>>>) {
            datas = state.result;
          } else if (state is BlocFailed) {
            final previousState = BlocProvider.of<LanguageCubit>(context).state;
            if (previousState is BlocSuccess<List<Map<String, dynamic>>>) {
              datas = previousState.result;
            } else {
              datas = [];
            }
          }

          if (datas == null) {
            return const Center(child: CircularProgressIndicator());
          }

          return buildLanguageTable(context, datas);
        },
      ),
    );
  }

  Widget buildLanguageTable(
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
                LanguageScreenTexts.languageList,
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
                {"code": LanguageScreenTexts.code},
                {"name": CoreScreenTexts.name},
              ],
              infoHover:
                  getInfoHover(context, LanguageMessages.languageInfoHover),
              utilityButton: DownloadButtons(
                      color: CustomColors.dark.getColor, data: datas)
                  .excelButton(context),
              color: CustomColors.danger.getColor,
              customManipulationButton: [
                updateLanguageButton,
                deleteLanguageButton,
              ],
              customManipulationCallback: [
                (id) {
                  var language = datas.firstWhere(
                    (element) => element['id'] == id,
                  );
                  _editLanguage(context, language);
                },
                (id) => _confirmDelete(context, id)
              ],
              addButton: ClaimedWidget(
                claimText: "CreateLanguageCommand",
                child: getAddButton(
                  context,
                  () => _addLanguage(context),
                  color: CustomColors.white.getColor,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget updateLanguageButton(BuildContext context, VoidCallback onPressed) =>
      ClaimedWidget(
          claimText: "UpdateLanguageCommand",
          child: getEditButton(context, onPressed));

  Widget deleteLanguageButton(BuildContext context, VoidCallback onPressed) =>
      ClaimedWidget(
          claimText: "DeleteLanguageCommand",
          child: getDeleteButton(context, onPressed));

  void _addLanguage(BuildContext context) async {
    final newLanguage = await showDialog<Language>(
      context: context,
      builder: (c) => const AddLanguageDialog(),
    );
    if (newLanguage != null) {
      BlocProvider.of<LanguageCubit>(context).add(newLanguage);
    }
  }

  void _editLanguage(
      BuildContext context, Map<String, dynamic> languageData) async {
    final updatedLanguage = await showDialog<Language>(
      context: context,
      builder: (c) =>
          UpdateLanguageDialog(language: Language.fromMap(languageData)),
    );
    if (updatedLanguage != null) {
      BlocProvider.of<LanguageCubit>(context).update(updatedLanguage);
    }
  }

  void _confirmDelete(BuildContext context, int languageId) {
    showConfirmationDialog(context,
        () => BlocProvider.of<LanguageCubit>(context).delete(languageId));
  }
}
