import 'package:flutter/material.dart';
import '/core/theme/extensions.dart';
import 'package:graphic/graphic.dart';

import '../../../theme/custom_colors.dart';
import '../i_chart.dart';

class GraphicBasicChart extends IBasicChart {
  @override
  Widget getBarChart(BuildContext context, List<Map<String, dynamic>> data,
      String xAxisHeader, String yAxisHeader, Color color,
      {String headerTitle = '',
      double? width,
      double? height,
      isVertical = true}) {
    return Column(children: [
      Text(headerTitle),
      Container(
        margin: const EdgeInsets.only(top: 10),
        width: width ?? context.percent40Screen,
        height: height ?? context.percent30Screen,
        child: Chart(
          data: data,
          variables: {
            xAxisHeader: Variable(
              accessor: (Map map) => map['x_axis'] as String,
            ),
            yAxisHeader: Variable(
              accessor: (Map map) => map['y_axis'] as num,
            ),
          },
          marks: [
            IntervalMark(
              label: LabelEncode(
                  encoder: (tuple) => Label(tuple[yAxisHeader].toString())),
              elevation: ElevationEncode(value: 0, updaters: {
                'tap': {true: (_) => data.length.toDouble()}
              }),
              color: ColorEncode(value: color, updaters: {
                'tap': {false: (color) => color.withAlpha(100)}
              }),
            )
          ],
          coord: RectCoord(transposed: !isVertical),
          axes: [
            isVertical ? Defaults.horizontalAxis : Defaults.verticalAxis,
            isVertical ? Defaults.verticalAxis : Defaults.horizontalAxis,
          ],
          selections: {'tap': PointSelection(dim: Dim.x)},
          tooltip: TooltipGuide(),
          crosshair: CrosshairGuide(),
        ),
      ),
    ]);
  }

