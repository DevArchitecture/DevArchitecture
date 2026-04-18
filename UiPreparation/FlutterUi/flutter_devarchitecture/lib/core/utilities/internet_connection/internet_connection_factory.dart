import 'i_internet_connection.dart';
import 'internet_connection_factory_stub.dart'
    if (dart.library.html) 'internet_connection_factory_web.dart'
    if (dart.library.io) 'internet_connection_factory_io.dart';

IInternetConnection createInternetConnection() => createInternetConnectionImpl();
