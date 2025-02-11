// bad request exception
class BadRequestException implements Exception {
  final String message;
  BadRequestException(this.message);
}

// unauthorized exception
class UnauthorizedException implements Exception {
  final String message;
  UnauthorizedException(this.message);
}

// forbidden exception
class ForbiddenException implements Exception {
  final String message;
  ForbiddenException(this.message);
}

// not found exception
class NotFoundException implements Exception {
  final String message;
  NotFoundException(this.message);
}

// Server exception
class InternalServerErrorException implements Exception {
  final String message;
  InternalServerErrorException(this.message);
}
