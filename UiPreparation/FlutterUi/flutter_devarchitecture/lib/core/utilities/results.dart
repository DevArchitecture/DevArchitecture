abstract class IDataResult<T> extends IResult {
  T? get data;
}

abstract class IResult {
  bool get isSuccess;
  String get message;
}

class SuccessDataResult<T> extends IDataResult<T> {
  final T? _data;
  final String _message;

  @override
  T? get data => this._data;
  @override
  bool get isSuccess => true;
  @override
  String get message => this._message;

  SuccessDataResult(this._data, this._message);
}

class FailureDataResult<T, E> extends IDataResult<T> {
  final String _message;

  @override
  T? get data => null;
  @override
  bool get isSuccess => false;
  @override
  @override
  String get message => this._message;

  FailureDataResult(this._message);
}

class SuccessResult extends IResult {
  final String _message;

  @override
  bool get isSuccess => true;

  @override
  String get message => _message;

  SuccessResult(this._message);
}

class FailureResult extends IResult {
  final String _message;

  @override
  bool get isSuccess => false;

  @override
  String get message => _message;

  FailureResult(this._message);
}
