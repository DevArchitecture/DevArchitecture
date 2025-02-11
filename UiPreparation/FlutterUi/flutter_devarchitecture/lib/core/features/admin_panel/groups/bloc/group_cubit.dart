import '../../../../bloc/base_cubit.dart';
import '../../../../../di/business_initializer.dart';
import '../models/group.dart';

class GroupCubit extends BaseCubit<Group> {
  GroupCubit() : super() {
    super.service = BusinessInitializer().businessContainer.groupService;
  }
}
