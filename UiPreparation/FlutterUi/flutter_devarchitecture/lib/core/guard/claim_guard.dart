import 'package:flutter_devarchitecture/di/business_initializer.dart';
import 'package:flutter_modular/flutter_modular.dart';
import '../../core/di/core_initializer.dart';
import '../../routes/routes_constants.dart';

class ModularClaimGuard extends RouteGuard {
  final String claim;

  ModularClaimGuard({required this.claim})
      : super(redirectTo: RoutesConstants.appHomePage);

  @override
  Future<bool> canActivate(String path, ModularRoute router) async {
    final claimStore = ClaimStore(claim: claim);
    return await claimStore.isClaim;
  }
}

class ClaimStore {
  final String claim;

  ClaimStore({required this.claim});

  Future<bool> get isClaim async {
    final storageService = CoreInitializer().coreContainer.storage;
    String? claimsString = await storageService.read("claims");

    if (claimsString == null || claimsString.isEmpty) {
      final authService = BusinessInitializer().businessContainer.authService;
      var result = await authService.setClaims();
      if (!result.isSuccess) {
        return false;
      }
      claimsString = await storageService.read("claims");
    }

    if (claimsString == null || claimsString.isEmpty) {
      return false;
    }
    final claims = _parseClaims(claimsString);
    return claims.contains(claim);
  }

  List<String> _parseClaims(String claimsString) {
    return claimsString
        .substring(1, claimsString.length - 1)
        .split(', ')
        .map((e) => e.trim())
        .toList();
  }
}
