import 'package:dart_amqp/dart_amqp.dart';
import 'package:flutter_devarchitecture/core/widgets/tables/data_table/data_table_2.dart';
import '../../key_value_storage/shared_pref/shared_preferences.dart';
import '/core/utilities/file_share/i_share.dart';
import '/core/utilities/logger/i_logger.dart';
import '/core/utilities/permission_handler/i_permission_handler.dart';
import '/core/utilities/permission_handler/permission_handler.dart';
import '/core/widgets/animations/lottie/lottie_interaction_animation_asset.dart';
import '/core/widgets/animations/lottie/lottie_status_animation_asset.dart';
import '/core/widgets/map/i_map.dart';
import '../../utilities/battery_state_management/battery_state_plus.dart';
import '../../utilities/battery_state_management/i_battery_state.dart';
import '../../utilities/biometric_auth/i_biometric_auth.dart';
import '../../utilities/biometric_auth/local_auth_service.dart';
import '../../utilities/download_management/csv_download.dart';
import '../../utilities/download_management/excel_download.dart';
import '../../utilities/download_management/i_download.dart';
import '../../utilities/download_management/image_download.dart';
import '../../utilities/download_management/json_download.dart';
import '../../utilities/download_management/pdf_download.dart';
import '../../utilities/download_management/txt_download.dart';
import '../../utilities/download_management/xml_download.dart';
import '../../utilities/file_share/csv_share.dart';
import '../../utilities/file_share/excel_share.dart';
import '../../utilities/file_share/image_share.dart';
import '../../utilities/file_share/json_share.dart';
import '../../utilities/file_share/pdf_share.dart';
import '../../utilities/file_share/txt_share.dart';
import '../../utilities/file_share/xml_share.dart';
import '../../utilities/logger/logger.dart';
import '../../utilities/qr_code/i_qr_code_scanner_service.dart';
import '../../utilities/qr_code/qr_code_scanner_service.dart';
import '../../utilities/screen_message/i_screen_message.dart';
import '../../widgets/animations/i_page_animation_asset.dart';
import '../../widgets/animations/i_interaction_animation_asset.dart';
import '../../widgets/animations/lottie/lottie_page_animation_asset.dart';
import '../../widgets/map/google_map.dart';
import '/core/utilities/internet_connection/internet_connection_checker.dart';
import '../../utilities/device_information_management/device_info_plus.dart';
import '../../utilities/device_information_management/i_device_information.dart';
import '/core/utilities/message_broker/i_message_broker.dart';
import '../../utilities/local_notification/i_notification_service.dart';
import '../../utilities/local_notification/local_notification_service.dart';
import '../../widgets/animations/i_status_animation_asset.dart';
import '/core/widgets/charts/graphic/graphic_analytics_chart.dart';
import '/core/widgets/charts/graphic/graphic_basic_chart.dart';
import '/core/widgets/charts/graphic/graphic_event_chart.dart';
import 'package:get_it/get_it.dart';
import '../../utilities/internet_connection/i_internet_connection.dart';
import '../../utilities/message_broker/rabbitmq_broker.dart';
import '../../utilities/screen_message/ok_toast_screen_message.dart';
import '../../http/http_dart.dart';
import '../../http/http_interceptor.dart';
import '../../http/i_http.dart';
import '../../key_value_storage/i_key_value_storage.dart';
import '../../widgets/charts/geekyants/geekyants_gauges_chart.dart';
import '../../widgets/charts/i_chart.dart';
import '../../widgets/tables/i_tables.dart';
import '../i_core_container.dart';

class GetItCoreContainer implements ICoreContainer {
  late GetIt _getIt;
  void init() {
    _getIt = GetIt.instance;
    setUp();
  }

  GetItCoreContainer() {
    init();
  }

  @override
  late IHttp http;

  @override
  late IScreenMessage screenMessage;

  @override
  late IKeyValueStorage storage;

  @override
  late IHttpInterceptor httpInterceptor;

  @override
  late IBasicChart basicChart;

  @override
  late IAnalyticsChart analyticsChart;

  @override
  late IEventStreamChart eventStreamChart;

