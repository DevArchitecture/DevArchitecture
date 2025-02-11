final List<User> users = [
  User(
      id: 1,
      name: 'Ahmet Yılmaz',
      email: 'ahmet.yilmaz@example.com',
      userType: 'Admin'),
  User(
      id: 2,
      name: 'Ayşe Kaya',
      email: 'ayse.kaya@example.com',
      userType: 'User'),
  User(
      id: 3,
      name: 'Mehmet Demir',
      email: 'mehmet.demir@example.com',
      userType: 'Moderator'),
  User(
      id: 4,
      name: 'Fatma Çelik',
      email: 'fatma.celik@example.com',
      userType: 'User'),
  User(
      id: 5,
      name: 'Emre Şahin',
      email: 'emre.sahin@example.com',
      userType: 'Admin'),
];

class User {
  final int id;
  final String name;
  final String email;
  final String userType;

  User({
    required this.id,
    required this.name,
    required this.email,
    required this.userType,
  });

  Map<String, dynamic> toMap() {
    return {'id': id, 'name': name, 'email': email, 'userType': userType};
  }

  factory User.fromMap(Map<String, dynamic> map) {
    return User(
      id: map['id'],
      name: map['name'],
      email: map['email'],
      userType: map['userType'],
    );
  }
}
