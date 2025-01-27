import '../../lookups/models/lookup.dart';

import '../../../../services/i_service.dart';
import '../../../../utilities/results.dart';

abstract class IUserClaimService implements IService {
  Future<IDataResult<List<LookUp>>> getUserClaimsByUserId(int userId);
}
