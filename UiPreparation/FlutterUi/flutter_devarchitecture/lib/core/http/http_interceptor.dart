import 'package:flutter_devarchitecture/core/constants/core_messages.dart';
import 'package:flutter_devarchitecture/di/business_initializer.dart';

import '../di/core_initializer.dart';
import '../helpers/exceptions.dart';

abstract class IHttpInterceptor {
  Future<Map<String, dynamic>> interceptJson();
  Future<Map<String, dynamic>> interceptXml();
  Future<void> handle401(String url, Map<String, dynamic> headers);
}

class HttpInterceptor implements IHttpInterceptor {
  @override
  Future<Map<String, dynamic>> interceptJson() async {
    try {
      return {
        "content-type": "application/json",
        "Accept": "application/json",
        "Authorization":
            "Bearer ${await CoreInitializer().coreContainer.storage.read("token")}"
      };
    } catch (e) {
      return {};
    }
  }

  @override
  Future<Map<String, dynamic>> interceptXml() async {
    return {
      "content-type": "application/xml",
      "Accept": "application/xml",
      "Authorization":
          "Bearer ${await CoreInitializer().coreContainer.storage.read("token")}"
    };
  }

  @override
  Future<void> handle401(String url, Map<String, dynamic> headers) async {
    var refreshToken =
        await CoreInitializer().coreContainer.storage.read("refreshToken");

    if (refreshToken == null || refreshToken.isEmpty) {
      throw UnauthorizedException(
        CoreMessages.unauthorizedErrorMessage,
      );
    }

    await BusinessInitializer().businessContainer.authService.refreshToken();
    headers["Authorization"] =
        "Bearer ${await CoreInitializer().coreContainer.storage.read("token")}";
  }
}
