import 'package:flutter/material.dart';

import '../../di/core_initializer.dart';
import '../../../layouts/base_scaffold.dart';

class BatteryStatusPage extends StatefulWidget {
  @override
  _BatteryStatusPageState createState() => _BatteryStatusPageState();
}

class _BatteryStatusPageState extends State<BatteryStatusPage> {
  String _batteryLevel = 'Unknown';
  String _chargingStatus = 'Unknown';

  late final batteryState = CoreInitializer().coreContainer.batteryState;
  late final screenMessage = CoreInitializer().coreContainer.screenMessage;

  @override
  void initState() {
    super.initState();
    batteryState.listenBatteryState(screenMessage);
  }

  Future<void> _getBatteryLevel() async {
    final level = await batteryState.getBatteryLevel();
    screenMessage.getInfoMessage("Battery Level: $level%");
    setState(() {
      _batteryLevel = '$level%';
    });
  }

  Future<void> _getChargingStatus() async {
    final isCharging = await batteryState.isBatteryCharging();
    screenMessage.getInfoMessage(
        "Charging Status: ${isCharging ? 'Charging' : 'Not Charging'}");
    setState(() {
      _chargingStatus = isCharging ? 'Charging' : 'Not Charging';
    });
  }

  @override
  void dispose() {
    batteryState.stopListeningBatteryState();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Padding(
        padding: const EdgeInsets.all(16.0),
        child: Center(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              SizedBox(height: 64),
              ElevatedButton(
                onPressed: _getBatteryLevel,
                child: Text('Get Battery Level'),
              ),
              Text('Battery Level: $_batteryLevel'),
              SizedBox(height: 16),
              ElevatedButton(
                onPressed: _getChargingStatus,
                child: Text('Get Charging Status'),
              ),
              Text('Charging Status: $_chargingStatus'),
            ],
          ),
        ),
      ),
    );
  }
}
