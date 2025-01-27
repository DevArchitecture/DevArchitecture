import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:provider/provider.dart';

import '../core/constants/core_messages.dart';
import '../core/constants/core_screen_texts.dart';
import '../core/theme/extensions.dart';
import '../core/theme/custom_colors.dart';
import '../core/di/core_initializer.dart';
import '../core/theme/theme_provider.dart';
import '../routes/routes_constants.dart';
import 'sidebar/sidebar.dart';

buildBaseScaffold(BuildContext context, Widget body, {bool isDrawer = true}) {
  final themeProvider = Provider.of<ThemeProvider>(context);

  return Scaffold(
    appBar: AppBar(
      actions:
          ModalRoute.of(context)?.settings.name == RoutesConstants.loginPage
              ? [buildThemeButton(context, themeProvider)]
              : [
                  buildThemeButton(context, themeProvider),
                  // buildNotificationButton(context),
                  // buildProfileButton(context),
                  buildLogOutButton(context),
                ],
      leading: Builder(
        builder: (context) => isDrawer == false
            ? const SizedBox()
            : IconButton(
                onPressed: () => Scaffold.of(context).openDrawer(),
                icon: const Icon(Icons.menu)),
      ),
      title: context.isMobile
          ? const SizedBox()
          : Text("Devarchitecture",
              style: TextStyle(fontWeight: FontWeight.w800, fontSize: 26)),
      centerTitle: true,
    ),
    drawer: isDrawer == true ? const NavBar() : null,
    body: body,
  );
}

Widget buildThemeButton(BuildContext context, ThemeProvider themeProvider) {
  return Tooltip(
    message: CoreScreenTexts.changeThemeButton,
    child: IconButton(
      icon: Icon(
        themeProvider.themeMode == ThemeMode.dark
            ? Icons.dark_mode
            : Icons.light_mode,
      ),
      onPressed: () {
        themeProvider.toggleTheme();
      },
    ),
  );
}

Widget buildNotificationButton(BuildContext context) {
  late bool isHasNotification = true;
  return Tooltip(
    message: CoreScreenTexts.notificationsButton,
    child: Stack(
      children: [
        IconButton(
          onPressed: () {
            CoreInitializer()
                .coreContainer
                .screenMessage
                .getInfoMessage(CoreMessages.comingSoon);
          },
          icon: const Icon(Icons.notifications_active_outlined),
        ),
        Positioned(
            top: 0,
            right: 0,
            child: Icon(Icons.circle,
                color: isHasNotification
                    ? CustomColors.success.getColor
                    : CustomColors.danger.getColor,
                size: 10))
      ],
    ),
  );
}

Widget buildProfileButton(BuildContext context) {
  return Tooltip(
    message: CoreScreenTexts.profileButton,
    child: IconButton(
      onPressed: () {
        CoreInitializer()
            .coreContainer
            .screenMessage
            .getInfoMessage(CoreMessages.comingSoon);
        //TODO: Modular.to.navigate('/profile');
      },
      icon: const Icon(Icons.account_circle_outlined),
    ),
  );
}

Widget buildLogOutButton(BuildContext context) {
  return Tooltip(
    message: CoreScreenTexts.logOutButton,
    child: IconButton(
        onPressed: () {
          CoreInitializer().coreContainer.storage.delete("userId");
          CoreInitializer().coreContainer.storage.delete("userName");
          CoreInitializer().coreContainer.storage.delete("token");
          Modular.to.navigate(RoutesConstants.loginPage);
        },
        icon: const Icon(Icons.logout)),
  );
}
