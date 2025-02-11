import '../../../../models/i_entity.dart';

class PasswordDto implements IEntity {
  String password;
  int userId;

  @override
  late int id;

  PasswordDto({
    required this.password,
    required this.userId,
  }) {
    id = userId;
  }

  @override
  Map<String, dynamic> toMap() {
    return {'password': password, 'userId': userId, 'id': id};
  }

  factory PasswordDto.fromMap(Map<String, dynamic> map) {
    return PasswordDto(
      password: map['password'],
      userId: map['userId'],
    );
  }
}
