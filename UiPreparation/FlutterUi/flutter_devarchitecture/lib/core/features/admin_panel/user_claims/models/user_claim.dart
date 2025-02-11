import '../../../../models/i_entity.dart';

class UserClaim implements IEntity {
  @override
  late int id;
  final int userId, claimId;

  UserClaim({
    required this.id,
    required this.userId,
    required this.claimId,
  }) {
    id = userId;
  }

  factory UserClaim.fromMap(Map<String, dynamic> json) {
    return UserClaim(
      id: json['id'] ?? 0,
      userId: json['userId'] ?? 0,
      claimId: json['claimId'] ?? 0,
    );
  }
  @override
  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'userId': userId,
      'claimId': claimId,
    };
  }
}
