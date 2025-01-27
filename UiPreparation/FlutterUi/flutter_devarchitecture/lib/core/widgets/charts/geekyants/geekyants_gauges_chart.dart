import 'package:flutter/material.dart';
import 'package:geekyants_flutter_gauges/geekyants_flutter_gauges.dart';

import '../../../theme/custom_colors.dart';
import '../i_chart.dart';

class GeekyantsGaugesChart implements IGaugesChart {
  static final GeekyantsGaugesChart _singleton =
      GeekyantsGaugesChart._internal();

  factory GeekyantsGaugesChart() {
    return _singleton;
  }

  GeekyantsGaugesChart._internal();

  @override
  Widget getCircleGaugeChart(
      double start, double end, double valid, Color color, Color pointerColor) {
    return RadialGauge(
      valueBar: [RadialValueBar(value: valid, color: color)],
      needlePointer: [
        NeedlePointer(
          value: valid,
          color: pointerColor,
          tailRadius: 18,
          tailColor: color,
          isInteractive: false,
          needleHeight: 96,
          needleWidth: 6,
        )
      ],
      track: RadialTrack(
        thickness: 10,
        color: CustomColors.light.getColor,
        trackStyle: TrackStyle(
          primaryRulersHeight: 3,
          secondaryRulerPerInterval: 2,
          showLabel: true,
          labelStyle:
              TextStyle(color: CustomColors.dark.getColor, fontSize: 10),
        ),
        steps: (end.toInt() ~/ 10),
        start: start,
        end: end,
        startAngle: -45,
        endAngle: 225,
      ),
    );
  }

  @override
  Widget getVerticalGaugeChart(
      double start, double end, double valid, Color color, Color pointerColor) {
    return LinearGauge(
      end: 126,
      gaugeOrientation: GaugeOrientation.vertical,
      rulers: RulerStyle(
        rulerPosition: RulerPosition.right,
      ),
      pointers: const [
        Pointer(
          value: 50,
          height: 20,
          color: Colors.green,
          width: 20,
          shape: PointerShape.triangle,
          isInteractive: true,
          onChanged: null,
          pointerPosition: PointerPosition.left,
        ),
      ],
      curves: const [
        CustomCurve(
          startHeight: 4,
          endHeight: 50,
          midHeight: 5,
          curvePosition: CurvePosition.left,
          end: 126,
          midPoint: 50 * 0.8,
        ),
      ],
    );
  }
}
