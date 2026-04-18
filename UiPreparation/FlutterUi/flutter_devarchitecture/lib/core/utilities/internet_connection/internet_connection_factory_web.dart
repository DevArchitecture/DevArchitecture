import 'i_internet_connection.dart';
import 'noop_internet_connection.dart';

IInternetConnection createInternetConnectionImpl() => NoopInternetConnection();
