import 'package:flutter/widgets.dart';
import '/core/widgets/animations/i_animation_asset.dart';

abstract class IStatusAnimationAsset extends IAnimationAsset {
  Widget getSendingAnimationAsset(double width, double height);
  Widget getLoadingAnimationAsset(double width, double height);
  Widget getCheckingAnimationAsset(double width, double height);

  Widget getSuccessAnimationAsset(double width, double height);
  Widget getErrorAnimationAsset(double width, double height);
}
