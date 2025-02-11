import '/core/utilities/file_share/i_share.dart';
import '/core/widgets/animations/i_page_animation_asset.dart';
import '/core/widgets/animations/i_interaction_animation_asset.dart';
import '/core/widgets/animations/i_status_animation_asset.dart';
import '../utilities/battery_state_management/i_battery_state.dart';
import '../utilities/biometric_auth/i_biometric_auth.dart';
import '../utilities/download_management/i_download.dart';
import '../utilities/internet_connection/i_internet_connection.dart';
import '../utilities/device_information_management/i_device_information.dart';
import '../http/http_interceptor.dart';
import '../utilities/logger/i_logger.dart';
import '../utilities/message_broker/i_message_broker.dart';
import '../utilities/local_notification/i_notification_service.dart';
import '../utilities/permission_handler/i_permission_handler.dart';
import '../key_value_storage/i_key_value_storage.dart';
import '../http/i_http.dart';
import '../utilities/qr_code/i_qr_code_scanner_service.dart';
import '../utilities/screen_message/i_screen_message.dart';
import '../widgets/charts/i_chart.dart';
import '../widgets/map/i_map.dart';
import '../widgets/tables/i_tables.dart';

abstract class ICoreContainer {
  // Storage
  late IKeyValueStorage storage;

  // http
  late IHttp http;
  late IHttpInterceptor httpInterceptor;

  // animations
  late IPageAnimationAsset pageAnimationAsset;
  late IInteractionAnimationAsset interactionAnimationAsset;
  late IStatusAnimationAsset statusAnimationAsset;

  // widgets
  late ITables dataTable;
  late IMap map;

  // charts
  late IBasicChart basicChart;
  late IGaugesChart gaugesChart;
  late IAnalyticsChart analyticsChart;
  late IEventStreamChart eventStreamChart;

  // debug
  late ILogger logger;

  // utilities
  late IScreenMessage screenMessage;
  late IDeviceInformation deviceInformation;
  late INotificationService notificationService;
  late IMessageBroker messageBroker;
  late IInternetConnection internetConnection;
  late IBatteryState batteryState;
  late IPermissionHandler permissionHandler;

  // utilities -> download
  late IPdfDownload pdfDownload;
  late IExcelDownload excelDownload;
  late ITxtDownload txtDownload;
  late IJsonDownload jsonDownload;
  late IXmlDownload xmlDownload;
  late IImageDownload imageDownload;
  late ICsvDownload csvDownload;

  // utilities -> file share
  late IPdfShare pdfShare;
  late IExcelShare excelShare;
  late ITxtShare txtShare;
  late IImageShare imageShare;
  late IJsonShare jsonShare;
  late IXmlShare xmlShare;
  late ICsvShare csvShare;

  // utilities -> qr code
  late IQRCodeScannerService qrCodeScannerService;

  // utilities -> biometric auth
  late IBiometricAuth biometricAuth;

  // get it
  setUp();
  void checkIfUnRegistered<T extends Object>(Function register);
}
