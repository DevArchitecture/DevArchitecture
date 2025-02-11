abstract class IShare {
  Future<void> share(List<Map<String, dynamic>> data);
}

abstract class IPdfShare extends IShare {}

abstract class IExcelShare extends IShare {}

abstract class ITxtShare extends IShare {}

abstract class IJsonShare extends IShare {}

abstract class IXmlShare extends IShare {}

abstract class IImageShare extends IShare {}

abstract class ICsvShare extends IShare {}
