import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'claim_provider.dart';

extension ClaimProviderExtension on BuildContext {
  ClaimProvider get claimProvider =>
      Provider.of<ClaimProvider>(this, listen: true);
}
