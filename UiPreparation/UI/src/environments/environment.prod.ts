export const environment = {
  production: true,
  getApiUrl: `https://localhost:5101/api/v1`,
  getDropDownSetting: {
    singleSelection: false,
    idField: 'id',
    textField: 'label',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: 3,
    allowSearchFilter: true
  },
  getDatatableSettings: {
    pagingType: 'full_numbers',
    pageLength: 2
  }
};
