import '../models/log_dto.dart';

import '../../../../bloc/base_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../../di/business_initializer.dart';
import '../models/log.dart';

class LogCubit extends BaseCubit<Log> {
  LogCubit() : super() {
    super.service = BusinessInitializer().businessContainer.logService;
  }

  Future<void> getLogs() async {
    emit(BlocLoading());
    try {
      final logResult =
          await BusinessInitializer().businessContainer.logService.getLogs();

      if (!logResult.isSuccess) {
        emitFailState(logResult.message);
        return;
      }
      if (logResult.data == null) {
        emit(BlocSuccess<List<LogDto>>([]));
        return;
      }
      emit(BlocSuccess<List<LogDto>>(
        logResult.data!,
      ));
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }
}
