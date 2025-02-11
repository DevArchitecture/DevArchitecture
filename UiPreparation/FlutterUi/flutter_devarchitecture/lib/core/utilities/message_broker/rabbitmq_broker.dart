import 'package:dart_amqp/dart_amqp.dart';
import 'dart:convert';

import '../../di/core_initializer.dart';
import 'i_message_broker.dart';

class RabbitMQMessageBroker implements IMessageBroker {
  final String _queueName;
  final ConnectionSettings _settings;
  late Client _client;
  late Channel _channel;
  late Queue _queue;

  RabbitMQMessageBroker(this._queueName, this._settings) {
    _client = Client(settings: _settings);
  }

  @override
  void getQueue() async {
    try {
      _channel = await _client.channel();
      _queue = await _channel.queue(_queueName, durable: true);

      Consumer consumer = await _queue.consume();

      consumer.listen((AmqpMessage message) {
        CoreInitializer()
            .coreContainer
            .logger
            .logDebug("Received message: ${message.payloadAsString}");
        message.ack();
      });
    } catch (e) {
      CoreInitializer().coreContainer.logger.logDebug("Error: $e");
    }
  }

  @override
  void close() {
    _client.close();
  }

  @override
  Future<void> queueMessageAsync<T>(T messageModel) async {
    try {
      final jsonString = jsonEncode(messageModel);
      _queue.publish(jsonString);
    } catch (e) {
      CoreInitializer().coreContainer.logger.logDebug("Error: $e");
    }
  }

  @override
  void queueMessageSync<T>(T messageModel) {
    try {
      final jsonString = jsonEncode(messageModel);
      _queue.publish(jsonString);
    } catch (e) {
      CoreInitializer().coreContainer.logger.logDebug("Error: $e");
    }
  }

  Future<void> _initializeQueue() async {
    _channel = await _client.channel();
    _queue = await _channel.queue(_queueName, durable: true);
  }
}
