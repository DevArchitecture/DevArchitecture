import '/core/models/i_entity.dart';

class OperationClaimDto implements IEntity {
  @override
  late int id;

  String alias;
  String description;

  OperationClaimDto(
      {required this.id, required this.alias, required this.description});

  @override
  Map<String, dynamic> toMap() {
    return {"id": id, "alias": alias, "description": description};
  }

  factory OperationClaimDto.fromMap(Map<String, dynamic> map) {
    return OperationClaimDto(
        id: map["id"] ?? 0,
        alias: map["alias"] ?? "",
        description: map["description"] ?? "");
  }
}
