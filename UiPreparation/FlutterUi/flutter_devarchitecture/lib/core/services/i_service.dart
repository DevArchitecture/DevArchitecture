import '../utilities/results.dart';

abstract class IService {
  Future<IDataResult<Map<String, dynamic>>> getById(int id);
  Future<IDataResult<List<Map<String, dynamic>>>> getAll();
  Future<IResult> create(Map<String, dynamic> body);
  Future<IResult> createMany(List<Map<String, dynamic>> body);
  Future<IResult> update(Map<String, dynamic> body);
  Future<IResult> delete(int id);
  Future<IDataResult<Map<String, dynamic>>> getByName(String name);
}
