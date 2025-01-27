import '../../../../models/i_entity.dart';

class Translate implements IEntity {
  int langId;
  String code;
  String value;

  @override
  late int id;

  Translate(
      {required this.id,
      required this.langId,
      required this.code,
      required this.value}) {
    id = langId;
  }

  @override
  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'langId': langId,
      'code': code,
      'value': value,
    };
  }

  factory Translate.fromMap(Map<String, dynamic> map) {
    return Translate(
      id: map['id'] ?? 0,
      langId: map['langId'] ?? 0,
      code: map['code'] ?? "",
      value: map['value'] ?? "",
    );
  }
}
