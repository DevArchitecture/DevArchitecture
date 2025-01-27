import '../../../../services/i_service.dart';
import '../../../../utilities/results.dart';
import '../../lookups/models/lookup.dart';

abstract class IGroupClaimService extends IService {
  Future<IDataResult<List<LookUp>>> getGroupClaimsByGroupId(int groupId);
}
