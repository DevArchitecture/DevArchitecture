import '/core/models/i_dto.dart';

class LogDto implements IDto {
  int id;

  String level;

  String exceptionMessage;

  DateTime timeStamp;

  String user;

  String value;

  String type;

  LogDto({
    required this.id,
    required this.level,
    required this.exceptionMessage,
    required this.timeStamp,
    required this.user,
    required this.value,
    required this.type,
  });

  @override
  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'level': level,
      'exceptionMessage': exceptionMessage,
      'timeStamp': timeStamp,
      'user': user,
      'value': value,
      'type': type,
    };
  }

  factory LogDto.fromMap(Map<String, dynamic> map) {
    return LogDto(
      id: map['id'],
      level: map['level'] ?? "",
      exceptionMessage: map['exceptionMessage'] ?? "",
      timeStamp: DateTime.parse(map['timeStamp'] ?? DateTime.now()),
      user: map['user'] ?? "",
      value: map['value'] ?? "",
      type: map['type'] ?? "",
    );
  }
}
