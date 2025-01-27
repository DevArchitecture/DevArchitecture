import '../../../../di/core_initializer.dart';
import '../../../../services/base_services/api_service.dart';
import '../../../../utilities/results.dart';
import '../models/log.dart';
import '../models/log_dto.dart';
import 'i_service.dart';

class ApiLogService extends ApiService<Log> implements ILogService {
  ApiLogService({required super.method});

  @override
  Future<IDataResult<List<LogDto>>> getLogs() async {
    var result = await CoreInitializer().coreContainer.http.get("$url");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data = result["data"] as List<Map<String, dynamic>>;
    return Future.value(
        SuccessDataResult(data.map((e) => LogDto.fromMap(e)).toList(), ""));
  }
}
