import '../models/lookup.dart';

abstract class ILookupService {
  Future<List<LookUp>> getGroupLookUp();
  Future<List<LookUp>> getUserLookUp();
  Future<List<LookUp>> getOperationClaimLookUp();
  Future<List<LookUp>> getLanguageLookup();
}
