import '../../../../services/base_services/api_service.dart';
import '../models/operation_claim.dart';
import 'i_operation_claim_service.dart';

class ApiOperationClaimService extends ApiService<OperationClaim>
    implements IOperationClaimService {
  ApiOperationClaimService({required super.method});
}
