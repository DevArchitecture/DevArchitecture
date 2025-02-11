import '../../../../services/base_services/api_service.dart';
import '../models/user.dart';
import 'i_user_service.dart';

class ApiUserService extends ApiService<User> implements IUserService {
  ApiUserService({required super.method});
}
