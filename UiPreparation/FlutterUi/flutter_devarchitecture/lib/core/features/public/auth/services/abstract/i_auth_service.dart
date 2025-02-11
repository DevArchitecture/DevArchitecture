import 'package:flutter/material.dart';

import '../../../../../utilities/results.dart';
import '../../../../../services/i_service.dart';
import '../../models/auth.dart';
import '../../models/password_dto.dart';

abstract class IAuthService implements IService {
  Future<IDataResult<AuthResponse>> login(String email, String password);
  Future<IResult> saveUserPassword(PasswordDto passwordDto);
  Future<String> getUsername();
  Future<IResult> setClaims();
  Future<int> getCurrentUserId();
  bool loggedIn();
  Future<bool> claimGuard(
      {required BuildContext context, required String claim});
  Future<void> refreshToken();
}
