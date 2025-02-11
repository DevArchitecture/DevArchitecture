import 'package:flutter/material.dart';
import 'package:lottie/lottie.dart';
import '../i_interaction_animation_asset.dart';

class LottieInteractionAnimationAsset implements IInteractionAnimationAsset {
  @override
  Widget getFeedbackAnimationAsset(double width, double height) {
    return Lottie.asset('assets/feedback.json', width: width, height: height);
  }
}
