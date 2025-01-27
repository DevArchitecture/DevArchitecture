import '../../../../models/i_dto.dart';

class TranslateDto implements IDto {
  int id;
  String code;
  String language;
  String value;

  TranslateDto(
      {required this.id,
      required this.code,
      required this.language,
      required this.value});

  factory TranslateDto.fromMap(Map<String, dynamic> map) {
    return TranslateDto(
      id: map['id'] ?? 0,
      code: map['code'] ?? "",
      language: map['language'] ?? "",
      value: map['value'] ?? "",
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'code': code,
      'language': language,
      'value': value,
    };
  }
}
