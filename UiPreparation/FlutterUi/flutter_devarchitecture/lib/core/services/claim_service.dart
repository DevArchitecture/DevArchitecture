import '/core/di/core_initializer.dart';

class ClaimService {
  Future<bool> hasClaim(String claim) async {
    final claimsString =
        await CoreInitializer().coreContainer.storage.read("claims");
    if (claimsString == null || claimsString.isEmpty) {
      return false;
    }

    final claims = claimsString
        .substring(1, claimsString.length - 1)
        .split(', ')
        .map((e) => e.trim())
        .toList();

    return claims.contains(claim);
  }
}
