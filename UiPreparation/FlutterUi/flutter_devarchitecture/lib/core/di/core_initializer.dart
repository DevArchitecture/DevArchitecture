import 'get_it/get_it_core_di.dart';

import 'i_core_container.dart';

class CoreInitializer {
  late ICoreContainer coreContainer;

  static final CoreInitializer _singleton = CoreInitializer._internal();

  factory CoreInitializer() {
    return _singleton;
  }

  CoreInitializer._internal() {
    _init();
  }

  void _init() {
    coreContainer = GetItCoreContainer();
  }
}
