import '/core/services/i_service.dart';
import '/core/utilities/results.dart';

import '../../lookups/models/lookup.dart';

abstract class ILanguageService extends IService {
  Future<IDataResult<List<LookUp>>> getLanguageCodes(); // id is language code
  Future<IDataResult<List<LookUp>>> getLanguageLookups(); // id is number
}
