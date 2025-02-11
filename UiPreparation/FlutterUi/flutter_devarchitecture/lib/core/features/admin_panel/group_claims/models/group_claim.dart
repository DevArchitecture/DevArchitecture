import '../../../../models/i_entity.dart';

class GroupClaim implements IEntity {
  @override
  late int id;

  late int groupId;

  late int claimId;

  GroupClaim({
    this.id = 0,
    required this.groupId,
    required this.claimId,
  });

  GroupClaim.fromMap(Map<String, dynamic> map) {
    id = map['groupId'];
    groupId = map['groupId'];
    claimId = map['claimId'];
  }

  @override
  Map<String, dynamic> toMap() {
    return {
      'id': groupId,
      'groupId': groupId,
      'claimId': claimId,
    };
  }
}