  @override
  late ITables dataTable;

  @override
  late IGaugesChart gaugesChart;

  @override
  late IDeviceInformation deviceInformation;

  @override
  late INotificationService notificationService;

  @override
  late IMessageBroker messageBroker;

  @override
  late IInternetConnection internetConnection;

  @override
  late IBatteryState batteryState;

  @override
  late IPermissionHandler permissionHandler;

  @override
  late ILogger logger;

  @override
  late IMap map;

  // Downloads
  @override
  late IPdfDownload pdfDownload;
  @override
  late IExcelDownload excelDownload;
  @override
  late ITxtDownload txtDownload;
  @override
  late IJsonDownload jsonDownload;
  @override
  late IXmlDownload xmlDownload;
  @override
  late IImageDownload imageDownload;
  @override
  late ICsvDownload csvDownload;

  // Shares
  @override
  late IPdfShare pdfShare;
  @override
  late IExcelShare excelShare;
  @override
  late ITxtShare txtShare;
  @override
  late IJsonShare jsonShare;
  @override
  late IXmlShare xmlShare;
  @override
  late IImageShare imageShare;
  @override
  late ICsvShare csvShare;

  // Animations
  @override
  late IPageAnimationAsset pageAnimationAsset;
  @override
  late IInteractionAnimationAsset interactionAnimationAsset;
  @override
  late IStatusAnimationAsset statusAnimationAsset;

  // Utilities -> qr code
  @override
  late IQRCodeScannerService qrCodeScannerService;

  // Utilities -> biometric auth
  @override
  late IBiometricAuth biometricAuth;

