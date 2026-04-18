import 'i_internet_connection.dart';
import 'internet_connection_checker.dart';

IInternetConnection createInternetConnectionImpl() =>
    InternetConnectionWithChecker();
