import 'package:flutter/material.dart';
import '../../di/core_initializer.dart';
import '../../../layouts/base_scaffold.dart';

class DeviceInfoPage extends StatefulWidget {
  @override
  _DeviceInfoPageState createState() => _DeviceInfoPageState();
}

class _DeviceInfoPageState extends State<DeviceInfoPage> {
  late final deviceInfoService =
      CoreInitializer().coreContainer.deviceInformation;
  Map<String, dynamic> _deviceData = {};

  @override
  void initState() {
    super.initState();
    _loadDeviceInfo();
  }

  Future<void> _loadDeviceInfo() async {
    final deviceData = await deviceInfoService.getPlatformBaseDeviceInfo();
    setState(() {
      _deviceData = deviceData;
    });
  }

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text('Device Information',
                style: Theme.of(context).textTheme.headlineLarge),
            SizedBox(height: 16),
            ..._deviceData.entries.map((entry) => Expanded(
                  child: Padding(
                    padding: const EdgeInsets.symmetric(vertical: 4.0),
                    child: Row(
                      children: [
                        Text('${entry.key}: ',
                            style: TextStyle(fontWeight: FontWeight.bold)),
                        Expanded(child: Text('${entry.value}')),
                      ],
                    ),
                  ),
                )),
          ],
        ),
      ),
      isDrawer: true, // Eğer Drawer kullanılacaksa true olarak bırakabilirsiniz
    );
  }
}
