import 'package:flutter/material.dart';

showDialogFunc(context, child) {
  return showDialog(
    context: context,
    builder: (context) {
      return Center(
        child: Material(
          type: MaterialType.transparency,
          child: Stack(
            alignment: Alignment.topRight,
            children: [
              child,
              InkWell(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: const Icon(
                    Icons.cancel,
                    color: Colors.white,
                    size: 50,
                  ))
            ],
          ),
        ),
      );
    },
  );
}
