import 'package:flutter/material.dart';
import '/core/theme/extensions.dart';
import '../../../../theme/custom_colors.dart';

class LookupMultiSelectAutocomplete extends StatefulWidget {
  final List<Map<String, dynamic>> options;
  final String labelText;
  final String hintText;
  final void Function(List<int>) onChanged;
  final double contentPadding;
  final TextEditingController controller;
  final FocusNode focusNode;
  final String valueKey;
  final bool isUpperCase;
  final bool enabled;
  final bool isAllSelected;
  final List<int> selectedIds;

  const LookupMultiSelectAutocomplete({
    super.key,
    required this.options,
    required this.labelText,
    required this.hintText,
    required this.onChanged,
    required this.selectedIds,
    this.enabled = true,
    this.valueKey = "name",
    this.isUpperCase = false,
    this.contentPadding = 20,
    required this.controller,
    required this.focusNode,
    this.isAllSelected = false,
  });

  @override
  _LookupMultiSelectAutocompleteState createState() =>
      _LookupMultiSelectAutocompleteState();
}

class _LookupMultiSelectAutocompleteState
    extends State<LookupMultiSelectAutocomplete> {
  late List<int> _selectedIds;
  late bool _isAllSelected;
  late TextEditingController _searchController;
  List<Map<String, dynamic>> filteredOptions = [];

  @override
  void initState() {
    super.initState();
    _selectedIds = List.from(widget.selectedIds);
    _isAllSelected = widget.isAllSelected;
    _searchController = TextEditingController();
    filteredOptions = widget.options;
  }

  void _toggleSelectAll() {
    setState(() {
      if (_isAllSelected) {
        _selectedIds.clear();
      } else {
        _selectedIds =
            widget.options.map((e) => int.parse(e['id'].toString())).toList();
      }
      _isAllSelected = !_isAllSelected;
      widget.onChanged(_selectedIds);
    });
  }

  void _filterOptions(String query) {
    setState(() {
      filteredOptions = widget.options.where((option) {
        final value = option[widget.valueKey].toString().toLowerCase();
        return value.contains(query.toLowerCase());
      }).toList();
    });
  }

  bool _isSelected(int id) {
    return _selectedIds.contains(id);
  }

  void _onCheckboxChanged(bool? value, int id) {
    setState(() {
      if (value == true) {
        _selectedIds.add(id);
      } else {
        _selectedIds.remove(id);
      }
      widget.onChanged(_selectedIds);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: context.percent70Screen,
      height: context.percent10Screen,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Wrap(
            spacing: 6.0,
            runSpacing: 6.0,
            children: _selectedIds.map((id) {
              final selectedOption = widget.options.firstWhere(
                (option) => option['id'] == id,
                orElse: () => {}, // Boş bir map döndür (hata önleme)
              );
              return selectedOption.isNotEmpty
                  ? Chip(
                      label: Text(selectedOption[widget.valueKey]),
                      onDeleted: () {
                        setState(() {
                          _selectedIds.remove(id);
                          widget.onChanged(_selectedIds);
                        });
                      },
                    )
                  : const SizedBox.shrink();
            }).toList(),
          ),
          TextFormField(
            enabled: widget.enabled,
            controller: widget.controller,
            focusNode: widget.focusNode,
            decoration: InputDecoration(
              suffixIcon: Padding(
                padding: const EdgeInsets.only(bottom: 10, left: 15),
                child: Icon(
                  Icons.arrow_drop_down,
                  color: CustomColors.dark.getColor,
                ),
              ),
              labelText: widget.labelText,
              hintText: widget.hintText,
              contentPadding: EdgeInsets.only(bottom: widget.contentPadding),
            ),
          ),
          RawAutocomplete<String>(
            focusNode: widget.focusNode,
            textEditingController: widget.controller,
            fieldViewBuilder:
                (context, controller, focusNode, onFieldSubmitted) {
              return const SizedBox
                  .shrink(); // Field görünümü zaten yukarıda var
            },
            optionsBuilder: (TextEditingValue textEditingValue) {
              if (_searchController.text.isNotEmpty) {
                _filterOptions(_searchController.text);
              }
              return filteredOptions
                  .map((e) => e[widget.valueKey].toString())
                  .toList();
            },
            optionsViewBuilder: (context, onSelected, options) {
              return Align(
                alignment: Alignment.topLeft,
                child: Material(
                  elevation: 4.0,
                  child: ConstrainedBox(
                    constraints: const BoxConstraints(
                      maxHeight: 300,
                      maxWidth: 500,
                    ),
                    child: Column(
                      children: [
                        ListTile(
                          title: Text(
                              _isAllSelected ? "Unselect All" : "Select All"),
                          onTap: _toggleSelectAll,
                        ),
                        Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: TextField(
                            controller: _searchController,
                            onChanged: _filterOptions,
                            decoration: const InputDecoration(
                              labelText: "Search",
                              prefixIcon: Icon(Icons.search),
                            ),
                          ),
                        ),
                        Expanded(
                          child: ListView.builder(
                            padding: EdgeInsets.zero,
                            itemCount: filteredOptions.length,
                            itemBuilder: (BuildContext context, int index) {
                              final option = filteredOptions[index];
                              final int id = int.parse(option['id'].toString());
                              return ListTile(
                                leading: Checkbox(
                                  value: _isSelected(id),
                                  onChanged: (bool? value) {
                                    _onCheckboxChanged(value, id);
                                  },
                                ),
                                title: Text(option[widget.valueKey]),
                                onTap: () {
                                  _onCheckboxChanged(!_isSelected(id), id);
                                },
                              );
                            },
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              );
            },
          ),
        ],
      ),
    );
  }
}
