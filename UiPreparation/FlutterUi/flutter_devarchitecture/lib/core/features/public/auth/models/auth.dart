import '../../../../models/i_entity.dart';

class AuthResponse {
  late String token;
  late String refreshToken;
  late List<String> claims;
  late DateTime expiration;

  AuthResponse(
      {required this.token,
      required this.claims,
      required this.refreshToken,
      required this.expiration});

  Map<String, dynamic> toMap() {
    return {
      'token': token,
      'refreshToken': refreshToken,
      'claims': claims,
      'expiration': expiration.toString()
    };
  }

  factory AuthResponse.fromMap(Map<String, dynamic> map) {
    return AuthResponse(
      token: map['token'],
      refreshToken: map['refreshToken'],
      claims: List<String>.from(map['claims']),
      expiration: DateTime.parse(map['expiration']),
    );
  }
}

class AuthRequestBasic implements IEntity {
  late String email;
  late String password;
  late String lang;

  AuthRequestBasic(
      {required this.email, required this.password, required this.lang});

  @override
  Map<String, dynamic> toMap() {
    return {'email': email, 'password': password, 'lang': lang};
  }

  factory AuthRequestBasic.fromMap(Map<String, dynamic> map) {
    return AuthRequestBasic(
      email: map['email'],
      password: map['password'],
      lang: map['lang'],
    );
  }

  @override
  late int id = 0;
}
