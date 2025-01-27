import 'package:flutter/foundation.dart';
import '/core/constants/core_messages.dart';
import '/core/di/core_initializer.dart';
import 'package:sqlite3/sqlite3.dart';
import '../../utilities/results.dart';
import '../../../core/services/i_service.dart';
import '../../models/i_entity.dart';

class LocalStorageService<T extends IEntity> implements IService {
  late final Database _database;
  final String tableName;

  LocalStorageService(this.tableName) {
    _database = sqlite3.open('local_storage.db');
  }

  Future<void> _ensureTableExists(Map<String, dynamic> body) async {
    final columns = body.entries.map((entry) {
      final columnName = entry.key;
      final columnType = _mapToSQLiteType(entry.value);
      return '$columnName $columnType';
    }).join(', ');

    final createTableQuery = '''
      CREATE TABLE IF NOT EXISTS $tableName (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        $columns
      )
    ''';

    _database.execute(createTableQuery);
    CoreInitializer()
        .coreContainer
        .logger
        .logDebug('Tablo oluşturuldu: $tableName');
  }

  String _mapToSQLiteType(dynamic value) {
    if (value is int) {
      return 'INTEGER';
    } else if (value is double) {
      return 'REAL';
    } else if (value is String) {
      return 'TEXT';
    } else if (value is bool) {
      return 'INTEGER';
    } else if (value is DateTime) {
      return 'TEXT';
    } else if (value is List || value is Map) {
      return 'TEXT';
    } else {
      return 'TEXT';
    }
  }

  Map<String, dynamic> _rowToMap(Row row, List<String> columnNames) {
    final map = <String, dynamic>{};
    for (var i = 0; i < columnNames.length; i++) {
      map[columnNames[i]] = row[i];
    }
    return map;
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> getById(int id) async {
    try {
      final result =
          _database.select('SELECT * FROM $tableName WHERE id = ?', [id]);
      if (result.isNotEmpty) {
        final row = result.first;
        return SuccessDataResult(
            _rowToMap(row, result.columnNames), CoreMessages.defaultSuccess);
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    } catch (e) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logError(e.toString());
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    }
  }

  @override
  Future<IDataResult<List<Map<String, dynamic>>>> getAll() async {
    try {
      final results = _database.select('SELECT * FROM $tableName');
      final data =
          results.map((row) => _rowToMap(row, results.columnNames)).toList();
      return SuccessDataResult(data, CoreMessages.defaultSuccess);
    } catch (e) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logError(e.toString());
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    }
  }

  @override
  Future<IResult> create(Map<String, dynamic> body) async {
    try {
      await _ensureTableExists(body);

      final columns = body.keys.join(', ');
      final placeholders = body.keys.map((_) => '?').join(', ');

      final query = '''
        INSERT INTO $tableName ($columns) VALUES ($placeholders)
      ''';

      _database.execute(query, body.values.toList());
      return SuccessResult(CoreMessages.customerAddSuccessMessage);
    } catch (e) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logError(e.toString());
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    }
  }

  @override
  Future<IResult> createMany(List<Map<String, dynamic>> bodyList) async {
    try {
      if (bodyList.isEmpty) {
        return FailureResult(CoreMessages.cantBeEmpty);
      }

      await _ensureTableExists(bodyList.first);

      final columns = bodyList.first.keys.join(', ');
      final placeholders = bodyList.first.keys.map((_) => '?').join(', ');

      final statement = _database
          .prepare('INSERT INTO $tableName ($columns) VALUES ($placeholders)');

      for (var body in bodyList) {
        statement.execute(body.values.toList());
      }
      statement.dispose();

      return SuccessResult(CoreMessages.customerDefaultSuccessMessage);
    } catch (e) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logError(e.toString());
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    }
  }

  @override
  Future<IResult> update(Map<String, dynamic> body) async {
    try {
      final updates =
          body.entries.map((entry) => '${entry.key} = ?').join(', ');

      final query = '''
        UPDATE $tableName SET $updates WHERE id = ?
      ''';

      _database.execute(query, [...body.values, body["id"]]);
      return SuccessResult(CoreMessages.customerDefaultSuccessMessage);
    } catch (e) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logError(e.toString());
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    }
  }

  @override
  Future<IResult> delete(int id) async {
    try {
      _database.execute('DELETE FROM $tableName WHERE id = ?', [id]);
      return SuccessResult(CoreMessages.customerDefaultSuccessMessage);
    } catch (e) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logError(e.toString());
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    }
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> getByName(String name) async {
    try {
      final result =
          _database.select('SELECT * FROM $tableName WHERE name = ?', [name]);
      if (result.isNotEmpty) {
        final row = result.first;
        return SuccessDataResult(
            _rowToMap(row, result.columnNames), CoreMessages.defaultSuccess);
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    } catch (e) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logError(e.toString());
      }
      return FailureDataResult(CoreMessages.customerDefaultErrorMessage);
    }
  }

  void close() {
    _database.dispose();
  }
}
