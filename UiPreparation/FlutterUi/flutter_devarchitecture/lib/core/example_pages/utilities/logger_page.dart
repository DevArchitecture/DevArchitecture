import 'dart:io';

import 'package:flutter/material.dart';
import '/core/di/core_initializer.dart';
import '../../../layouts/base_scaffold.dart';

import '../../utilities/logger/i_logger.dart';

class LoggerPage extends StatefulWidget {
  @override
  _LoggerPageState createState() => _LoggerPageState();
}

class _LoggerPageState extends State<LoggerPage> {
  late ILogger _logger;

  @override
  void initState() {
    super.initState();
    _logger = CoreInitializer().coreContainer.logger;
  }

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      Center(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            children: [
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () => _logger.logError('Bu bir hata mesajıdır.'),
                  child: Text('Hata Mesajı Gönder'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () =>
                      _logger.logWarning('Bu bir uyarı mesajıdır.'),
                  child: Text('Uyarı Mesajı Gönder'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () => _logger.logInfo('Bu bir bilgi mesajıdır.'),
                  child: Text('Bilgi Mesajı Gönder'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () =>
                      _logger.logSuccess('İşlem başarıyla tamamlandı.'),
                  child: Text('Başarı Mesajı Gönder'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () => _logger.logDebug('Bu bir debug mesajıdır.'),
                  child: Text('Debug Mesajı Gönder'),
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 2,
                child: ElevatedButton(
                  onPressed: () {
                    _logger.logTrace('İzleme mesajı', () {
                      // Burada izlenecek işlem gerçekleşir
                      sleep(Duration(seconds: 5));
                      CoreInitializer()
                          .coreContainer
                          .logger
                          .logDebug('İzlenecek işlem çalışıyor...');
                    });
                  },
                  child: Text('İzleme Mesajı Gönder'),
                ),
              ),
              const Spacer(),
            ],
          ),
        ),
      ),
    );
  }
}
