abstract class IKeyValueStorage {
  Future<void> save(String key, String value) async {}
  Future<String?> read(String key) async {
    return null;
  }

  Future<void> delete(String key) async {}
  Future<void> deleteAll() async {}
  Future<List<Map<String, String>>> readAll();

  Future<bool> containsKey(String key) async {
    return false;
  }

  init();
}
