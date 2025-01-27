import 'package:flutter/material.dart';
import '/core/di/core_initializer.dart';
import '/core/utilities/screen_message/i_screen_message.dart';

import '../../../layouts/base_scaffold.dart';

class ScreenMessagePage extends StatefulWidget {
  @override
  _ScreenMessagePageState createState() => _ScreenMessagePageState();
}

class _ScreenMessagePageState extends State<ScreenMessagePage> {
  late IScreenMessage _screenMessage;

  @override
  void initState() {
    super.initState();
    _screenMessage = CoreInitializer().coreContainer.screenMessage;
  }

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      _buildBody(),
    );
  }

  Widget _buildBody() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          ElevatedButton(
            onPressed: () =>
                _screenMessage.getInfoMessage("Bu bir bilgi mesajıdır."),
            child: Text('Bilgi Mesajı Göster'),
          ),
          const SizedBox(height: 16),
          ElevatedButton(
            onPressed: () =>
                _screenMessage.getSuccessMessage("İşlem başarılı!"),
            child: Text('Başarı Mesajı Göster'),
          ),
          const SizedBox(height: 16),
          ElevatedButton(
            onPressed: () => _screenMessage.getErrorMessage("Bir hata oluştu!"),
            child: Text('Hata Mesajı Göster'),
          ),
          const SizedBox(height: 16),
          ElevatedButton(
            onPressed: () =>
                _screenMessage.getWarningMessage("Dikkat! Bu bir uyarıdır."),
            child: Text('Uyarı Mesajı Göster'),
          ),
        ],
      ),
    );
  }
}
