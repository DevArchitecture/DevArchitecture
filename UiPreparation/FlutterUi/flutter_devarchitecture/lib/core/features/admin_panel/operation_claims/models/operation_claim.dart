import '/core/models/i_entity.dart';

class OperationClaim implements IEntity {
  @override
  late int id;

  String name;
  String alias;
  String description;

  OperationClaim(
      {required this.id,
      required this.name,
      required this.alias,
      required this.description});

  @override
  Map<String, dynamic> toMap() {
    return {"id": id, "name": name, "alias": alias, "description": description};
  }

  factory OperationClaim.fromMap(Map<String, dynamic> map) {
    return OperationClaim(
        id: map["id"] ?? 0,
        name: map["name"] ?? "",
        alias: map["alias"] ?? "",
        description: map["description"] ?? "");
  }
}
