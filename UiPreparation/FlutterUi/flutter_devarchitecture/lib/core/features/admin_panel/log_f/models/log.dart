import '../../../../models/i_entity.dart';

class Log implements IEntity {
  @override
  int id;
  String messageTemplate;
  String level;
  DateTime timeStamp;
  String exception;

  Log({
    required this.id,
    required this.messageTemplate,
    required this.level,
    required this.timeStamp,
    required this.exception,
  });

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'messageTemplate': messageTemplate,
      'level': level,
      'timeStamp': timeStamp,
      'exception': exception,
    };
  }

  factory Log.fromMap(Map<String, dynamic> map) {
    return Log(
      id: map['id'],
      messageTemplate: map['messageTemplate'],
      level: map['level'],
      timeStamp: map['timeStamp'],
      exception: map['exception'],
    );
  }
}
