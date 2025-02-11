import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../../lookups/models/lookup.dart';
import 'package:provider/provider.dart';

import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../../../../extensions/translation_provider.dart';
import '../bloc/language_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../widgets/inputs/dropdown_button.dart';

class LanguageCodeDropdownButton extends StatefulWidget {
  final bool isShort;
  final Key? key;

  const LanguageCodeDropdownButton({
    this.key,
    this.isShort = false,
  }) : super(key: key);

  @override
  _LanguageCodeDropdownButtonState createState() =>
      _LanguageCodeDropdownButtonState();
}

class _LanguageCodeDropdownButtonState
    extends State<LanguageCodeDropdownButton> {
  LookUp? _selectedLanguage;

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => LanguageCubit(),
      child: ExtendedBlocConsumer<LanguageCubit, BaseState>(
        builder: (context, state) {
          if (state is BlocInitial) {
            BlocProvider.of<LanguageCubit>(context).getLanguageCodes();
          }
          var resultWidget = getResultWidgetByState(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }
          if (state is BlocSuccess<List<LookUp>>) {
            List<LookUp> languages = state.result!;

            List<String> options = widget.isShort
                ? languages.map((lang) => lang.id.toString()).toList()
                : languages.map((lang) => lang.label).toList();

            return CustomDropdownButton(
              key: widget.key,
              icon: Icons.language,
              getFirstValue: (value) {
                _selectedLanguage = widget.isShort
                    ? languages.firstWhere(
                        (lang) => lang.id.toString() == value,
                        orElse: () => languages.first)
                    : languages.firstWhere((lang) => lang.label == value,
                        orElse: () => languages.first);
              },
              options: options,
              onChanged: (String? selectedValue) async {
                if (selectedValue != null) {
                  setState(() {
                    _selectedLanguage = widget.isShort
                        ? languages.firstWhere(
                            (lang) => lang.id.toString() == selectedValue)
                        : languages
                            .firstWhere((lang) => lang.label == selectedValue);
                  });
                  await _updateLanguage(
                      context, _selectedLanguage!.id.toString());
                }
              },
            );
          }
          return const SizedBox.shrink();
        },
      ),
    );
  }

  Future<void> _updateLanguage(
      BuildContext context, String selectedLanguage) async {
    await Provider.of<TranslationProvider>(context, listen: false)
        .changeLocale(selectedLanguage);
  }
}
