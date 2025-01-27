import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/foundation.dart';
import '../constants/core_messages.dart';
import '../di/core_initializer.dart';
import '/core/helpers/exceptions.dart';
import 'http_interceptor.dart';
import 'i_http.dart';

class HttpDart implements IHttp {
  late IHttpInterceptor interceptor;
  static final HttpDart _singleton = HttpDart._internal();

  factory HttpDart(IHttpInterceptor interceptor) {
    _singleton.interceptor = interceptor;
    return _singleton;
  }

  HttpDart._internal();

  bool _retrying = false;

  @override
  Future<Map<String, dynamic>> get(String url) async {
    try {
      var intercepts = await interceptor.interceptJson();
      final headers =
          intercepts.map((key, value) => MapEntry(key, value.toString()));

      final response = await http.get(Uri.parse(url), headers: headers);
      if (kDebugMode) {
        CoreInitializer()
            .coreContainer
            .logger
            .logDebug("$url -> ${response.statusCode}");
      }

      await _handleError(response.statusCode, response.body, url, headers);

      return _processResponse(response.body);
    } catch (e) {
      rethrow;
    }
  }

  @override
  Future<Map<String, dynamic>> post(
      String url, Map<String, dynamic> body) async {
    try {
      var intercepts = await interceptor.interceptJson();
      final headers =
          intercepts.map((key, value) => MapEntry(key, value.toString()));

      final response = await http.post(
        Uri.parse(url),
        headers: {
          ...headers,
        },
        body: jsonEncode(body),
      );

      if (kDebugMode) {
        CoreInitializer()
            .coreContainer
            .logger
            .logDebug("$url -> ${response.statusCode}");
      }

      await _handleError(response.statusCode, response.body, url, headers);

      return _processResponse(response.body);
    } catch (e) {
      rethrow;
    }
  }

  @override
  Future<Map<String, dynamic>> delete(String url) async {
    try {
      var intercepts = await interceptor.interceptJson();
      final headers =
          intercepts.map((key, value) => MapEntry(key, value.toString()));

      final response = await http.delete(Uri.parse(url), headers: headers);

      if (kDebugMode) {
        CoreInitializer()
            .coreContainer
            .logger
            .logDebug("$url -> ${response.statusCode}");
      }

      await _handleError(response.statusCode, response.body, url, headers);

      return _processResponse(response.body);
    } catch (e) {
      rethrow;
    }
  }

  @override
  Future<Map<String, dynamic>> put(
      String url, Map<String, dynamic> body) async {
    try {
      var intercepts = await interceptor.interceptJson();
      final headers =
          intercepts.map((key, value) => MapEntry(key, value.toString()));

      final response = await http.put(
        Uri.parse(url),
        headers: {
          ...headers,
        },
        body: jsonEncode(body),
      );

      if (kDebugMode) {
        CoreInitializer()
            .coreContainer
            .logger
            .logDebug("$url -> ${response.statusCode}");
      }

      await _handleError(response.statusCode, response.body, url, headers);

      return _processResponse(response.body);
    } catch (e) {
      rethrow;
    }
  }

  Future<void> _handleError(int statusCode, String reply, String url,
      Map<String, dynamic> headers) async {
    if (statusCode == 400) {
      throw BadRequestException(reply);
    }

    if (statusCode == 401) {
      if (_retrying) {
        throw UnauthorizedException(
          CoreMessages.unauthorizedErrorMessage,
        );
      }

      _retrying = true;

      try {
        await interceptor.handle401(url, headers);
      } catch (e) {
        _retrying = false;
        throw UnauthorizedException(
          CoreMessages.unauthorizedErrorMessage,
        );
      }

      _retrying = false;
      return;
    }

    if (statusCode == 403) {
      throw ForbiddenException(reply);
    }

    if (statusCode == 500) {
      throw InternalServerErrorException(reply);
    }
  }

  Map<String, dynamic> _processResponse(String responseBody) {
    final decodedJson = jsonDecode(responseBody);
    if (decodedJson is String) {
      return {"message": decodedJson};
    }
    if (decodedJson is Map<String, dynamic>) {
      return decodedJson;
    } else if (decodedJson is List) {
      List<Map<String, dynamic>> list =
          decodedJson.map((e) => e as Map<String, dynamic>).toList();
      return {"data": list};
    }
    return {"response": decodedJson};
  }
}
