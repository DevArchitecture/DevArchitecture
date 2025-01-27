import 'package:flutter/material.dart';
import 'package:lottie/lottie.dart';
import '../i_page_animation_asset.dart';

class LottiePageAnimationAsset implements IPageAnimationAsset {
  @override
  Widget get404PageAnimationAsset(double width, double height) {
    return Lottie.asset('assets/404.json', width: width, height: height);
  }

  @override
  Widget get403PageAnimationAsset(double width, double height) {
    return Lottie.asset('assets/403.json', width: width, height: height);
  }
}
