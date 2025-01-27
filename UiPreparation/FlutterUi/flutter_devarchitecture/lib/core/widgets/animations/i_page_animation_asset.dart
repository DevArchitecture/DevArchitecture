import 'package:flutter/widgets.dart';
import '/core/widgets/animations/i_animation_asset.dart';

abstract class IPageAnimationAsset extends IAnimationAsset {
  Widget get404PageAnimationAsset(double width, double height);
  Widget get403PageAnimationAsset(double width, double height);
}
