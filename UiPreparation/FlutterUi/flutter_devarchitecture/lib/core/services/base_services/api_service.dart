import '../../configs/app_config.dart';
import '../../constants/core_messages.dart';
import '../../utilities/results.dart';
import '../../di/core_initializer.dart';
import '../i_service.dart';

abstract class ApiService<T> implements IService {
  late String url;

  init(String method) {
    url = appConfig.apiUrl + method;
  }

  ApiService({required String method}) {
    init(method);
  }

  @override
  Future<IResult> create(Map<String, dynamic> body) async {
    var result = await CoreInitializer().coreContainer.http.post(url, body);
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureResult(result["message"] ?? ""));
      }
    }
    return Future.value(SuccessResult(CoreMessages.customerAddSuccessMessage));
  }

  @override
  Future<IResult> createMany(List<Map<String, dynamic>> maps) async {
    for (var i = 0; i < maps.length; i++) {
      var result =
          await CoreInitializer().coreContainer.http.post(url, maps[i]);
      if (result["success"] != null) {
        if (result["success"] == false) {
          return Future.value(FailureResult("${result["message"]}"));
        }
      }
    }
    return Future.value(SuccessResult(CoreMessages.customerAddSuccessMessage));
  }

  @override
  Future<IResult> delete(int id) async {
    var result = await CoreInitializer().coreContainer.http.delete("$url/$id");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureResult(result["message"] ?? ""));
      }
    }
    return Future.value(
        SuccessResult(CoreMessages.customerDefaultSuccessMessage));
  }

  @override
  Future<IDataResult<List<Map<String, dynamic>>>> getAll() async {
    var result = await CoreInitializer().coreContainer.http.get(url);
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data =
        (result["data"] as List).map((e) => e as Map<String, dynamic>).toList();

    return Future.value(SuccessDataResult(data, ""));
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> getById(int id) async {
    var result = await CoreInitializer().coreContainer.http.get("$url/$id");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data = result["data"] as Map<String, dynamic>;
    return Future.value(SuccessDataResult(data, ""));
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> getByName(String name) async {
    var result =
        await CoreInitializer().coreContainer.http.get("$url/name?name=$name");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    var data = result["data"] as Map<String, dynamic>;
    return Future.value(SuccessDataResult(data, ""));
  }

  @override
  Future<IResult> update(Map<String, dynamic> body) async {
    var result = await CoreInitializer().coreContainer.http.put(url, body);
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureResult(result["message"] ?? ""));
      }
    }
    return Future.value(
        SuccessResult(CoreMessages.customerDefaultSuccessMessage));
  }
}