  @override
  Widget getLineChart(BuildContext context, List<Map<String, dynamic>> data,
      String xAxisHeader, String yAxisHeader, Color color,
      {String headerTitle = '',
      double? width,
      double? height,
      bool isSmooth = true}) {
    return Column(
      children: [
        Text(headerTitle),
        Container(
          margin: const EdgeInsets.only(top: 10),
          width: width ?? context.percent40Screen,
          height: height ?? context.percent30Screen,
          child: Chart(
            data: data,
            variables: {
              xAxisHeader: Variable(
                accessor: (Map map) => map['x_axis'] as String,
              ),
              yAxisHeader: Variable(
                accessor: (Map map) => map['y_axis'] as num,
              ),
            },
            marks: [
              IntervalMark(
                label: LabelEncode(
                    encoder: (tuple) => Label(tuple[yAxisHeader].toString())),
                elevation: ElevationEncode(value: 0, updaters: {
                  'tap': {true: (_) => data.length.toDouble()}
                }),
                color: ColorEncode(
                    value: CustomColors.transparent.getColor,
                    updaters: {
                      'tap': {
                        false: (color) => CustomColors.transparent.getColor
                      }
                    }),
              ),
              PointMark(
                color: ColorEncode(value: color),
                size: SizeEncode(value: 8),
              ),
              LineMark(
                shape: ShapeEncode(value: BasicLineShape(smooth: isSmooth)),
                color: ColorEncode(value: color),
                size: SizeEncode(value: 3),
                selected: {
                  'touchMove': {1}
                },
              )
            ],
            axes: [
              Defaults.horizontalAxis,
              Defaults.verticalAxis,
            ],
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
  Widget getPieChart(BuildContext context, List<Map<String, dynamic>> data,
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
            data: data,
            variables: {
              xAxisHeader: Variable(
                accessor: (Map map) => map['x_axis'] as String,
              ),
              yAxisHeader: Variable(
                accessor: (Map map) => map['y_axis'] as num,
              ),
            },
            transforms: [
              Proportion(
                variable: yAxisHeader,
                as: 'Yüzde',
              )
            ],
            marks: [
              IntervalMark(
                position: Varset('Yüzde') / Varset(xAxisHeader),
                label: LabelEncode(
                    encoder: (tuple) => Label(
                          tuple[yAxisHeader].toString(),
                          LabelStyle(textStyle: Defaults.runeStyle),
                        )),
                elevation: ElevationEncode(value: 0, updaters: {
                  'tap': {true: (_) => data.length.toDouble()}
                }),
                color: ColorEncode(
                    values: Defaults.colors10,
                    variable: xAxisHeader,
                    updaters: {
                      'tap': {
                        false: (color) => CustomColors.transparent.getColor
                      }
                    }),
                modifiers: [StackModifier()],
              )
            ],
            coord: PolarCoord(transposed: true, dimCount: 1),
            selections: {
              'touchMove': PointSelection(
                on: {
                  GestureType.scaleUpdate,
                  GestureType.tapDown,
                  GestureType.longPressMoveUpdate
                },
                dim: Dim.y,
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
  Widget getStackChart(
      BuildContext context, List<Map<String, dynamic>> adjustData,
      {String headerTitle = '', double? width, double? height}) {
    return Column(
      children: [
        Text(headerTitle),
        Container(
          margin: const EdgeInsets.only(top: 10),
          width: width ?? context.percent40Screen,
          height: height ?? context.percent30Screen,
          child: Chart(
            data: adjustData,
            variables: {
              'index': Variable(
                accessor: (Map map) => map['index'].toString(),
              ),
              'type': Variable(
                accessor: (Map map) => map['type'] as String,
              ),
              'value': Variable(
                accessor: (Map map) => map['value'] as num,
                scale: LinearScale(min: 0, max: 1800),
              ),
            },
            marks: [
              IntervalMark(
                position: Varset('index') * Varset('value') / Varset('type'),
                shape: ShapeEncode(value: RectShape(labelPosition: 0.5)),
                color: ColorEncode(variable: 'type', values: Defaults.colors10),
                label: LabelEncode(
                    encoder: (tuple) => Label(
                          tuple['value'].toString(),
                          LabelStyle(textStyle: const TextStyle(fontSize: 6)),
                        )),
                modifiers: [StackModifier()],
              )
            ],
            coord: RectCoord(
              horizontalRangeUpdater: Defaults.horizontalRangeEvent,
            ),
            axes: [
              Defaults.horizontalAxis,
              Defaults.verticalAxis,
            ],
            selections: {
              'tap': PointSelection(
                variable: 'index',
              )
            },
            tooltip: TooltipGuide(multiTuples: true),
            crosshair: CrosshairGuide(),
          ),
        ),
      ],
    );
  }

  @override
  Widget getLineAreaChart(BuildContext context, List<Map<String, dynamic>> data,
      String xAxisHeader, String yAxisHeader, Color color,
      {String headerTitle = '', double? width, double? height}) {
    return Column(
      children: [
        Text(headerTitle),
        Container(
          margin: const EdgeInsets.only(top: 10),
          width: width ?? context.percent40Screen,
          height: height ?? context.percent30Screen,
          child: Chart(
            data: data,
            variables: {
              xAxisHeader: Variable(
                accessor: (Map map) => map['x_axis'] as String,
                scale: OrdinalScale(tickCount: 5),
              ),
              yAxisHeader: Variable(
                accessor: (Map map) => (map['y_axis'] ?? double.nan) as num,
              ),
            },
            marks: [
              AreaMark(
                shape: ShapeEncode(value: BasicAreaShape(smooth: true)),
                color: ColorEncode(value: color, updaters: {
                  'tap': {false: (color) => color.withAlpha(500)}
                }),
              ),
              LineMark(
                color: ColorEncode(value: color, updaters: {
                  'tap': {false: (color) => color.withAlpha(500)}
                }),
                shape: ShapeEncode(value: BasicLineShape(smooth: true)),
                size: SizeEncode(value: 0.5),
              ),
            ],
            axes: [
              Defaults.horizontalAxis,
              Defaults.verticalAxis,
            ],
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
}
