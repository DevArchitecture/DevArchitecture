import 'dart:ui';
import 'package:flutter/material.dart';
import '../theme/custom_colors.dart';

Future<void> showConfirmationDialog(
    BuildContext context, Future<void> Function() onConfirm) async {
  await showDialog(
    context: context,
    barrierDismissible: false,
    builder: (BuildContext dialogContext) {
      return BackdropFilter(
        filter: ImageFilter.blur(sigmaX: 5, sigmaY: 5),
        child: AlertDialog(
          surfaceTintColor: CustomColors.danger.getColor.withOpacity(0.8),
          backgroundColor: CustomColors.white.getColor,
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.all(Radius.circular(20)),
          ),
          title: Text(
            "Dikkat!",
            style: TextStyle(
              color: CustomColors.danger.getColor,
              fontWeight: FontWeight.bold,
            ),
          ),
          content: const Text(
              "Bu işlem geri alınamaz. Devam etmek istiyor musunuz?"),
          actions: <Widget>[
            TextButton(
              onPressed: () {
                Navigator.of(dialogContext).pop(); // Dialog'u kapat.
              },
              child: const Text("Vazgeç"),
            ),
            ElevatedButton(
                onPressed: () {
                  Navigator.of(context).pop();
                  onConfirm();
                },
                child: const Text("Evet")),
          ],
        ),
      );
    },
  );
}
