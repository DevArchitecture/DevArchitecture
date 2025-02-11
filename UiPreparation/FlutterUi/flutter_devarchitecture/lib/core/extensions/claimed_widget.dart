import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'claim_provider.dart';

class ClaimedWidget extends StatelessWidget {
  final Widget child;
  final String claimText;

  const ClaimedWidget({
    Key? key,
    required this.child,
    required this.claimText,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<bool>(
      future: context.read<ClaimProvider>().hasClaim(context, claimText),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Center(
            child: CircularProgressIndicator(),
          );
        } else if (snapshot.hasError || snapshot.data == false) {
          return const SizedBox.shrink();
        }
        return child;
      },
    );
  }
}
