import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'i_map.dart';

class MapGoogle implements IMap {
  @override
  Widget buildMap() {
    return Container(
      child: GoogleMap(
        initialCameraPosition: CameraPosition(
          target: LatLng(36.5873, 36.1735), // Hatay İskenderun koordinatları :)
          zoom: 12,
        ),
      ),
    );
  }
}
