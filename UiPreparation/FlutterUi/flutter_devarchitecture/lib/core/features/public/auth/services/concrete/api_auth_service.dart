import 'package:flutter/material.dart';

import '../../../../../../routes/routes_constants.dart';
import '../../models/password_dto.dart';

import '../../../../../utilities/results.dart';
import '../../../../../di/core_initializer.dart';
import '../../../../../services/base_services/api_service.dart';
import '../../models/auth.dart';
import '../abstract/i_auth_service.dart';

class ApiAuthService extends ApiService<AuthRequestBasic>
    implements IAuthService {
  ApiAuthService({required super.method});

  @override
  Future<IDataResult<AuthResponse>> login(
      String username, String password) async {
    var result = await CoreInitializer()
        .coreContainer
        .http
        .post("$url/login", {'email': username, 'password': password});
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    return SuccessDataResult(
        AuthResponse(
          token: result["data"]["token"],
          refreshToken: result["data"]["refreshToken"],
          claims: List<String>.from(result["data"]["claims"]),
          expiration: DateTime.parse(result["data"]["expiration"]),
        ),
        result["message"]);
  }

  @override
  Future<IResult> saveUserPassword(PasswordDto passwordDto) async {
    var result = await CoreInitializer()
        .coreContainer
        .http
        .put("$url/user-password", passwordDto.toMap());
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureResult(result["message"] ?? ""));
      }
    }
    return SuccessResult(result["message"]);
  }

  @override
  Future<bool> claimGuard(
      {required BuildContext context, required String claim}) async {
    String? claimsString =
        await CoreInitializer().coreContainer.storage.read("claims");

    if (claimsString == null || claimsString.isEmpty) {
      var result = await setClaims();
      if (!result.isSuccess) {
        Navigator.of(context).pushNamed(RoutesConstants.loginPage);
        return false;
      }
    }
    claimsString = await CoreInitializer().coreContainer.storage.read("claims");
    var _claims = claimsString!
        .substring(1, claimsString.length - 1)
        .split(', ')
        .map((e) => e.trim())
        .toList();
    return _claims.contains(claim);
  }

  @override
  Future<IResult> setClaims() async {
    var _token = await CoreInitializer().coreContainer.storage.read("token");

    if (_token == null || loggedIn() == false) {
      return FailureResult("Geçerli bir oturum bulunamadı.");
    }

    var result = await CoreInitializer()
        .coreContainer
        .http
        .get("$url/operation-claims/cache");

    if (result["success"] != null && result["success"] == false) {
      return FailureResult(result["message"] ?? "");
    }

    var _claims = List<String>.from(result["data"]["claims"]);
    await CoreInitializer()
        .coreContainer
        .storage
        .save("claims", _claims.toString());
    return SuccessResult("Claim'ler başarıyla güncellendi.");
  }

  @override
  bool loggedIn() {
    var _token =
        CoreInitializer().coreContainer.storage.read("token") as String?;
    return _token != null;
  }

  @override
  Future<int> getCurrentUserId() async {
    var userId = await CoreInitializer().coreContainer.storage.read("userId");
    return int.parse(userId!);
  }

  @override
  Future<String> getUsername() async {
    var username =
        await CoreInitializer().coreContainer.storage.read("userName");
    return username!;
  }

  @override
  Future<void> refreshToken() async {
    var _refreshToken =
        await CoreInitializer().coreContainer.storage.read("refreshToken");
    if (_refreshToken != null) {
      var result = await CoreInitializer()
          .coreContainer
          .http
          .post("$url/Auth/refresh-token", {"refreshToken": _refreshToken});

      if (result["success"] != null && result["success"] == false) {
        return;
      }
      await CoreInitializer()
          .coreContainer
          .storage
          .save("token", result["data"]["token"]);
      await CoreInitializer()
          .coreContainer
          .storage
          .save("refreshToken", result["data"]["refreshToken"]);
    }
  }
}
