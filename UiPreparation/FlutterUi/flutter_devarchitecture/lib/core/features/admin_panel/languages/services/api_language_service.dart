import '../../../../di/core_initializer.dart';
import '../../../../services/base_services/api_service.dart';
import '../../../../utilities/results.dart';
import '../../lookups/models/lookup.dart';
import '../models/language.dart';
import 'i_language_service.dart';

class ApiLanguageService extends ApiService<Language>
    implements ILanguageService {
  ApiLanguageService({required super.method});

  @override
  Future<IDataResult<List<LookUp>>> getLanguageCodes() async {
    var result =
        await CoreInitializer().coreContainer.http.get(url + "/codes/");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data =
        (result["data"] as List).map((e) => e as Map<String, dynamic>).toList();

    return Future.value(
        SuccessDataResult(data.map((e) => LookUp.fromMap(e)).toList(), ""));
  }

  @override
  Future<IDataResult<List<LookUp>>> getLanguageLookups() async {
    var result =
        await CoreInitializer().coreContainer.http.get(url + "/lookups/");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data =
        (result["data"] as List).map((e) => e as Map<String, dynamic>).toList();

    return Future.value(
        SuccessDataResult(data.map((e) => LookUp.fromMap(e)).toList(), ""));
  }
}
