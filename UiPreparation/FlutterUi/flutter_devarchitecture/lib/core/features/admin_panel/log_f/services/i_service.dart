import '/core/utilities/results.dart';

import '../../../../services/i_service.dart';
import '../models/log_dto.dart';

abstract class ILogService implements IService {
  Future<IDataResult<List<LogDto>>> getLogs();
}
