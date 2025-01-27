import 'package:flutter/material.dart';
import '../../core/theme/extensions.dart';
import '../../core/services/claim_service.dart';
import 'package:flutter_modular/flutter_modular.dart';

import '../../core/theme/custom_colors.dart';

ListTile buildNavElement(IconData icon, String text, String route,
    {bool isClickable = true}) {
  return ListTile(
    contentPadding: const EdgeInsets.only(left: 36.0, right: 24.0),
    leading: GestureDetector(
        onTap: () => isClickable ? Modular.to.navigate(route) : () {},
        child: Icon(
          icon,
          size: 24,
        )),
    title: Text(text),
    onTap: () => Modular.to.navigate(route),
  );
}

Future<Widget> buildNavWithSubMenuItemElement(BuildContext context,
    IconData icon, String text, List<Map<String, dynamic>> options) async {
  final filteredOptions = await filterMenuOptions(context, options);

  return PopupMenuButton(
    color: CustomColors.white.getColor,
    surfaceTintColor: CustomColors.white.getColor,
    offset: !context.isMobile ? const Offset(200, 0) : const Offset(0, 0),
    itemBuilder: (context) {
      return List.generate(filteredOptions.length, (index) {
        if (filteredOptions[index].containsKey('subMenu') &&
            filteredOptions[index]['subMenu'] != null) {
          return PopupMenuItem(
            value: Text(filteredOptions[index]["name"]),
            child: FutureBuilder<Widget>(
              future: buildNavWithSubMenuItemElement(
                context,
                filteredOptions[index]["icon"],
                filteredOptions[index]["name"],
                List<Map<String, dynamic>>.from(
                    filteredOptions[index]['subMenu']),
              ),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return const CircularProgressIndicator();
                }
                if (snapshot.hasError) {
                  return const SizedBox.shrink();
                }
                return snapshot.data ?? const SizedBox.shrink();
              },
            ),
          );
        } else {
          return PopupMenuItem(
            value: Text(filteredOptions[index]["name"]),
            child: GestureDetector(
              onTap: () => Modular.to.navigate(filteredOptions[index]["route"]),
              child: ListTile(
                contentPadding: const EdgeInsets.only(left: 8.0, right: 8.0),
                leading: Icon(
                  filteredOptions[index]["icon"],
                  size: 24,
                ),
                title: Text(filteredOptions[index]["name"]),
              ),
            ),
          );
        }
      });
    },
    child: AbsorbPointer(
      child: ListTile(
        trailing: const Icon(
          Icons.chevron_right,
          size: 24,
        ),
        contentPadding: const EdgeInsets.only(left: 36.0, right: 24.0),
        leading: Icon(
          icon,
          size: 24,
        ),
        title: Text(text),
      ),
    ),
  );
}

Future<List<Map<String, dynamic>>> filterMenuOptions(
    BuildContext context, List<Map<String, dynamic>> options) async {
  final filteredOptions = <Map<String, dynamic>>[];
  final claimService = ClaimService();

  for (var option in options) {
    if (option.containsKey('guard') && option['guard'] != null) {
      final isClaimed = await claimService.hasClaim(option['guard']);
      if (isClaimed) {
        filteredOptions.add(option);
      }
    } else {
      filteredOptions.add(option);
    }
  }

  return filteredOptions;
}

DrawerHeader buildLogoWidget() {
  return DrawerHeader(
    padding:
        const EdgeInsets.only(top: 0.0, bottom: 0.0, right: 20.0, left: 20.0),
    child: Image.asset(
      'assets/images/logo.png',
    ),
  );
}
