part of '../home_page.dart';

class HomePageDesktop extends StatefulWidget {
  const HomePageDesktop({super.key});

  @override
  State<HomePageDesktop> createState() => _HomePageDesktopState();
}

class _HomePageDesktopState extends State<HomePageDesktop> {
  final rdm = Random();
  List<Map<String, dynamic>> streamData = [];
  late Timer timer;

  @override
  void dispose() {
    super.dispose();
  }

  @override
  void initState() {
    streamData.add(
      {'x_axis': 'No.${rdm.nextInt(300)}', 'y_axis': rdm.nextInt(300)},
    );
    streamData.add(
      {'x_axis': 'No.${rdm.nextInt(300)}', 'y_axis': rdm.nextInt(300)},
    );
    streamData.add(
      {'x_axis': 'No.${rdm.nextInt(300)}', 'y_axis': rdm.nextInt(300)},
    );

    timer = Timer.periodic(const Duration(seconds: 1), (_) {
      if (mounted) {
        setState(() {
          streamData = [
            {'x_axis': 'No.${rdm.nextInt(300)}', 'y_axis': rdm.nextInt(300)},
            {'x_axis': 'No.${rdm.nextInt(300)}', 'y_axis': rdm.nextInt(300)},
            {'x_axis': 'No.${rdm.nextInt(300)}', 'y_axis': rdm.nextInt(300)},
          ];
        });
      }
    });

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
        context,
        CustomScrollView(
          slivers: [
            SliverFillRemaining(
              hasScrollBody: false,
              child: Column(
                children: <Widget>[
                  Expanded(
                    child: Padding(
                      padding: context.defaultPadding,
                      child: Column(
                        children: [
                          Expanded(
                            flex: 10,
                            child: buildPageTitle(
                                context, SidebarConstants.homePageTitle,
                                subDirection:
                                    SidebarConstants.appPanelPageTitle),
                          ),
                          Expanded(
                            flex: 20,
                            child: Row(
                              children: [
                                Expanded(
                                  child: buildUsedSpaceCardWidget(context),
                                ),
                                Expanded(
                                  child: buildRevenueCardWidget(context),
                                ),
                                Expanded(
                                  child: BuildFixedIssuesCardWidget(context),
                                ),
                                Expanded(
                                  child: buildFollowersCardWidget(context),
                                ),
                              ],
                            ),
                          ),
                          const Spacer(flex: 5),
                          Expanded(
                              flex: 30,
                              child: Padding(
                                padding: context.highHorizontalPadding,
                                child: Row(
                                  mainAxisAlignment: MainAxisAlignment.center,
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  children: [
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .basicChart
                                            .getBarChart(
                                                context,
                                                basicData,
                                                CoreScreenTexts.categories,
                                                CoreScreenTexts.sales,
                                                headerTitle: 'BAR CHART',
                                                CustomColors
                                                    .secondary.getColor)),
                                    const Spacer(),
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .basicChart
                                            .getLineChart(
                                                headerTitle: 'LINE CHART',
                                                context,
                                                basicData,
                                                CoreScreenTexts.categories,
                                                CoreScreenTexts.sales,
                                                CustomColors.danger.getColor)),
                                    const Spacer(),
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .basicChart
                                            .getPieChart(
                                              headerTitle: 'PİE CHART',
                                              context,
                                              basicData,
                                              CoreScreenTexts.categories,
                                              CoreScreenTexts.sales,
                                            ))
                                  ],
                                ),
                              )),
                          const Spacer(flex: 5),
                          Expanded(
                              flex: 30,
                              child: Padding(
                                padding: context.highHorizontalPadding,
                                child: Row(
                                  mainAxisAlignment: MainAxisAlignment.center,
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  children: [
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .basicChart
                                            .getStackChart(
                                                headerTitle: 'STACK CHART',
                                                context,
                                                adjustData)),
                                    const Spacer(),
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .basicChart
                                            .getLineAreaChart(
                                                headerTitle: 'LINE AREA CHART',
                                                context,
                                                basicData,
                                                CoreScreenTexts.categories,
                                                CoreScreenTexts.sales,
                                                CustomColors
                                                    .secondary.getColor)),
                                    const Spacer(),
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .analyticsChart
                                            .getCandleStickChart(
                                                headerTitle:
                                                    'CANDLE STICK CHART',
                                                context,
                                                stockData)),
                                  ],
                                ),
                              )),
                          const Spacer(flex: 5),
                          Expanded(
                              flex: 30,
                              child: Padding(
                                padding: context.highHorizontalPadding,
                                child: Row(
                                  mainAxisAlignment: MainAxisAlignment.center,
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  children: [
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .analyticsChart
                                            .getHeatMapChart(
                                              context,
                                              headerTitle: 'HEATMAP CHART',
                                              heatmapData,
                                              CoreScreenTexts.name,
                                              CoreScreenTexts.days,
                                            )),
                                    const Spacer(),
                                    Expanded(
                                        flex: 5,
                                        child: CoreInitializer()
                                            .coreContainer
                                            .analyticsChart
                                            .getScatterChart(
                                                headerTitle: 'SCATTER CHART',
                                                context,
                                                scatterData)),
                                    const Spacer(),
                                    Expanded(
                                      flex: 5,
                                      child: CoreInitializer()
                                          .coreContainer
                                          .eventStreamChart
                                          .getEventStreamChart(
                                              headerTitle: 'EVENT STREAM CHART',
                                              context,
                                              streamData,
                                              CoreScreenTexts.categories,
                                              CoreScreenTexts.sales,
                                              CustomColors.warning.getColor),
                                    ),
                                  ],
                                ),
                              )),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ));
  }
}
