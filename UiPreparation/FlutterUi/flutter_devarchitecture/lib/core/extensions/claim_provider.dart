import 'package:flutter/material.dart';
import '/core/di/core_initializer.dart';
import '/di/business_initializer.dart';

class ClaimProvider with ChangeNotifier {
  final storageService = CoreInitializer().coreContainer.storage;
  final authService = BusinessInitializer().businessContainer.authService;

  static List<String> _claims = [];
  List<String> get claims => _claims;

  Future<void> loadClaims(BuildContext context) async {
    String? claimsString = await storageService.read("claims");
    if (claimsString != null && claimsString.isNotEmpty) {
      _claims = _parseClaims(claimsString);
    } else {
      await refreshClaims(context);
    }
    notifyListeners();
  }

  Future<void> refreshClaims(BuildContext context) async {
    var result = await authService.setClaims();
    if (result.isSuccess) {
      String? updatedClaims = await storageService.read("claims");
      if (updatedClaims != null && updatedClaims.isNotEmpty) {
        _claims = _parseClaims(updatedClaims);
      }
    } else {
      Navigator.of(context).pushNamed('/login');
    }
    notifyListeners();
  }

  Future<bool> hasClaim(BuildContext context, String claim) async {
    if (_claims.isEmpty) {
      await loadClaims(context);
    }
    return _claims.contains(claim);
  }

  List<String> _parseClaims(String claimsString) {
    return claimsString
        .substring(1, claimsString.length - 1)
        .split(', ')
        .map((e) => e.trim())
        .toList();
  }
}
