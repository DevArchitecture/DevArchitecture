
# Yeni Özellik Ekleme Kılavuzu: Ürün Yönetimi

Bu kılavuz, "Ürün" yönetimi özelliğini örnek alarak projeye yeni bir özellik ekleme sürecini göstermektedir.

## 1. Modeli Oluşturma

Dosya: models/product.dart

```dart
import '/core/models/i_entity.dart';

class Product implements IEntity {
  @override
  int id;
  String productName;

  Product({required this.id, required this.productName});

  @override
  Map<String, dynamic> toMap() {
    return {"id": id, "productName": productName};
  }

  factory Product.fromMap(Map<String, dynamic> map) {
    return Product(id: map["id"], productName: map["productName"]);
  }
}
```

## 2. Servis Arayüzünü Tanımlama

Dosya: services/i_product_service.dart

```dart
import '/core/services/i_service.dart';

abstract class IProductService extends IService {}
```

## 3. Servisi Uygulama

Dosya: services/api_product_service.dart

```dart
import '../../../../services/base_services/api_service.dart';
import '../models/product.dart';
import 'i_product.dart';

class ApiProductService extends ApiService<Product> implements IProductService {
  ApiProductService({required super.method});
}
```

## 4. Cubit Oluşturma

Dosya: bloc/product_cubit.dart

```dart
import '../../../../bloc/base_cubit.dart';
import '../../../../../di/business_initializer.dart';
import '../models/product.dart';

class ProductCubit extends BaseCubit<Product> {
  ProductCubit() : super() {
    super.service = BusinessInitializer().businessContainer.productService;
  }
}
```

## 5. Business Container'ı Güncelleme

Dosya: di/business_container.dart

```dart
abstract class IBusinessContainer {
  late IProductService productService;
}

class GetItBusinessContainer implements IBusinessContainer {
  // ... mevcut kod ...

  @override
  void setup() {

    if (appConfig.name == 'dev' || appConfig.name == '') {
        // ... mevcut kurulum kodu ...

      checkIfUnRegistered<IProductService>((() {
        productService = _getIt.registerSingleton<IProductService>(
            ApiProductService(method: "/Products"));
      }));
    }

    if (appConfig.name == 'staging') {

        // ... mevcut kurulum kodu ...
      checkIfUnRegistered<IProductService>((() {
        productService = _getIt.registerSingleton<IProductService>(
            ApiProductService(method: "/Products"));
      }));
    }

    if (appConfig.name == 'prod') {

        // ... mevcut kurulum kodu ...
      checkIfUnRegistered<IProductService>((() {
        productService = _getIt.registerSingleton<IProductService>(
            ApiProductService(method: "/Products"));
      }));
    }
  }
}
```

## 6. Kullanıcı Arayüzünü Oluşturma

Dosya: pages/admin_product_page.dart

```dart
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
// ... diğer importlar ...

class AdminProductPage extends StatelessWidget {
  const AdminProductPage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => ProductCubit(),
      child: ExtendedBlocConsumer<ProductCubit, BaseState>(
        builder: (context, state) {
          // ... duruma göre UI oluştur ...
        },
      ),
    );
  }

  // ... diğer UI bileşenleri için metodlar ...
}
```

## 7. Rotaları Ekleme

Dosya: routes/app_route_module.dart

```dart
class AppRouteModule extends Module {
  @override
  void routes(r) {
    // ... mevcut rotalar ...

    r.child(RoutesConstants.adminProductPage,
        child: (context) => const AdminProductPage(),
        transition: transition,
        guards: [
          ModularAuthGuard(),
          ModularClaimGuard(claim: "GetProductsQuery")
        ]);

    // ... diğer rotalar ...
  }
}

// RoutesConstants'ı Güncelleme
class RoutesConstants {
  // ... mevcut sabitler ...
  static const String adminProductPage = '/admin/products';
  // ... diğer sabitler ...
}
```

## 8. Navigasyonu Güncelleme

Dosya: widgets/nav_bar.dart

```dart
class _NavBarState extends State<NavBar> {
  // ... mevcut kod ...

  Future<void> _loadMenuItems() async {
    adminPanelMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.apps,
      SidebarConstants.adminPanelPageTitle,
      [
        // ... mevcut menü öğeleri ...
        {
          "name": SidebarConstants.adminPanelPageProductTitle,
          "route": RoutesConstants.adminProductPage,
          "icon": Icons.inventory,
          "guard": 'GetProductsQuery',
        },
        // ... diğer menü öğeleri ...
      ],
    );
    // ... diğer menü konfigürasyonları ...
  }

  // ... sınıfın geri kalanı ...
}
```

## 9. İhtiyaca göre Constantlar oluşturma

Özellik için gerekli olan sabit metinleri ve mesajları tanımlamak için constant sınıfları oluşturun. Bu, çoklu dil desteği ve merkezi metin yönetimi için önemlidir.

Mesajlar için Constant Sınıfı
Dosya: product_constants/product_messages.dart

```dart
import 'package:flutter/material.dart';
import '../../../../constants/base_constants.dart';

class ProductMessages implements ScreenConstantsBase {
  static late String productInfoHover;

  static void init(BuildContext context) {
    BaseConstants.init(context);

    productInfoHover = BaseConstants.translate("ProductInfoHover");
  }
}
```

Ekran Metinleri için Constant Sınıfı
Dosya: product_constants/product_screen_texts.dart

```dart

dart
CopyInsert
import 'package:flutter/material.dart';
import '../../../../constants/base_constants.dart';

class ProductScreenTexts implements ScreenConstantsBase {
  static late String productList;
  static late String productName;
  static late String addProduct;
  static late String productNameHint;
  static late String updateProduct;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    productList = BaseConstants.translate("ProductList");
    productName = BaseConstants.translate("ProductName");
    productNameHint = BaseConstants.translate("ProductNameHint");
    addProduct = BaseConstants.translate("AddProduct");
    updateProduct = BaseConstants.translate("UpdateProduct");
  }
}
```

Constants Initializer'ı Güncelleme
Dosya: constants_initializer.dart

```dart
import 'package:flutter/material.dart';
// ... diğer importlar ...
import '../core/features/admin_panel/products/product_constants/product_messages.dart';
import '../core/features/admin_panel/products/product_constants/product_screen_texts.dart';

class ConstantsInitializer {
  static final ConstantsInitializer _inst = ConstantsInitializer._internal();

  ConstantsInitializer._internal();

  factory ConstantsInitializer(BuildContext context) {
    _inst._init(context);
    return _inst;
  }

  void _init(BuildContext context) {
    // ... diğer constant initializations ...

    // Ürün yönetimi için constant'ları initialize et
    ProductMessages.init(context);
    ProductScreenTexts.init(context);

    // ... diğer constant initializations ...
  }
}
```

## 10. Özellik Mantığını Uygulama

ProductCubit içinde ürünler için CRUD işlemlerini ele almak için gerekli mantığı ekleyin. getAll(), add(), update() ve delete() gibi metodları uygulayın.

## 11. Test Etme

Product modeli, ApiProductService ve ProductCubit için birim testleri oluşturun. Ürün yönetimi özelliği için entegrasyon testleri uygulayın.
