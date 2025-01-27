import 'package:flutter/material.dart';

import '../../../theme/custom_colors.dart';
import '../../../utilities/file_share/Buttons/share_buttons.dart';
import '../../../widgets/base_widgets.dart';
import '../../../widgets/tables/filter_table_widget.dart';
import '../../../../layouts/base_scaffold.dart';
import '../data/users.dart';

class PdfSharePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final shareButtons =
        ShareButtons(data: users.map((e) => e.toMap()).toList());

    return buildBaseScaffold(
      context,
      Column(
        children: [
          Expanded(
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child:
                  buildPageTitle(context, "PDF Share Button", subDirection: ""),
            ),
          ),
          Expanded(
            flex: 9,
            child: FilterTableWidget(
              datas: users.map((e) => e.toMap()).toList(),
              headers: const [
                {"id": "id"},
                {"name": "name"},
                {"email": "email"},
                {"userType": "userType"},
              ],
              color: CustomColors.white.getColor,
              customManipulationButton: const [],
              customManipulationCallback: [],
              utilityButton: shareButtons.pdfButton(context),
            ),
          ),
          const Spacer(flex: 5),
        ],
      ),
      isDrawer: true,
    );
  }
}
