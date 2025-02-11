import '../../core/features/admin_panel/log_f/services/api_log_service.dart';
import '../../core/features/admin_panel/log_f/services/i_service.dart';
import '../../core/features/admin_panel/group_claims/services/i_group_claim_service.dart';
import '../../core/features/admin_panel/lookups/services/api_lookup_service.dart';
import 'package:get_it/get_it.dart';
import '../../core/features/admin_panel/group_claims/services/api_group_claim_service.dart';
import '../../core/features/admin_panel/groups/services/i_group.dart';
import '../../../core/configs/app_config.dart';
import '../../core/features/admin_panel/groups/services/api_group_service.dart';
import '../../core/features/admin_panel/languages/services/api_language_service.dart';
import '../../core/features/admin_panel/languages/services/i_language_service.dart';
import '../../core/features/admin_panel/lookups/services/i_lookup_service.dart';
import '../../core/features/admin_panel/operation_claims/services/api_operation_claim_service.dart';
import '../../core/features/admin_panel/operation_claims/services/i_operation_claim_service.dart';
import '../../core/features/admin_panel/translates/services/api_translate_service.dart';
import '../../core/features/admin_panel/translates/services/i_translate_service.dart';
import '../../core/features/admin_panel/user_claims/services/i_user_claim_service.dart';
import '../../core/features/admin_panel/user_groups/services/api_user_group_service.dart';
import '../../core/features/admin_panel/user_groups/services/i_user_group_service.dart';
import '../../core/features/admin_panel/user_claims/services/api_user_claim.service.dart';
import '../../core/features/admin_panel/users/services/api_user_service.dart';
import '../../core/features/admin_panel/users/services/i_user_service.dart';
import '../../core/features/public/auth/services/abstract/i_auth_service.dart';
import '../../core/features/public/auth/services/concrete/api_auth_service.dart';
import '../i_business_container.dart';

class GetItBusinessContainer implements IBusinessContainer {
  late GetIt _getIt;
  void _init() {
    _getIt = GetIt.instance;
    setup();
  }

  GetItBusinessContainer() {
    _init();
  }

  @override
  late IAuthService authService;

  @override
  late IUserService userService;

  @override
  late IUserClaimService userClaimService;

  @override
  late IUserGroupService userGroupService;

  @override
  late ITranslateService translateService;

  @override
  late ILanguageService languageService;

  @override
  late IOperationClaimService operationClaimService;

  @override
  late IGroupService groupService;

  @override
  late ILookupService lookupService;

  @override
  late IGroupClaimService groupClaimService;

  @override
  late ILogService logService;

