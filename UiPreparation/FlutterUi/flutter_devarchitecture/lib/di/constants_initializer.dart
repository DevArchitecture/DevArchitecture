import 'package:flutter/material.dart';

import '../core/constants/core_messages.dart';
import '../core/constants/core_screen_texts.dart';
import '../core/constants/sidebar_constants.dart';
import '../core/features/admin_panel/group_claims/group_constants/group_claim_messages.dart';
import '../core/features/admin_panel/group_claims/group_constants/group_claim_screen_texts.dart';
import '../core/features/admin_panel/groups/group_constants/group_messages.dart';
import '../core/features/admin_panel/groups/group_constants/group_screen_texts.dart';
import '../core/features/admin_panel/languages/language_constants/language_messages.dart';
import '../core/features/admin_panel/languages/language_constants/language_screen_texts.dart';
import '../core/features/admin_panel/log_f/log_messages.dart';
import '../core/features/admin_panel/log_f/log_screen_texts.dart';
import '../core/features/admin_panel/operation_claims/operation_claims_constants/operation_claim_messages.dart';
import '../core/features/admin_panel/operation_claims/operation_claims_constants/operation_claims_screen_texts.dart';
import '../core/features/admin_panel/translates/translate_constants/translate_Screen_texts.dart';
import '../core/features/admin_panel/translates/translate_constants/translate_messages.dart';
import '../core/features/admin_panel/user_claims/user_claim_constants/user_claim_messages.dart';
import '../core/features/admin_panel/user_claims/user_claim_constants/user_claim_screen_texts.dart';
import '../core/features/admin_panel/user_groups/user_group_constants/user_group_messages.dart';
import '../core/features/admin_panel/user_groups/user_group_constants/user_group_screen_texts.dart';
import '../core/features/admin_panel/users/user_constants/user_messages.dart';
import '../core/features/admin_panel/users/user_constants/user_screen_texts.dart';
import '../core/features/public/constants/public_messages.dart';
import '../core/features/public/constants/public_screen_texts.dart';

class ConstantsInitializer {
  static final ConstantsInitializer _inst = ConstantsInitializer._internal();

  ConstantsInitializer._internal();

  factory ConstantsInitializer(BuildContext context) {
    _inst._init(context);
    return _inst;
  }

  void _init(BuildContext context) {
    // init core constants
    CoreMessages.init(context);
    CoreScreenTexts.init(context);
    SidebarConstants.init(context);

    // init public constants
    PublicMessages.init(context);
    PublicScreenTexts.init(context);

    // init admin panel constants
    GroupClaimMessages.init(context);
    GroupClaimScreenTexts.init(context);
    GroupMessages.init(context);
    GroupScreenTexts.init(context);
    LanguageMessages.init(context);
    LanguageScreenTexts.init(context);
    LogMessages.init(context);
    LogScreenTexts.init(context);
    OperationClaimMessages.init(context);
    OperationClaimScreenTexts.init(context);
    TranslateMessages.init(context);
    TranslateScreenTexts.init(context);
    UserClaimMessages.init(context);
    UserClaimScreenTexts.init(context);
    UserGroupMessages.init(context);
    UserGroupScreenTexts.init(context);
    UserMessages.init(context);
    UserScreenTexts.init(context);
  }
}
