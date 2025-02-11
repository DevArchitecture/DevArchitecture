import 'package:flutter/material.dart';

import 'base_constants.dart';

class CoreMessages extends MessageConstantsBase {
  static late String validationError;
  static late String internetConnectionError;
  static late String defaultSuccess;
  static late String customerAddSuccessMessage;
  static late String customerDefaultSuccessMessage;
  static late String customerDefaultErrorMessage;
  static late String selectOutputFileMessage;
  static late String comingSoon;
  static late String cantBeEmpty;
  static late String invalidEmail;
  static late String minCharacter;
  static late String maxCharacter;
  static late String invalidPhone;
  static late String invalidPassword;
  static late String minimum;
  static late String maximum;
  static late String invalidNumber;
  static late String invalid;
  static late String invalidDate;
  static late String dateCantBeEmpty;
  static late String downloadSuccessMessage;
  static late String unauthorizedErrorMessage;

  static late String editMessage;
  static late String deleteMessage;
  static late String downloadMessage;
  static late String addMessage;
  static late String detailedInformationMessage;
  static late String noDataAvailable;

  static late String noData;
  static late String noConnection;
  static late String noDataFound;
  static late String error;
  static late String atLeastOneSelection;
  static late String passwordsDoNotMatch;

  static void init(BuildContext context) {
    BaseConstants.init(context);
    validationError = BaseConstants.translate("ValidationError");
    internetConnectionError =
        BaseConstants.translate("InternetConnectionError");
    defaultSuccess = BaseConstants.translate("DefaultSuccess");
    customerAddSuccessMessage =
        BaseConstants.translate("CustomerAddSuccessMessage");
    customerDefaultSuccessMessage =
        BaseConstants.translate("CustomerDefaultSuccessMessage");
    customerDefaultErrorMessage =
        BaseConstants.translate("CustomerDefaultErrorMessage");
    selectOutputFileMessage =
        BaseConstants.translate("SelectOutputFileMessage");
    comingSoon = BaseConstants.translate("ComingSoon");
    downloadSuccessMessage = BaseConstants.translate("DownloadSuccessMessage");
    cantBeEmpty = BaseConstants.translate("CantBeEmpty");
    invalidEmail = BaseConstants.translate("InvalidEmail");
    minCharacter = BaseConstants.translate("MinCharacter");
    maxCharacter = BaseConstants.translate("MaxCharacter");
    invalidPhone = BaseConstants.translate("invalidPhone");
    invalidPassword = BaseConstants.translate("InvalidPassword");
    minimum = BaseConstants.translate("Minimum");
    maximum = BaseConstants.translate("Maximum");
    invalidNumber = BaseConstants.translate("InvalidNumber");
    invalid = BaseConstants.translate("Invalid");
    invalidDate = BaseConstants.translate("InvalidDate");
    dateCantBeEmpty = BaseConstants.translate("DateCantBeEmpty");

    editMessage = BaseConstants.translate("EditMessage");
    deleteMessage = BaseConstants.translate("DeleteMessage");
    downloadMessage = BaseConstants.translate("DownloadMessage");
    addMessage = BaseConstants.translate("AddMessage");
    detailedInformationMessage =
        BaseConstants.translate("DetailedInformationMessage");
    noDataAvailable = BaseConstants.translate("NoDataAvailable");

    noData = BaseConstants.translate("NoData");
    noConnection = BaseConstants.translate("NoConnection");
    noDataFound = BaseConstants.translate("NoDataFound");
    error = BaseConstants.translate("Error");
    atLeastOneSelection = BaseConstants.translate("AtLeastOneSelection");
    passwordsDoNotMatch = BaseConstants.translate("PasswordsDoNotMatch");
    unauthorizedErrorMessage =
        BaseConstants.translate("UnauthorizedErrorMessage");
  }
}
