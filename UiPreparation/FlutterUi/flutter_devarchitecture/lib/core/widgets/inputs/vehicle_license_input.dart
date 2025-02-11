import '../../constants/core_screen_texts.dart';
import 'base/split_input.dart';

/// Turkey Vehicle Registration Number Format
/// Serial - Number
/// "99 xxxxxx"
class CustomVehicleLicenseInput extends SingleSplitInput {
  CustomVehicleLicenseInput(
      {super.key,
      required super.controllers,
      required super.onChange,
      super.enabled = true,
      required super.confirmFocusNodes})
      : super(
            format: 's2 d6',
            labelText: CoreScreenTexts.vehicleRegistrationNumber,
            hintTexts: ["XX", "000000"]);
}
