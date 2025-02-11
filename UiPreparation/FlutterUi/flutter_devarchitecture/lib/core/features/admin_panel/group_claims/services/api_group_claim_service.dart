import '/core/utilities/results.dart';

import '../../lookups/models/lookup.dart';

import '../../../../di/core_initializer.dart';
import '../../../../services/base_services/api_service.dart';
import '../models/group_claim.dart';
import 'i_group_claim_service.dart';

class ApiGroupClaimService extends ApiService<GroupClaim>
    implements IGroupClaimService {
  ApiGroupClaimService({required super.method});

  @override
  Future<IDataResult<List<LookUp>>> getGroupClaimsByGroupId(int groupId) async {
    var result =
        await CoreInitializer().coreContainer.http.get("$url/groups/$groupId");
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
