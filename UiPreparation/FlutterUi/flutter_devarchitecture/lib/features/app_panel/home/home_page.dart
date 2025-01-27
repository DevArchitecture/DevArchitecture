import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../../core/constants/core_screen_texts.dart';
import '../../../core/constants/sidebar_constants.dart';
import '../../../core/theme/responsive.dart';
import 'dart:async';
import 'dart:math';
import '../../../core/extensions/claim_provider.dart';
import '/core/theme/extensions.dart';
import '../../../../core/di/core_initializer.dart';
import '../../../core/theme/custom_colors.dart';
import '../../../../core/widgets/base_widgets.dart';
import '../../../layouts/base_scaffold.dart';
import 'responsive/fake_data.dart';
import 'widgets/header_cards.dart';

part 'responsive/home_page_desktop.dart';
part 'responsive/home_page_mobile.dart';
part 'responsive/home_page_tablet.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: Provider.of<ClaimProvider>(context, listen: false)
          .loadClaims(context),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Scaffold(
            body: Center(
              child: CircularProgressIndicator(),
            ),
          );
        } else if (snapshot.hasError) {
          return Scaffold(
            body: Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text('Bir hata oluştu.'),
                  const SizedBox(height: 10),
                  ElevatedButton(
                    onPressed: () {
                      Provider.of<ClaimProvider>(context, listen: false)
                          .loadClaims(context);
                    },
                    child: const Text('Tekrar Dene'),
                  ),
                ],
              ),
            ),
          );
        }

        return const ResponsiveWidget(
          mobile: HomePageMobile(),
          tablet: HomePageTablet(),
          desktop: HomePageDesktop(),
        );
      },
    );
  }
}
