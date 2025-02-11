import 'package:flutter/material.dart';
import '../../routes/routes_constants.dart';
import 'helper.dart';
import '../../core/constants/sidebar_constants.dart';

class NavBar extends StatefulWidget {
  const NavBar({super.key});

  @override
  State<NavBar> createState() => _NavBarState();
}

class _NavBarState extends State<NavBar> {
  final ValueNotifier<Widget?> adminPanelMenu = ValueNotifier<Widget?>(null);
  final ValueNotifier<Widget?> appPanelMenu = ValueNotifier<Widget?>(null);
  final ValueNotifier<Widget?> utilityPanelMenu = ValueNotifier<Widget?>(null);
  final ValueNotifier<Widget?> templatesMenu = ValueNotifier<Widget?>(null);
  final ValueNotifier<Widget?> widgetsMenu = ValueNotifier<Widget?>(null);

  bool _menuItemsLoaded = false; // Tekrar yüklemeyi önlemek için flag.

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();

    if (!_menuItemsLoaded) {
      _menuItemsLoaded = true;
      _loadMenuItems();
    }
  }

  Future<void> _loadMenuItems() async {
    adminPanelMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.apps,
      SidebarConstants.adminPanelPageTitle,
      [
        {
          "name": SidebarConstants.adminPanelHomePageTitle,
          "route": RoutesConstants.adminHomePage,
          "icon": Icons.home_work_rounded,
        },
        {
          "name": SidebarConstants.adminPanelPageUserTitle,
          "route": RoutesConstants.adminUserPage,
          "icon": Icons.person,
          "guard": 'GetUsersQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageGroupTitle,
          "route": RoutesConstants.adminGroupPage,
          "icon": Icons.groups,
          "guard": 'GetGroupsQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageOperationClaimTitle,
          "route": RoutesConstants.adminOperationClaimPage,
          "icon": Icons.security,
          "guard": 'GetOperationClaimsQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageLanguageTitle,
          "route": RoutesConstants.adminLanguagePage,
          "icon": Icons.language,
          "guard": 'GetLanguagesQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageTranslateTitle,
          "route": RoutesConstants.adminTranslatePage,
          "icon": Icons.translate,
          "guard": 'GetTranslatesQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageLogTitle,
          "route": RoutesConstants.adminLogPage,
          "icon": Icons.history,
          "guard": 'GetLogDtoQuery',
        },
      ],
    );

    appPanelMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.format_list_bulleted_sharp,
      SidebarConstants.appPanelPageTitle,
      [
        {
          "name": SidebarConstants.homePageTitle,
          "route": RoutesConstants.appHomePage,
          "icon": Icons.home,
        },
      ],
    );

    utilityPanelMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.architecture_outlined,
      SidebarConstants.utilitiesPageTitle,
      [
        {
          "name": SidebarConstants.batteryStatusPageTitle,
          "route": RoutesConstants.batteryStatusPage,
          "icon": Icons.battery_unknown
        },
        {
          "name": SidebarConstants.biometricAuthPageTitle,
          "route": RoutesConstants.biometricAuthPage,
          "icon": Icons.fingerprint
        },
        {
          "name": SidebarConstants.deviceInfoPageTitle,
          "route": RoutesConstants.deviceInfoPage,
          "icon": Icons.devices
        },
        {
          "name": SidebarConstants.internetConnectionPageTitle,
          "route": RoutesConstants.internetConnectionPage,
          "icon": Icons.network_check_outlined
        },
        {
          "name": SidebarConstants.localNotificationPageTitle,
          "route": RoutesConstants.localNotificationPage,
          "icon": Icons.notifications_active_outlined
        },
        {
          "name": SidebarConstants.screenMessagePageTitle,
          "route": RoutesConstants.screenMessagePage,
          "icon": Icons.message
        },
        {
          "name": SidebarConstants.loggerPageTitle,
          "route": RoutesConstants.loggerPage,
          "icon": Icons.monitor
        },
        {
          "name": SidebarConstants.permissionPageTitle,
          "route": RoutesConstants.permissionPage,
          "icon": Icons.security_outlined
        },
        {
          "name": SidebarConstants.qrCodeScannerPageTitle,
          "route": RoutesConstants.qrCodePage,
          "icon": Icons.qr_code
        },
        {
          "icon": Icons.share_outlined,
          "name": SidebarConstants.sharePageTitle,
          "subMenu": [
            {
              "name": SidebarConstants.excelSharePageTitle,
              "route": RoutesConstants.excelSharePage,
              "icon": Icons.table_chart
            },
            {
              "name": SidebarConstants.pdfSharePageTitle,
              "route": RoutesConstants.pdfSharePage,
              "icon": Icons.picture_as_pdf
            },
            {
              "name": SidebarConstants.imageSharePageTitle,
              "route": RoutesConstants.imageSharePage,
              "icon": Icons.image
            },
            {
              "name": SidebarConstants.csvSharePageTitle,
              "route": RoutesConstants.csvSharePage,
              "icon": Icons.table_chart
            },
            {
              "name": SidebarConstants.xmlSharePageTitle,
              "route": RoutesConstants.xmlSharePage,
              "icon": Icons.code
            },
            {
              "name": SidebarConstants.txtSharePageTitle,
              "route": RoutesConstants.txtSharePage,
              "icon": Icons.description
            },
            {
              "name": SidebarConstants.jsonSharePageTitle,
              "route": RoutesConstants.jsonSharePage,
              "icon": Icons.code
            },
          ]
        },
        {
          "icon": Icons.download_for_offline_outlined,
          "name": SidebarConstants.downloadPageTitle,
          "subMenu": [
            {
              "name": SidebarConstants.excelDownloadPageTitle,
              "route": RoutesConstants.excelDownloadPage,
              "icon": Icons.table_chart
            },
            {
              "name": SidebarConstants.pdfDownloadPageTitle,
              "route": RoutesConstants.pdfDownloadPage,
              "icon": Icons.picture_as_pdf
            },
            {
              "name": SidebarConstants.imageDownloadPageTitle,
              "route": RoutesConstants.imageDownloadPage,
              "icon": Icons.image
            },
            {
              "name": SidebarConstants.csvDownloadPageTitle,
              "route": RoutesConstants.csvDownloadPage,
              "icon": Icons.table_chart
            },
            {
              "name": SidebarConstants.xmlDownloadPageTitle,
              "route": RoutesConstants.xmlDownloadPage,
              "icon": Icons.code
            },
            {
              "name": SidebarConstants.txtDownloadPageTitle,
              "route": RoutesConstants.txtDownloadPage,
              "icon": Icons.description
            },
            {
              "name": SidebarConstants.jsonDownloadPageTitle,
              "route": RoutesConstants.jsonDownloadPage,
              "icon": Icons.code
            },
          ]
        }
      ],
    );

    templatesMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.pages_outlined,
      SidebarConstants.templatesPageTitle,
      [
        {
          "name": SidebarConstants.colorPalettePageTitle,
          "route": RoutesConstants.colorPalettePage,
          "icon": Icons.color_lens_outlined,
        },
      ],
    );

    widgetsMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.widgets,
      SidebarConstants.widgetsPageTitle,
      [
        {
          "name": SidebarConstants.inputExamplesPageTitle,
          "route": RoutesConstants.inputFieldPage,
          "icon": Icons.input_outlined,
        },
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        children: [
          buildLogoWidget(),
          buildNavElement(
            Icons.home_outlined,
            SidebarConstants.homePageTitle,
            RoutesConstants.appHomePage,
          ),
          // Admin Panel
          ValueListenableBuilder<Widget?>(
            valueListenable: adminPanelMenu,
            builder: (context, menu, _) {
              if (menu == null) {
                return const Center(child: CircularProgressIndicator());
              }
              return menu;
            },
          ),
          // App Panel
          ValueListenableBuilder<Widget?>(
            valueListenable: appPanelMenu,
            builder: (context, menu, _) {
              if (menu == null) {
                return const Center(child: CircularProgressIndicator());
              }
              return menu;
            },
          ),
          // Utility Panel
          ValueListenableBuilder<Widget?>(
            valueListenable: utilityPanelMenu,
            builder: (context, menu, _) {
              if (menu == null) {
                return const Center(child: CircularProgressIndicator());
              }
              return menu;
            },
          ),
          // Templates
          ValueListenableBuilder<Widget?>(
            valueListenable: templatesMenu,
            builder: (context, menu, _) {
              if (menu == null) {
                return const Center(child: CircularProgressIndicator());
              }
              return menu;
            },
          ),
          // Widgets
          ValueListenableBuilder<Widget?>(
            valueListenable: widgetsMenu,
            builder: (context, menu, _) {
              if (menu == null) {
                return const Center(child: CircularProgressIndicator());
              }
              return menu;
            },
          ),
        ],
      ),
    );
  }
}
