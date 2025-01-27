import 'package:flutter/material.dart';

abstract class IChart {}

abstract class IGaugesChart implements IChart {
  Widget getCircleGaugeChart(
      double start, double end, double valid, Color color, Color pointerColor);

  Widget getVerticalGaugeChart(
      double start, double end, double valid, Color color, Color pointerColor);
}

abstract class IBasicChart implements IChart {
  Widget getBarChart(BuildContext context, List<Map<String, dynamic>> data,
      String xAxisHeader, String yAxisHeader, Color color,
      {String headerTitle = '',
      double? width,
      double? height,
      isVertical = true});

  Widget getLineChart(BuildContext context, List<Map<String, dynamic>> data,
      String xAxisHeader, String yAxisHeader, Color color,
      {String headerTitle = '',
      double? width,
      double? height,
      bool isSmooth = true});

  Widget getPieChart(BuildContext context, List<Map<String, dynamic>> data,
      String xAxisHeader, String yAxisHeader,
      {String headerTitle = '', double? width, double? height});

  Widget getStackChart(
      BuildContext context, List<Map<String, dynamic>> adjustData,
      {String headerTitle = '', double? width, double? height});

  Widget getLineAreaChart(BuildContext context, List<Map<String, dynamic>> data,
      String xAxisHeader, String yAxisHeader, Color color,
      {String headerTitle = '', double? width, double? height});
}

abstract class IAnalyticsChart implements IChart {
  Widget getCandleStickChart(
      BuildContext context, List<Map<String, dynamic>> stockData,
      {String headerTitle = '', double? width, double? height});
  Widget getHeatMapChart(BuildContext context, List<List<int>> heatmapData,
      String xAxisHeader, String yAxisHeader,
      {String headerTitle = '', double? width, double? height});

  Widget getScatterChart(BuildContext context, List<List<dynamic>> scatterData,
      {String headerTitle = '', double? width, double? height});
}

abstract class IEventStreamChart implements IChart {
  Widget getEventStreamChart(
      BuildContext context,
      List<Map<String, dynamic>> data,
      String xAxisHeader,
      String yAxisHeader,
      Color color,
      {String headerTitle = '',
      double? width,
      double? height});
}

abstract class IEChart implements IChart {
  Widget getEChart();
}
