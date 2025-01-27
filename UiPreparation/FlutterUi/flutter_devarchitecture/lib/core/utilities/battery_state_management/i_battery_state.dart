import '../screen_message/i_screen_message.dart';

abstract class IBatteryState {
  Future<int> getBatteryLevel();

  Future<bool> isBatteryCharging();

  Future<void> listenBatteryState(IScreenMessage screenMessage);

  Future<void> stopListeningBatteryState();
}
