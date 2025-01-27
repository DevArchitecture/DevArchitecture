import 'package:flutter/material.dart';
import '../../di/core_initializer.dart';
import '/core/theme/extensions.dart';
import '../../../layouts/base_scaffold.dart';
import '../../widgets/inputs/auto_complete.dart';
import '../../widgets/inputs/date_input.dart';
import '../../widgets/inputs/dropdown_button.dart';
import '../../widgets/inputs/email_input.dart';
import '../../widgets/inputs/number_input.dart';
import '../../widgets/inputs/password_input.dart';
import '../../widgets/inputs/phone_input.dart';
import '../../widgets/inputs/plate_input.dart';
import '../../widgets/inputs/text_input.dart';
import '../../widgets/inputs/vehicle_license_input.dart';

class InputExamplesPage extends StatefulWidget {
  @override
  _InputExamplesPageState createState() => _InputExamplesPageState();
}

class _InputExamplesPageState extends State<InputExamplesPage> {
  // Controllers for the various inputs
  final TextEditingController _autoCompleteController = TextEditingController();
  final TextEditingController _dateController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _numberController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _phoneController = TextEditingController();
  final TextEditingController _textController = TextEditingController();

  final FocusNode _autoCompleteFocusNode = FocusNode();
  final List<TextEditingController> _plateControllers = [
    TextEditingController(),
    TextEditingController(),
    TextEditingController()
  ];
  final List<TextEditingController> _vehicleLicenseControllers = [
    TextEditingController(),
    TextEditingController()
  ];

  final List<FocusNode> _plateFocusNodes = [
    FocusNode(),
    FocusNode(),
    FocusNode()
  ];
  final List<FocusNode> _vehicleLicenseFocusNodes = [FocusNode(), FocusNode()];

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Padding(
        padding: context.highestPadding,
        child: Column(
          children: [
            Expanded(
                flex: 3,
                child: Row(
                  children: [
                    Expanded(
                      flex: 5,
                      child: CustomAutoComplete(
                        options: [
                          {"id": 1, "name": "Option 1"},
                          {"id": 2, "name": "Option 2"},
                          {"id": 3, "name": "Option 3"}
                        ],
                        labelText: 'Otomatik Tamamlama',
                        hintText: 'Bir seçenek giriniz',
                        onChanged: (id) {
                          CoreInitializer()
                              .coreContainer
                              .logger
                              .logDebug('Selected option ID: $id');
                        },
                        controller: _autoCompleteController,
                        focusNode: _autoCompleteFocusNode,
                      ),
                    ),
                    const Spacer(),
                    // CustomDropdownButton Example
                    Expanded(
                      flex: 5,
                      child: CustomDropdownButton(
                        options: ['Seçenek 1', 'Seçenek 2', 'Seçenek 3'],
                        icon: Icons.arrow_drop_down,
                        onChanged: (value) {
                          CoreInitializer()
                              .coreContainer
                              .logger
                              .logDebug('Selected dropdown value: $value');
                        },
                        getFirstValue: (value) {},
                      ),
                    ),
                    const Spacer(),
                  ],
                )),
            Expanded(
                flex: 5,
                child: Row(
                  children: [
                    // CustomDateInput Example

                    Expanded(
                      flex: 5,
                      child: CustomDateInput(
                        dateController: _dateController,
                        onDateChanged: (value) {
                          CoreInitializer()
                              .coreContainer
                              .logger
                              .logDebug('Selected date: $value');
                        },
                      ),
                    ),
                    const Spacer(),

                    // CustomEmailInput Example
                    Expanded(
                      flex: 5,
                      child: CustomEmailInput(
                        context: context,
                        controller: _emailController,
                      ),
                    ),
                    const Spacer(),

                    // CustomNumberInput Example
                    Expanded(
                      flex: 5,
                      child: CustomNumberInput(
                        labelText: "Sayı Girişi",
                        hintText: "örn: 42",
                        min: 0,
                        max: 100,
                        controller: _numberController,
                      ),
                    ),
                    const Spacer(),
                  ],
                )),
            Expanded(
                flex: 5,
                child: Row(children: [
                  // CustomTextInput Example
                  Expanded(
                    flex: 5,
                    child: CustomTextInput(
                      labelText: "Metin Girişi",
                      hintText: "örn: Merhaba Dünya",
                      min: 5,
                      max: 100,
                      controller: _textController,
                    ),
                  ),
                  const Spacer(),
                  // CustomPasswordInput Example
                  Expanded(
                    flex: 5,
                    child: CustomPasswordInput(
                      context: context,
                      passwordController: _passwordController,
                    ),
                  ),
                  const Spacer(),
                  // CustomPhoneInput Example
                  Expanded(
                    flex: 5,
                    child: CustomPhoneInput(
                      controller: _phoneController,
                    ),
                  ),
                  const Spacer(),
                ])),
            // CustomPlateInput Example
            Expanded(
              flex: 2,
              child: CustomPlateInput(
                controllers: _plateControllers,
                onChange: (value) {
                  CoreInitializer()
                      .coreContainer
                      .logger
                      .logDebug('Plate input changed: $value');
                },
                confirmFocusNodes: _plateFocusNodes,
              ),
            ),
            const Spacer(),

            // CustomVehicleLicenseInput Example
            Expanded(
              flex: 2,
              child: CustomVehicleLicenseInput(
                controllers: _vehicleLicenseControllers,
                onChange: (value) {
                  CoreInitializer()
                      .coreContainer
                      .logger
                      .logDebug('Vehicle license input changed: $value');
                },
                confirmFocusNodes: _vehicleLicenseFocusNodes,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
