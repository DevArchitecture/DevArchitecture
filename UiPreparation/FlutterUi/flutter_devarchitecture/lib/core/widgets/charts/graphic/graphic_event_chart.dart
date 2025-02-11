import 'package:flutter/material.dart';
import '../../../theme/extensions.dart';
import '/core/widgets/charts/i_chart.dart';
import 'package:graphic/graphic.dart';

class GraphicEventStreamChart extends IEventStreamChart {
  @override
  Widget getEventStreamChart(
      BuildContext context,
      List<Map<String, dynamic>> data,
      String xAxisHeader,
      String yAxisHeader,
      Color color,
      {String headerTitle = '',
      double? width,
      double? height}) {
    return Column(
      children: [
        Text(headerTitle),
        Container(
          margin: const EdgeInsets.only(top: 10),
          width: width ?? context.percent40Screen,
          height: height ?? context.percent30Screen,
          child: Chart(
            rebuild: false,
            data: data,
            variables: {
              xAxisHeader: Variable(
                accessor: (Map map) => map['x_axis'] as String,
              ),
              yAxisHeader: Variable(
                accessor: (Map map) => map['y_axis'] as num,
              ),
            },
            marks: [IntervalMark()],
            axes: [
              Defaults.horizontalAxis,
              Defaults.verticalAxis,
            ],
            selections: {
              'tap': PointSelection(
                on: {
                  GestureType.hover,
                  GestureType.tap,
                },
                dim: Dim.x,
              )
            },
            tooltip: TooltipGuide(
              backgroundColor: Colors.black,
              elevation: 5,
              textStyle: Defaults.textStyle,
              variables: [xAxisHeader, yAxisHeader],
            ),
            crosshair: CrosshairGuide(),
          ),
        ),
      ],
    );
  }
}
