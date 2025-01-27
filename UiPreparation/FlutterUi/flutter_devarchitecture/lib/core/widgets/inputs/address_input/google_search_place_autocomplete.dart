import 'package:flutter/material.dart';
import 'package:google_search_place/google_search_place.dart';
import 'package:google_search_place/model/prediction.dart';

import '../../../constants/core_screen_texts.dart';

class GoogleSearchPlaceAutoComplete {
  Widget getAddressInput({
    required Function(Prediction placeDetails) getPlaceDetailWithLatLng,
  }) {
    final TextEditingController _searchPlaceController =
        TextEditingController();
    return SearchPlaceAutoCompletedTextField(
      countries: ['tr'],
      inputDecoration: InputDecoration(labelText: CoreScreenTexts.address),
      googleAPIKey: "//TODO: Add Api Key",
      controller: _searchPlaceController,
      itmOnTap: (Prediction prediction) {
        _searchPlaceController.text = prediction.description ?? "";

        _searchPlaceController.selection = TextSelection.fromPosition(
            TextPosition(offset: prediction.description?.length ?? 0));
      },
      getPlaceDetailWithLatLng: getPlaceDetailWithLatLng,
    );
  }
}
