import 'get_it/get_it_firebase_di.dart';
import 'i_firebase_container.dart';

class FirebaseInitializer {
  late IFirebaseContainer firebaseContainer;

  static final FirebaseInitializer _singleton = FirebaseInitializer._internal();

  factory FirebaseInitializer() {
    return _singleton;
  }

  FirebaseInitializer._internal() {
    _init();
  }

  void _init() {
    firebaseContainer = GetItFirebaseContainer();
  }
}
