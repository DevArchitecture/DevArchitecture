import '../../lookups/models/lookup.dart';

import '../../../../di/core_initializer.dart';
import '../../../../services/base_services/api_service.dart';
import '../../../../utilities/results.dart';
import '../models/user_claim.dart';
import 'i_user_claim_service.dart';

class ApiUserClaimService extends ApiService<UserClaim>
    implements IUserClaimService {
  ApiUserClaimService({required super.method});

  @override
  Future<IDataResult<List<LookUp>>> getUserClaimsByUserId(int userId) async {
    var result =
        await CoreInitializer().coreContainer.http.get("$url/users/$userId");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data = result["data"] as List<Map<String, dynamic>>;
    return Future.value(
        SuccessDataResult(data.map((e) => LookUp.fromMap(e)).toList(), ""));
  }
}
