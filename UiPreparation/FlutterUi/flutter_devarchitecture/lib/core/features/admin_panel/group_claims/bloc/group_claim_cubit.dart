import '../../../../bloc/base_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../constants/core_messages.dart';
import '../../../../utilities/results.dart';
import '../../../../../di/business_initializer.dart';
import '../../lookups/models/lookup.dart';
import '../models/group_claim.dart';

class GroupClaimCubit extends BaseCubit<GroupClaim> {
  GroupClaimCubit() : super() {
    super.service = BusinessInitializer().businessContainer.groupClaimService;
  }

  Future<void> getGroupClaimsByGroupId(int groupId) async {
    emit(BlocLoading());
    try {
      final groupClaims = await BusinessInitializer()
          .businessContainer
          .lookupService
          .getOperationClaimLookUp();

      final selectedGroupClaimResult = await BusinessInitializer()
          .businessContainer
          .groupClaimService
          .getGroupClaimsByGroupId(groupId);
      if (!selectedGroupClaimResult.isSuccess) {
        emitFailState(selectedGroupClaimResult.message);
        return;
      }
      var selectedClaimIds =
          selectedGroupClaimResult.data!.map((claim) => claim.id).toSet();

      List<LookUp> updatedClaims = groupClaims.map((claim) {
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

  Future<void> saveGroupClaimsByGroupId(int groupId, List<int> claims) async {
    emit(BlocLoading());
    try {
      var result = await BusinessInitializer()
          .businessContainer
          .groupClaimService
          .update({'GroupId': groupId, 'ClaimIds': claims});
      if (result.isSuccess == false) {
        emitFailState(result.message);
        return;
      }
      await getGroupClaimsByGroupId(groupId);
      emit(BlocSuccess(
          new SuccessResult(CoreMessages.customerDefaultSuccessMessage)));
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }
}
