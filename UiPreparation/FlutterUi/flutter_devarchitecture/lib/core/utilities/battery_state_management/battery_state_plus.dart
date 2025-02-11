import 'dart:async';
import 'package:battery_plus/battery_plus.dart';
import '../screen_message/i_screen_message.dart';
import 'i_battery_state.dart';

class BatteryStateBatteryPlus implements IBatteryState {
  late StreamSubscription<BatteryState> _listener;

  @override
  Future<int> getBatteryLevel() async {
    var battery = Battery();
    return await battery.batteryLevel;
  }

  @override
  Future<bool> isBatteryCharging() async {
    var battery = Battery();
    return await battery.batteryState == BatteryState.charging;
  }

  @override
  Future<void> listenBatteryState(IScreenMessage screenMessage) async {
    var battery = Battery();
    _listener = battery.onBatteryStateChanged.listen((BatteryState state) {
      screenMessage.getInfoMessage("State: ${state.toString()}");
      // TODO:Do something with new state
    });
  }

  @override
  Future<void> stopListeningBatteryState() async {
    await _listener.cancel();
  }
}
