import 'dart:convert';

import '../../../../di/core_initializer.dart';
import '../../../../services/base_services/api_service.dart';
import '../../../../utilities/results.dart';
import '../models/translate.dart';
import '../models/translate_dto.dart';
import 'i_translate_service.dart';

class ApiTranslateService extends ApiService<Translate>
    implements ITranslateService {
  ApiTranslateService({required super.method});

  @override
  Future<IDataResult<List<TranslateDto>>> getTranslates() async {
    var result = await CoreInitializer().coreContainer.http.get("$url/dtos/");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data = ((result["data"] ?? []) as List)
        .map((e) => e as Map<String, dynamic>)
        .toList();
    return Future.value(SuccessDataResult(
        data.map((e) => TranslateDto.fromMap(e)).toList(), ""));
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> getTranslatesByCode(
      String code) async {
    var result = await CoreInitializer()
        .coreContainer
        .http
        .get("$url/languages/${code}/");
    if (result["success"] != null && result["success"] == false) {
      return Future.value(
          FailureDataResult(result["message"]?.toString() ?? ""));
    }

    // WebAPI uses GetResponseOnlyResultData: body is the dictionary only (no wrapper).
    // Legacy paths may return { "data": { ... } } or { "message": "<json string>" }.
    Map<String, dynamic> map;
    final data = result["data"];
    if (data is Map) {
      map = Map<String, dynamic>.from(data);
    } else {
      final msg = result["message"];
      if (msg is String && msg.isNotEmpty) {
        final decoded = jsonDecode(msg);
        map = Map<String, dynamic>.from(decoded as Map);
      } else {
        map = Map<String, dynamic>.from(result);
        map.remove("success");
        map.remove("message");
        map.remove("data");
      }
    }
    return Future.value(SuccessDataResult(map, ""));
  }
}
