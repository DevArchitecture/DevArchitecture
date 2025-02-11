import 'package:flutter/material.dart';
import '../../../theme/extensions.dart';
import '/core/widgets/charts/i_chart.dart';
import 'package:graphic/graphic.dart';

class GraphicAnalyticsChart extends IAnalyticsChart {
  @override
  Widget getCandleStickChart(
      BuildContext context, List<Map<String, dynamic>> stockData,
      {String headerTitle = '', double? width, double? height}) {
    return Column(
      children: [
        Text(headerTitle),
        Container(
          margin: const EdgeInsets.only(top: 10),
          width: width ?? context.percent40Screen,
          height: height ?? context.percent30Screen,
          child: Chart(
            data: stockData.reversed.toList(),
            variables: {
              'time': Variable(
                accessor: (Map datumn) => datumn['time'].toString(),
                scale: OrdinalScale(tickCount: 4),
              ),
              'start': Variable(
                accessor: (Map datumn) => datumn['start'] as num,
                scale: LinearScale(min: 6, max: 9),
              ),
              'max': Variable(
                accessor: (Map datumn) => datumn['max'] as num,
                scale: LinearScale(min: 6, max: 9),
              ),
              'min': Variable(
                accessor: (Map datumn) => datumn['min'] as num,
                scale: LinearScale(min: 6, max: 9),
              ),
              'end': Variable(
                accessor: (Map datumn) => datumn['end'] as num,
                scale: LinearScale(min: 6, max: 9),
              ),
            },
            marks: [
              CustomMark(
                elevation: ElevationEncode(value: 0, updaters: {
                  'tap': {true: (_) => stockData.length.toDouble()}
                }),
                shape: ShapeEncode(value: CandlestickShape()),
                position: Varset('time') *
                    (Varset('start') +
                        Varset('max') +
                        Varset('min') +
                        Varset('end')),
                color: ColorEncode(
                    encoder: (tuple) => tuple['end'] >= tuple['start']
                        ? Colors.red
                        : Colors.green),
              )
            ],
            axes: [
              Defaults.horizontalAxis,
              Defaults.verticalAxis,
            ],
            coord: RectCoord(
                horizontalRangeUpdater: Defaults.horizontalRangeEvent),
            selections: {
              'touchMove': PointSelection(
                on: {
                  GestureType.scaleUpdate,
                  GestureType.tapDown,
                  GestureType.longPressMoveUpdate
                },
                dim: Dim.x,
              )
            },
            tooltip: TooltipGuide(
              followPointer: [false, true],
              align: Alignment.topLeft,
              offset: const Offset(-20, -20),
            ),
            crosshair: CrosshairGuide(followPointer: [false, true]),
          ),
        ),
      ],
    );
  }

  @override
  Widget getHeatMapChart(BuildContext context, List<List<int>> heatmapData,
      String xAxisHeader, String yAxisHeader,
      {String headerTitle = '', double? width, double? height}) {
    return Column(
      children: [
        Text(headerTitle),
        Container(
          margin: const EdgeInsets.only(top: 10),
          width: width ?? context.percent40Screen,
          height: height ?? context.percent30Screen,
          child: Chart(
            data: heatmapData,
            variables: {
              xAxisHeader: Variable(
                accessor: (List datum) => datum[0].toString(),
              ),
              yAxisHeader: Variable(
                accessor: (List datum) => datum[1].toString(),
              ),
              'value': Variable(
                accessor: (List datum) => datum[2] as num,
              ),
            },
            marks: [
              PolygonMark(
                color: ColorEncode(
                  variable: 'value',
                  values: [
                    const Color(0xffbae7ff),
                    const Color(0xff1890ff),
                    const Color(0xff0050b3)
                  ],
                ),
              )
            ],
            axes: [
              Defaults.horizontalAxis,
              Defaults.verticalAxis,
            ],
            selections: {'tap': PointSelection()},
            tooltip: TooltipGuide(),
          ),
        ),
      ],
    );
  }

  @override
  Widget getScatterChart(BuildContext context, List<List<dynamic>> scatterData,
      {String headerTitle = '', double? width, double? height}) {
    return Column(
      children: [
        Text(headerTitle),
        Container(
          margin: const EdgeInsets.only(top: 10),
          width: width ?? context.percent40Screen,
          height: height ?? context.percent30Screen,
          child: Chart(
            data: scatterData,
            variables: {
              '0': Variable(
                accessor: (List datum) => datum[0] as num,
              ),
              '1': Variable(
                accessor: (List datum) => datum[1] as num,
              ),
              '2': Variable(
                accessor: (List datum) => datum[2] as num,
              ),
              '4': Variable(
                accessor: (List datum) => datum[4].toString(),
              ),
            },
            marks: [
              PointMark(
                size: SizeEncode(variable: '2', values: [5, 20]),
                color: ColorEncode(
                  variable: '4',
                  values: Defaults.colors10,
                  updaters: {
                    'choose': {true: (_) => Colors.red}
                  },
                ),
                shape: ShapeEncode(variable: '4', values: [
                  CircleShape(hollow: true),
                  SquareShape(hollow: true),
                ]),
              )
            ],
            axes: [
              Defaults.horizontalAxis,
              Defaults.verticalAxis,
            ],
            coord: RectCoord(
              horizontalRange: [0.05, 0.95],
              verticalRange: [0.05, 0.95],
              horizontalRangeUpdater: Defaults.horizontalRangeEvent,
              verticalRangeUpdater: Defaults.verticalRangeEvent,
            ),
            selections: {'choose': PointSelection(toggle: true)},
            tooltip: TooltipGuide(
              anchor: (_) => Offset.zero,
              align: Alignment.bottomRight,
              multiTuples: true,
            ),
          ),
        ),
      ],
    );
  }
}
