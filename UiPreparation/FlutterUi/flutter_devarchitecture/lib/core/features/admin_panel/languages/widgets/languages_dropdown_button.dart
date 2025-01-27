import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../../lookups/models/lookup.dart';

import '../../../../bloc/bloc_consumer_extension.dart';
import '../../../../bloc/bloc_helper.dart';
import '../bloc/language_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../widgets/inputs/dropdown_button.dart';

class LanguageDropdownButton extends StatefulWidget {
  final void Function(LookUp selectedLanguage) onChanged;
  final void Function(LookUp selectedLanguage) getInitialValue;
  final Key? key;

  const LanguageDropdownButton({
    this.key,
    required this.onChanged,
    required this.getInitialValue,
  }) : super(key: key);

  @override
  _LanguageDropdownButtonState createState() => _LanguageDropdownButtonState();
}

class _LanguageDropdownButtonState extends State<LanguageDropdownButton> {
  LookUp? _selectedLanguage;

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => LanguageCubit(),
      child: ExtendedBlocConsumer<LanguageCubit, BaseState>(
        builder: (context, state) {
          if (state is BlocInitial) {
            BlocProvider.of<LanguageCubit>(context).getLanguageLookups();
          }
          var resultWidget = getResultWidgetByState(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }
          if (state is BlocSuccess<List<LookUp>>) {
            List<LookUp> languages = state.result!;

            List<String> options = languages.map((lang) => lang.label).toList();

            return CustomDropdownButton(
              key: widget.key,
              icon: Icons.language,
              getFirstValue: (value) {
                _selectedLanguage = languages.firstWhere(
                    (lang) => lang.label == value,
                    orElse: () => languages.first);
                widget.getInitialValue(_selectedLanguage!);
              },
              options: options,
              onChanged: (String? selectedValue) {
                if (selectedValue != null) {
                  _selectedLanguage = languages
                      .firstWhere((lang) => lang.label == selectedValue);
                  widget.onChanged(_selectedLanguage!);
                }
              },
            );
          }
          return const SizedBox.shrink();
        },
      ),
    );
  }
}
