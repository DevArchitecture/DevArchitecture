import '/core/configs/app_config.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

import '../../../../bloc/base_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../di/core_initializer.dart';
import '../../../../../di/business_initializer.dart';
import '../models/auth.dart';

class AuthCubit extends BaseCubit<AuthRequestBasic> {
  AuthCubit() : super() {
    super.service = BusinessInitializer().businessContainer.authService;
  }

  Future<void> login(AuthRequestBasic body) async {
    try {
      emit(BlocSending());
      var result = await BusinessInitializer()
          .businessContainer
          .authService
          .login(body.email, body.password);
      if (!result.isSuccess) {
        emitFailState(result.message);
        return;
      }
      if (appConfig.name == "dev") {
        emit(const BlocSuccess('Hoşgeldiniz. Admin'));
        return;
      }

      Map<String, dynamic> decodedToken = JwtDecoder.decode(result.data!.token);
      await CoreInitializer().coreContainer.storage.save("lang", body.lang);

      await CoreInitializer().coreContainer.storage.save(
          "userId",
          decodedToken[
                  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
              .toString());
      await CoreInitializer().coreContainer.storage.save(
          "userName",
          decodedToken[
                  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
              .toString());
      await CoreInitializer()
          .coreContainer
          .storage
          .save("token", result.data!.token.toString());
      await CoreInitializer()
          .coreContainer
          .storage
          .save("refreshToken", result.data!.refreshToken.toString());
      await CoreInitializer()
          .coreContainer
          .storage
          .save("token_expiration", result.data!.expiration.toString());
      await CoreInitializer()
          .coreContainer
          .storage
          .save("claims", result.data!.claims.toString());
      emit(BlocSuccess(
          '${decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"].toString()}'));
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }
}
