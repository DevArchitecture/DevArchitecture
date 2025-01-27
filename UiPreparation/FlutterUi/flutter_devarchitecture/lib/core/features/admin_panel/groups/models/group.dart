import '/core/models/i_entity.dart';

class Group implements IEntity {
  @override
  int id;
  String groupName;

  Group({required this.id, required this.groupName});

  @override
  Map<String, dynamic> toMap() {
    return {"id": id, "groupName": groupName};
  }

  factory Group.fromMap(Map<String, dynamic> map) {
    return Group(id: map["id"], groupName: map["groupName"]);
  }
}
