import '../../../public/auth/models/password_dto.dart';
import '../../../../bloc/base_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../../di/business_initializer.dart';
import '../models/user.dart';

class UserCubit extends BaseCubit<User> {
  UserCubit() : super() {
    super.service = BusinessInitializer().businessContainer.userService;
  }

  Future<void> getAllUser() async {
    emit(BlocLoading());

    try {
      final usersResult =
          await BusinessInitializer().businessContainer.userService.getAll();

      if (!usersResult.isSuccess) {
        emitFailState(usersResult.message);
        return;
      }
      emit(BlocSuccess<List<User>>(
          usersResult.data!.map((e) => User.fromMap(e)).toList()));
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }

  Future<void> saveUserPassword(int userId, String password) async {
    emit(BlocLoading());
    try {
      var authService = BusinessInitializer().businessContainer.authService;
      var result = await authService
          .saveUserPassword(PasswordDto(password: password, userId: userId));
      if (!result.isSuccess) {
        emitFailState(result.message);
        return;
      }
      await getAllUser();
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }
}
