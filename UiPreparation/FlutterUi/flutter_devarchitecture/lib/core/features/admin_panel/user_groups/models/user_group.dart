import '../../../../models/i_entity.dart';

class UserGroup implements IEntity {
  @override
  late int id;
  late int groupId;
  late int userId;

  UserGroup({required this.id, required this.groupId, required this.userId});

  @override
  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'groupId': groupId,
      'userId': userId,
    };
  }

  @override
  factory UserGroup.fromMap(Map<String, dynamic> map) {
    return UserGroup(
      id: map['id'] ?? 0,
      groupId: map['groupId'] ?? 0,
      userId: map['userId'] ?? 0,
    );
  }
}