  @override
  setUp() {
    checkIfUnRegistered<ITables>((() {
      dataTable = _getIt.registerSingleton<ITables>(DataTables());
    }));

    checkIfUnRegistered<IScreenMessage>((() {
      screenMessage =
          _getIt.registerSingleton<IScreenMessage>(OkToastScreenMessage());
    }));

    checkIfUnRegistered<IKeyValueStorage>((() {
      storage = _getIt
          .registerSingleton<IKeyValueStorage>(SharedPreferencesLocalStorage());
    }));

    checkIfUnRegistered<IHttpInterceptor>((() {
      httpInterceptor =
          _getIt.registerSingleton<IHttpInterceptor>(HttpInterceptor());
    }));

    checkIfUnRegistered<IHttp>((() {
      http = _getIt.registerSingleton<IHttp>(HttpDart(httpInterceptor));
    }));

    //! charts
    checkIfUnRegistered<IBasicChart>((() {
      basicChart = _getIt.registerSingleton<IBasicChart>(GraphicBasicChart());
    }));

    checkIfUnRegistered<IAnalyticsChart>((() {
      analyticsChart =
          _getIt.registerSingleton<IAnalyticsChart>(GraphicAnalyticsChart());
    }));

    checkIfUnRegistered<IEventStreamChart>((() {
      eventStreamChart = _getIt
          .registerSingleton<IEventStreamChart>(GraphicEventStreamChart());
    }));

    checkIfUnRegistered<IGaugesChart>((() {
      gaugesChart =
          _getIt.registerSingleton<IGaugesChart>(GeekyantsGaugesChart());
    }));

    //! Utilities
    checkIfUnRegistered<IDeviceInformation>((() {
      deviceInformation =
          _getIt.registerSingleton<IDeviceInformation>(DeviceInfoPlus());
    }));

    checkIfUnRegistered<INotificationService>((() {
      notificationService = _getIt
          .registerSingleton<INotificationService>(LocalNotificationService());
    }));

    checkIfUnRegistered<IMessageBroker>((() {
      messageBroker =
          _getIt.registerSingleton<IMessageBroker>(RabbitMQMessageBroker(
              "default_queue",
              ConnectionSettings(
                host: "localhost",
                authProvider: PlainAuthenticator("guest", "guest"),
              )));
    }));

    checkIfUnRegistered<IInternetConnection>((() {
      internetConnection = _getIt.registerSingleton<IInternetConnection>(
          InternetConnectionWithChecker());
    }));

    checkIfUnRegistered<IBatteryState>((() {
      batteryState =
          _getIt.registerSingleton<IBatteryState>(BatteryStateBatteryPlus());
    }));

    checkIfUnRegistered<IPermissionHandler>((() {
      permissionHandler =
          _getIt.registerSingleton<IPermissionHandler>(PermissionHandler());
    }));

    checkIfUnRegistered<ILogger>((() {
      logger = _getIt.registerSingleton<ILogger>(LoggerImpl());
    }));

    //! download
    checkIfUnRegistered<IPdfDownload>(() {
      pdfDownload = _getIt.registerSingleton<IPdfDownload>(PdfDownload());
    });

    checkIfUnRegistered<IExcelDownload>(() {
      excelDownload = _getIt.registerSingleton<IExcelDownload>(ExcelDownload());
    });

    checkIfUnRegistered<ITxtDownload>(() {
      txtDownload = _getIt.registerSingleton<ITxtDownload>(TxtDownload());
    });

    checkIfUnRegistered<IJsonDownload>(() {
      jsonDownload = _getIt.registerSingleton<IJsonDownload>(JsonDownload());
    });

    checkIfUnRegistered<IXmlDownload>(() {
      xmlDownload = _getIt.registerSingleton<IXmlDownload>(XmlDownload());
    });

    checkIfUnRegistered<IImageDownload>(() {
      imageDownload = _getIt.registerSingleton<IImageDownload>(ImageDownload());
    });
    checkIfUnRegistered<ICsvDownload>(() {
      csvDownload = _getIt.registerSingleton<ICsvDownload>(CsvDownload());
    });

    //! map
    checkIfUnRegistered<IMap>(() {
      map = _getIt.registerSingleton<IMap>(MapGoogle());
    });

    //! file share
    checkIfUnRegistered<IPdfShare>(() {
      pdfShare = _getIt.registerSingleton<IPdfShare>(PdfShare());
    });

    checkIfUnRegistered<IExcelShare>(() {
      excelShare = _getIt.registerSingleton<IExcelShare>(ExcelShare());
    });

    checkIfUnRegistered<ITxtShare>(() {
      txtShare = _getIt.registerSingleton<ITxtShare>(TxtShare());
    });

    checkIfUnRegistered<IJsonShare>(() {
      jsonShare = _getIt.registerSingleton<IJsonShare>(JsonShare());
    });

    checkIfUnRegistered<IXmlShare>(() {
      xmlShare = _getIt.registerSingleton<IXmlShare>(XmlShare());
    });

    checkIfUnRegistered<IImageShare>(() {
      imageShare = _getIt.registerSingleton<IImageShare>(ImageShare());
    });

    checkIfUnRegistered<ICsvShare>(() {
      csvShare = _getIt.registerSingleton<ICsvShare>(CsvShare());
    });

    //!animation assets
    checkIfUnRegistered<IPageAnimationAsset>((() {
      pageAnimationAsset = _getIt
          .registerSingleton<IPageAnimationAsset>(LottiePageAnimationAsset());
    }));

    checkIfUnRegistered<IInteractionAnimationAsset>((() {
      interactionAnimationAsset =
          _getIt.registerSingleton<IInteractionAnimationAsset>(
              LottieInteractionAnimationAsset());
    }));

    checkIfUnRegistered<IStatusAnimationAsset>((() {
      statusAnimationAsset = _getIt.registerSingleton<IStatusAnimationAsset>(
          LottieStatusAnimationAsset());
    }));

    //! qrCode Scanner
    checkIfUnRegistered<IQRCodeScannerService>((() {
      qrCodeScannerService = _getIt
          .registerSingleton<IQRCodeScannerService>(QRCodeScannerService());
    }));

    //! Biometric auth
    checkIfUnRegistered<IBiometricAuth>((() {
      biometricAuth =
          _getIt.registerSingleton<IBiometricAuth>(LocalAuthService());
    }));
  }

  @override
  void checkIfUnRegistered<T extends Object>(Function register) {
    if (!_getIt.isRegistered<T>()) {
      register.call();
    }
  }
}
