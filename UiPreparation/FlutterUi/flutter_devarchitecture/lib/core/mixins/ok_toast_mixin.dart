import 'package:flutter/material.dart';
import 'package:oktoast/oktoast.dart';

mixin OKToastMixin<T extends StatefulWidget> on State<T> {
  @override
  Widget build(BuildContext context) {
    return OKToast(
      backgroundColor: Colors.grey,
      textPadding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      radius: 10,
      position: ToastPosition.bottom,
      duration: const Duration(seconds: 3),
      child: buildChild(context),
    );
  }

  @protected
  Widget buildChild(BuildContext context);
}
