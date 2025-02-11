class LookUp {
  dynamic id;
  String label;
  bool? isSelected = false;

  LookUp({required this.id, required this.label, this.isSelected});

  Map<String, dynamic> toMap() {
    return {'id': id, 'label': label, 'isSelected': isSelected};
  }

  @override
  factory LookUp.fromMap(Map<String, dynamic> map) {
    return LookUp(
        id: map['id'], label: map['label'], isSelected: map['isSelected']);
  }
}
