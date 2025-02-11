abstract class IMessageBroker {
  void getQueue();
  void close();
  Future<void> queueMessageAsync<T>(T messageModel);
  void queueMessageSync<T>(T messageModel);
}
