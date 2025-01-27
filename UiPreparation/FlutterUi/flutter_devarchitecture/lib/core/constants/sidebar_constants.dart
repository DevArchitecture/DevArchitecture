import 'package:flutter/material.dart';
import 'base_constants.dart';

class SidebarConstants extends SidebarConstantsBase {
  static late String homePageTitle;

  static late String adminPanelPageTitle;
  static late String adminPanelHomePageTitle;
  static late String adminPanelPageUserTitle;
  static late String adminPanelPageGroupTitle;
  static late String adminPanelPageOperationClaimTitle;
  static late String adminPanelPageLanguageTitle;
  static late String adminPanelPageTranslateTitle;
  static late String adminPanelPageLogTitle;

  static late String appPanelPageTitle;

  // Utilities
  static late String utilitiesPageTitle;
  static late String batteryStatusPageTitle;
  static late String biometricAuthPageTitle;
  static late String deviceInfoPageTitle;

  static late String downloadPageTitle;
  static late String excelDownloadPageTitle;
  static late String pdfDownloadPageTitle;
  static late String csvDownloadPageTitle;
  static late String jsonDownloadPageTitle;
  static late String xmlDownloadPageTitle;
  static late String imageDownloadPageTitle;
  static late String txtDownloadPageTitle;

  static late String sharePageTitle;
  static late String excelSharePageTitle;
  static late String pdfSharePageTitle;
  static late String csvSharePageTitle;
  static late String jsonSharePageTitle;
  static late String xmlSharePageTitle;
  static late String imageSharePageTitle;
  static late String txtSharePageTitle;

  static late String internetConnectionPageTitle;
  static late String localNotificationPageTitle;
  static late String screenMessagePageTitle;
  static late String loggerPageTitle;
  static late String permissionPageTitle;
  static late String qrCodeScannerPageTitle;

  // Templates
  static late String templatesPageTitle;
  static late String colorPalettePageTitle;

  // Widgets
  static late String widgetsPageTitle;
  static late String inputExamplesPageTitle;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    homePageTitle = BaseConstants.translate("HomePageTitle");

    adminPanelPageTitle = BaseConstants.translate("AdminPanelPageTitle");
    adminPanelHomePageTitle =
        BaseConstants.translate("AdminPanelHomePageTitle");
    adminPanelPageUserTitle =
        BaseConstants.translate("AdminPanelPageUserTitle");
    adminPanelPageGroupTitle =
        BaseConstants.translate("AdminPanelPageGroupTitle");
    adminPanelPageOperationClaimTitle =
        BaseConstants.translate("AdminPanelPageOperationClaimTitle");
    adminPanelPageLanguageTitle =
        BaseConstants.translate("AdminPanelPageLanguageTitle");
    adminPanelPageTranslateTitle =
        BaseConstants.translate("AdminPanelPageTranslateTitle");
    adminPanelPageLogTitle = BaseConstants.translate("AdminPanelPageLogTitle");

    appPanelPageTitle = BaseConstants.translate("AppPanelPageTitle");

    utilitiesPageTitle = BaseConstants.translate("UtilitiesPageTitle");
    batteryStatusPageTitle = BaseConstants.translate("BatteryStatusPageTitle");
    biometricAuthPageTitle = BaseConstants.translate("BiometricAuthPageTitle");
    deviceInfoPageTitle = BaseConstants.translate("DeviceInfoPageTitle");

    downloadPageTitle = BaseConstants.translate("DownloadPageTitle");
    excelDownloadPageTitle = BaseConstants.translate("ExcelDownloadPageTitle");
    pdfDownloadPageTitle = BaseConstants.translate("PdfDownloadPageTitle");
    csvDownloadPageTitle = BaseConstants.translate("CsvDownloadPageTitle");
    jsonDownloadPageTitle = BaseConstants.translate("JsonDownloadPageTitle");
    xmlDownloadPageTitle = BaseConstants.translate("XmlDownloadPageTitle");
    imageDownloadPageTitle = BaseConstants.translate("ImageDownloadPageTitle");
    txtDownloadPageTitle = BaseConstants.translate("TxtDownloadPageTitle");

    sharePageTitle = BaseConstants.translate("SharePageTitle");
    excelSharePageTitle = BaseConstants.translate("ExcelSharePageTitle");
    pdfSharePageTitle = BaseConstants.translate("PdfSharePageTitle");
    csvSharePageTitle = BaseConstants.translate("CsvSharePageTitle");
    jsonSharePageTitle = BaseConstants.translate("JsonSharePageTitle");
    xmlSharePageTitle = BaseConstants.translate("XmlSharePageTitle");
    imageSharePageTitle = BaseConstants.translate("ImageSharePageTitle");
    txtSharePageTitle = BaseConstants.translate("TxtSharePageTitle");

    internetConnectionPageTitle =
        BaseConstants.translate("InternetConnectionPageTitle");
    localNotificationPageTitle =
        BaseConstants.translate("LocalNotificationPageTitle");
    screenMessagePageTitle = BaseConstants.translate("ScreenMessagePageTitle");
    loggerPageTitle = BaseConstants.translate("LoggerPageTitle");
    permissionPageTitle = BaseConstants.translate("PermissionPageTitle");
    qrCodeScannerPageTitle = BaseConstants.translate("QrCodeScannerPageTitle");

    templatesPageTitle = BaseConstants.translate("TemplatesPageTitle");
    colorPalettePageTitle = BaseConstants.translate("ColorPalettePageTitle");

    widgetsPageTitle = BaseConstants.translate("WidgetsPageTitle");
    inputExamplesPageTitle = BaseConstants.translate("InputExamplesPageTitle");
  }
}
