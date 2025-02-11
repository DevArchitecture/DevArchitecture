import '../../constants/core_screen_texts.dart';
import 'base/split_input.dart';

/// Turkey Vehicle License Plate Number Format
/// "99 X 9999", "99 X 99999"
/// "99 XX 999", "99 XX 9999"
/// "99 XXX 99" or "99 XXX 999"
class CustomPlateInput extends SingleSplitInput {
  CustomPlateInput({
    super.key,
    required super.controllers,
    required super.onChange,
    required super.confirmFocusNodes,
    super.enabled = true,
    isSimple = false,
  }) : super(
            format: isSimple ? 's15' : 'd2 s3 d5',
            labelText: CoreScreenTexts.vehicleLicensePlate,
            hintTexts: isSimple ? ["XXXXXXXX"] : ["00", "XXX", "00000"]);
}
