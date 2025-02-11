import 'package:flutter_devarchitecture/routes/routes_constants.dart';
import 'package:flutter_modular/flutter_modular.dart';

import '../../../../di/core_initializer.dart';
import '../../lookups/models/lookup.dart';
import '../models/user_claim.dart';
import '../../../../bloc/base_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../../di/business_initializer.dart';

class UserClaimCubit extends BaseCubit<UserClaim> {
  UserClaimCubit() : super() {
    super.service = BusinessInitializer().businessContainer.userClaimService;
  }

  Future<void> getUserClaimsByUserId(int userId) async {
    emit(BlocLoading());
    try {
      final userClaims = await BusinessInitializer()
          .businessContainer
          .lookupService
          .getOperationClaimLookUp();

      final selectedUserClaimsResult = await BusinessInitializer()
          .businessContainer
          .userClaimService
          .getUserClaimsByUserId(userId);

      if (!selectedUserClaimsResult.isSuccess) {
        emitFailState(selectedUserClaimsResult.message);
        return;
      }
      var selectedClaimIds =
          selectedUserClaimsResult.data!.map((claim) => claim.id).toSet();

      List<LookUp> updatedClaims = userClaims.map((claim) {
        return LookUp(
          id: claim.id,
          label: claim.label,
          isSelected: selectedClaimIds.contains(claim.id),
        );
      }).toList();

      emit(BlocSuccess<List<LookUp>>(updatedClaims));
    } on Exception catch (e) {
      emitFailState(e.toString(), e: e);
    }
  }

  Future<void> saveUserClaims(int userId, List<int> claims) async {
    emit(BlocLoading());
    try {
      var result = await service.update({"UserId": userId, "ClaimIds": claims});

      if (!result.isSuccess) {
        emitFailState(result.message);
        return;
      }
      var currentUser =
          await CoreInitializer().coreContainer.storage.read("userId");

      if (currentUser == null || userId == int.parse(currentUser)) {
        Modular.to.pushNamed(RoutesConstants.loginPage);
        return;
      }
      await getUserClaimsByUserId(userId);
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }
}
