import '../core/features/admin_panel/log_f/services/i_service.dart';
import '../core/features/admin_panel/group_claims/services/i_group_claim_service.dart';
import '../core/features/admin_panel/languages/services/i_language_service.dart';
import '../core/features/admin_panel/operation_claims/services/i_operation_claim_service.dart';
import '../core/features/admin_panel/translates/services/i_translate_service.dart';

import '../core/features/admin_panel/groups/services/i_group.dart';
import '../core/features/admin_panel/lookups/services/i_lookup_service.dart';
import '../core/features/admin_panel/user_groups/services/i_user_group_service.dart';
import '../core/features/admin_panel/user_claims/services/i_user_claim_service.dart';
import '../core/features/admin_panel/users/services/i_user_service.dart';

import '../core/features/public/auth/services/abstract/i_auth_service.dart';

abstract class IBusinessContainer {
  late IAuthService authService;

  late IUserService userService;

  late ILogService logService;

  late IUserClaimService userClaimService;

  late IUserGroupService userGroupService;

  late ITranslateService translateService;

  late ILanguageService languageService;

  late IOperationClaimService operationClaimService;

  late IGroupService groupService;

  late ILookupService lookupService;

  late IGroupClaimService groupClaimService;

  void setup();
  void checkIfUnRegistered<T extends Object>(Function register);
}
