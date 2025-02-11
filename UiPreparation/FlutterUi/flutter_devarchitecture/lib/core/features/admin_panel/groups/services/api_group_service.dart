import '../../../../services/base_services/api_service.dart';
import '../models/group.dart';
import 'i_group.dart';

class ApiGroupService extends ApiService<Group> implements IGroupService {
  ApiGroupService({required super.method});
}
