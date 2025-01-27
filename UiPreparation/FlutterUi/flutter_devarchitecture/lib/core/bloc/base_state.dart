abstract class BaseState {
  const BaseState();
}

class BlocInitial extends BaseState {
  const BlocInitial();
}

class BlocLoading extends BaseState {
  final String? message;
  BlocLoading([this.message]);
}

class BlocChecking extends BlocLoading {
  BlocChecking([String? message]) : super(message);
}

class BlocSending extends BlocLoading {
  BlocSending([String? message]) : super(message);
}

class BlocFailed extends BaseState {
  final int statusCode;
  final String message;

  BlocFailed(this.statusCode, this.message);

  factory BlocFailed.fromJson(Map<String, dynamic> json) {
    return BlocFailed(json['statusCode'], json['message']);
  }
}

class BlocSuccess<T> extends BaseState {
  final T? result;
  final String? message;

  const BlocSuccess(this.result, {this.message});
}

class BlocAdded extends BlocSuccess {
  const BlocAdded({String? message}) : super(null, message: message);
}

class BlocUpdated extends BlocSuccess {
  const BlocUpdated({String? message}) : super(null, message: message);
}

class BlocDeleted extends BlocSuccess {
  const BlocDeleted({String? message}) : super(null, message: message);
}
