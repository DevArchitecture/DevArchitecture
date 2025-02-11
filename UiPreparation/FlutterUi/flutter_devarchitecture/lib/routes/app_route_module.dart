import 'package:flutter_devarchitecture/core/features/admin_panel/log_f/log_page.dart';

import '../core/example_pages/theme/color_palette_page.dart';
import '../core/example_pages/utilities/logger_page.dart';
import '../core/example_pages/utilities/permission_page.dart';
import '../core/example_pages/utilities/qr_code_scanner.dart';
import '../core/example_pages/utilities/screen_message_page.dart';
import '../core/example_pages/utilities/share/csv_share_button_page.dart';
import '../core/example_pages/utilities/share/excel_share_button_page.dart';
import '../core/example_pages/utilities/share/image_share_button_page.dart';
import '../core/example_pages/utilities/share/json_share_button_page.dart';
import '../core/example_pages/utilities/share/pdf_share_button_page.dart';
import '../core/example_pages/utilities/share/txt_share_button_page.dart';
import '../core/example_pages/utilities/share/xml_share_button_page.dart';
import '../core/example_pages/utilities/internet_connection_page.dart';
import '../core/example_pages/utilities/local_notification_page.dart';
import '../core/example_pages/widgets/input_examples_page.dart';
import '../core/guard/claim_guard.dart';
import '../core/features/admin_panel/languages/pages/admin_language_page.dart';
import '../core/features/admin_panel/users/pages/user_page.dart';
import '../core/example_pages/utilities/download/csv_download_button_page.dart';
import '../core/example_pages/utilities/download/excel_download_button_page.dart';
import '../core/example_pages/utilities/download/image_download_button_page.dart';
import '../core/example_pages/utilities/download/json_download_button_page.dart';

import 'package:flutter_modular/flutter_modular.dart';

import '../core/guard/auth_guard.dart';
import '../core/features/admin_panel/groups/pages/admin_group_page.dart';
import '../core/features/admin_panel/operation_claims/pages/admin_operation_claim_page.dart';
import '../core/features/admin_panel/translates/pages/admin_translate_page.dart';
import '../features/app_panel/home/home_page.dart';
import '../core/features/public/auth/pages/login_page.dart';
import '../core/features/public/not_found/not_found_page.dart';
import '../core/example_pages/utilities/download/pdf_download_button_page.dart';
import '../core/example_pages/utilities/download/txt_download_button_page.dart';
import '../core/example_pages/utilities/download/xml_download_button_page.dart';
import '../core/example_pages/utilities/battery_status_page.dart';
import '../core/example_pages/utilities/biometric_auth_page.dart';
import '../core/example_pages/utilities/device_info_page.dart';
import 'routes_constants.dart';

class AppRouteModule extends Module {
  @override
  void binds(Injector i) {
    i.addSingleton<AuthStore>(AuthStore.new);
    super.binds(i);
  }

  @override
  void routes(r) {
    var transition = TransitionType.fadeIn;

    // ADMIN LAYOUT
    //*? ADMIN HOME PAGE
    r.child(RoutesConstants.adminHomePage,
        child: (context) => const HomePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    //*? ADMIN LOG PAGE
    r.child(RoutesConstants.adminLogPage,
        child: (context) => const AdminLogPage(),
        transition: transition,
        guards: [
          ModularAuthGuard(),
          ModularClaimGuard(claim: "GetLogDtoQuery")
        ]);

    //*? ADMIN USER PAGE
    r.child(RoutesConstants.adminUserPage,
        child: (context) => const AdminUserPage(),
        transition: transition,
        guards: [
          ModularAuthGuard(),
          ModularClaimGuard(claim: "GetUsersQuery")
        ]);

    //*? ADMIN GROUP PAGE
    r.child(RoutesConstants.adminGroupPage,
        child: (context) => const AdminGroupPage(),
        transition: transition,
        guards: [
          ModularAuthGuard(),
          ModularClaimGuard(claim: "GetGroupsQuery")
        ]);

    //*? ADMIN OPERATION CLAIM PAGE
    r.child(RoutesConstants.adminOperationClaimPage,
        child: (context) => const AdminOperationClaimPage(),
        transition: transition,
        guards: [
          ModularAuthGuard(),
          ModularClaimGuard(claim: "GetOperationClaimsQuery")
        ]);

    //*? ADMIN LANGUAGE PAGE
    r.child(RoutesConstants.adminLanguagePage,
        child: (context) => const AdminLanguagePage(),
        transition: transition,
        guards: [
          ModularAuthGuard(),
          ModularClaimGuard(claim: "GetLanguagesQuery")
        ]);

    //*? ADMIN TRANSLATE PAGE
    r.child(RoutesConstants.adminTranslatePage,
        child: (context) => const AdminTranslatePage(),
        transition: transition,
        guards: [
          ModularAuthGuard(),
          ModularClaimGuard(claim: "GetTranslatesQuery")
        ]);

    // APP LAYOUT
    //*? HOME PAGE
    r.child(RoutesConstants.appHomePage,
        child: (context) => const HomePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    // PUBLIC LAYOUT
    //*? LOGIN PAGE
    r.child(RoutesConstants.loginPage,
        child: (context) => LoginPage(), transition: transition);

    //*? WILDCARD
    r.wildcard(
      child: (context) => const NotFoundPage(),
      transition: transition,
    );

    //UTILITIES
    //*? Battery Status Page
    r.child(RoutesConstants.batteryStatusPage,
        child: (context) => BatteryStatusPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    //*? Biometric Auth Page
    r.child(RoutesConstants.biometricAuthPage,
        child: (context) => BiometricAuthPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    //*? Device Info Page
    r.child(RoutesConstants.deviceInfoPage,
        child: (context) => DeviceInfoPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    //*? DOWNLOAD PAGES
    r.child(RoutesConstants.excelDownloadPage,
        child: (context) => ExcelDownloadPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.csvDownloadPage,
        child: (context) => CsvDownloadPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.imageDownloadPage,
        child: (context) => ImageDownloadPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.jsonDownloadPage,
        child: (context) => JsonDownloadPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.pdfDownloadPage,
        child: (context) => PdfDownloadPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.txtDownloadPage,
        child: (context) => TxtDownloadPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.xmlDownloadPage,
        child: (context) => XmlDownloadPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    //*? SHARE PAGES
    r.child(RoutesConstants.xmlSharePage,
        child: (context) => XmlSharePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.pdfSharePage,
        child: (context) => PdfSharePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.excelSharePage,
        child: (context) => ExcelSharePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.csvSharePage,
        child: (context) => CsvSharePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.imageSharePage,
        child: (context) => ImageSharePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.txtSharePage,
        child: (context) => TxtSharePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.jsonSharePage,
        child: (context) => JsonSharePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.internetConnectionPage,
        child: (context) => InternetConnectionPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.localNotificationPage,
        child: (context) => LocalNotificationPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.screenMessagePage,
        child: (context) => ScreenMessagePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.loggerPage,
        child: (context) => LoggerPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.permissionPage,
        child: (context) => PermissionPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    r.child(RoutesConstants.qrCodePage,
        child: (context) => QRCodeScannerPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    //*? theme pages
    r.child(RoutesConstants.colorPalettePage,
        child: (context) => ColorPalettePage(),
        transition: transition,
        guards: [ModularAuthGuard()]);

    //*? widgets
    r.child(RoutesConstants.inputFieldPage,
        child: (context) => InputExamplesPage(),
        transition: transition,
        guards: [ModularAuthGuard()]);
  }
}
