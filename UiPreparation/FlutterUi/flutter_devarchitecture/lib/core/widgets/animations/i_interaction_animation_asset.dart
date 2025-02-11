import 'package:flutter/widgets.dart';
import '/core/widgets/animations/i_animation_asset.dart';

abstract class IInteractionAnimationAsset extends IAnimationAsset {
  Widget getFeedbackAnimationAsset(double width, double height);
}
