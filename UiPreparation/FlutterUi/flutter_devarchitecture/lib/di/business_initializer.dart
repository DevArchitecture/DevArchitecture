import 'i_business_container.dart';
import 'get_it/get_it_business_di.dart';

class BusinessInitializer {
  late IBusinessContainer businessContainer;

  static final BusinessInitializer _singleton = BusinessInitializer._internal();

  factory BusinessInitializer() {
    return _singleton;
  }

  BusinessInitializer._internal() {
    _init();
  }

  void _init() {
    businessContainer = GetItBusinessContainer();
  }
}