  @override
  void setup() {
    //*? Services Binding For DEVELOPMENT
    if (appConfig.name == 'dev' || appConfig.name == '') {
      checkIfUnRegistered<IAuthService>((() {
        authService = _getIt
            .registerSingleton<IAuthService>(ApiAuthService(method: "/Auth"));
      }));

      checkIfUnRegistered<IUserService>((() {
        userService = _getIt
            .registerSingleton<IUserService>(ApiUserService(method: "/Users"));
      }));

      checkIfUnRegistered<IUserClaimService>((() {
        userClaimService = _getIt.registerSingleton<IUserClaimService>(
            ApiUserClaimService(method: "/user-claims"));
      }));

      checkIfUnRegistered<IUserGroupService>((() {
        userGroupService = _getIt.registerSingleton<IUserGroupService>(
            ApiUserGroupService(method: "/user-groups"));
      }));

      checkIfUnRegistered<ITranslateService>((() {
        translateService = _getIt.registerSingleton<ITranslateService>(
            ApiTranslateService(method: "/Translates"));
      }));

      checkIfUnRegistered<ILanguageService>((() {
        languageService = _getIt.registerSingleton<ILanguageService>(
            ApiLanguageService(method: "/Languages"));
      }));

      checkIfUnRegistered<IOperationClaimService>((() {
        operationClaimService =
            _getIt.registerSingleton<IOperationClaimService>(
                ApiOperationClaimService(method: "/operation-claims"));
      }));

      checkIfUnRegistered<IGroupService>((() {
        groupService = _getIt.registerSingleton<IGroupService>(
            ApiGroupService(method: "/Groups"));
      }));

      checkIfUnRegistered<ILookupService>((() {
        lookupService =
            _getIt.registerSingleton<ILookupService>(ApiLookupService());
      }));

      checkIfUnRegistered<IGroupClaimService>((() {
        groupClaimService = _getIt.registerSingleton<IGroupClaimService>(
            ApiGroupClaimService(method: "/group-claims"));
      }));

      checkIfUnRegistered<ILogService>((() {
        logService = _getIt
            .registerSingleton<ILogService>(ApiLogService(method: "/Logs"));
      }));
    }

    //*? Services Binding For STAGING
    if (appConfig.name == 'staging') {
      checkIfUnRegistered<IAuthService>((() {
        authService = _getIt
            .registerSingleton<IAuthService>(ApiAuthService(method: "/Auth"));
      }));

      checkIfUnRegistered<IUserService>((() {
        userService = _getIt
            .registerSingleton<IUserService>(ApiUserService(method: "/Users"));
      }));

      checkIfUnRegistered<IUserClaimService>((() {
        userClaimService = _getIt.registerSingleton<IUserClaimService>(
            ApiUserClaimService(method: "/user-claims"));
      }));

      checkIfUnRegistered<IUserGroupService>((() {
        userGroupService = _getIt.registerSingleton<IUserGroupService>(
            ApiUserGroupService(method: "/user-groups"));
      }));

      checkIfUnRegistered<ITranslateService>((() {
        translateService = _getIt.registerSingleton<ITranslateService>(
            ApiTranslateService(method: "/Translates"));
      }));

      checkIfUnRegistered<ILanguageService>((() {
        languageService = _getIt.registerSingleton<ILanguageService>(
            ApiLanguageService(method: "/Languages"));
      }));

      checkIfUnRegistered<IOperationClaimService>((() {
        operationClaimService =
            _getIt.registerSingleton<IOperationClaimService>(
                ApiOperationClaimService(method: "/operation-claims"));
      }));

      checkIfUnRegistered<IGroupService>((() {
        groupService = _getIt.registerSingleton<IGroupService>(
            ApiGroupService(method: "/Groups"));
      }));

      checkIfUnRegistered<ILookupService>((() {
        lookupService =
            _getIt.registerSingleton<ILookupService>(ApiLookupService());
      }));

      checkIfUnRegistered<IGroupClaimService>((() {
        groupClaimService = _getIt.registerSingleton<IGroupClaimService>(
            ApiGroupClaimService(method: "/group-claims"));
      }));

      checkIfUnRegistered<ILogService>((() {
        logService = _getIt
            .registerSingleton<ILogService>(ApiLogService(method: "/Logs"));
      }));
    }

    //*? Services Binding For PRODUCTION
    if (appConfig.name == 'prod') {
      checkIfUnRegistered<IAuthService>((() {
        authService = _getIt
            .registerSingleton<IAuthService>(ApiAuthService(method: "/Auth"));
      }));

      checkIfUnRegistered<IUserService>((() {
        userService = _getIt
            .registerSingleton<IUserService>(ApiUserService(method: "/Users"));
      }));

      checkIfUnRegistered<IUserClaimService>((() {
        userClaimService = _getIt.registerSingleton<IUserClaimService>(
            ApiUserClaimService(method: "/user-claims"));
      }));

      checkIfUnRegistered<IUserGroupService>((() {
        userGroupService = _getIt.registerSingleton<IUserGroupService>(
            ApiUserGroupService(method: "/user-groups"));
      }));

      checkIfUnRegistered<ITranslateService>((() {
        translateService = _getIt.registerSingleton<ITranslateService>(
            ApiTranslateService(method: "/Translates"));
      }));

      checkIfUnRegistered<ILanguageService>((() {
        languageService = _getIt.registerSingleton<ILanguageService>(
            ApiLanguageService(method: "/Languages"));
      }));

      checkIfUnRegistered<IOperationClaimService>((() {
        operationClaimService =
            _getIt.registerSingleton<IOperationClaimService>(
                ApiOperationClaimService(method: "/operation-claims"));
      }));

      checkIfUnRegistered<IGroupService>((() {
        groupService = _getIt.registerSingleton<IGroupService>(
            ApiGroupService(method: "/Groups"));
      }));

      checkIfUnRegistered<ILookupService>((() {
        lookupService =
            _getIt.registerSingleton<ILookupService>(ApiLookupService());
      }));

      checkIfUnRegistered<IGroupClaimService>((() {
        groupClaimService = _getIt.registerSingleton<IGroupClaimService>(
            ApiGroupClaimService(method: "/group-claims"));
      }));

      checkIfUnRegistered<ILogService>((() {
        logService = _getIt
            .registerSingleton<ILogService>(ApiLogService(method: "/Logs"));
      }));
    }
  }

  @override
  void checkIfUnRegistered<T extends Object>(Function register) {
    if (!_getIt.isRegistered<T>()) {
      register.call();
    }
  }
}
