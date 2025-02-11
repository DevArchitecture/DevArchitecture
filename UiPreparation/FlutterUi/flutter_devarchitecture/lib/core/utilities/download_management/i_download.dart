abstract class IDownload {
  Future<void> download(List<Map<String, dynamic>> data);
}

abstract class IPdfDownload extends IDownload {}

abstract class IExcelDownload extends IDownload {}

abstract class ITxtDownload extends IDownload {}

abstract class IJsonDownload extends IDownload {}

abstract class IXmlDownload extends IDownload {}

abstract class IImageDownload extends IDownload {}

abstract class ICsvDownload extends IDownload {}
