import 'dart:math';

import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:flutter_devarchitecture/core/constants/core_messages.dart';
import '../../utilities/results.dart';
import '../i_service.dart';

abstract class FirebaseService implements IService {
  late CollectionReference<Map<String, dynamic>> _collectionRef;

  init(String tableName) {
    _collectionRef = FirebaseFirestore.instance.collection(tableName);
  }

  FirebaseService({required String tableName}) {
    init(tableName);
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> create(
      Map<String, dynamic> body) async {
    body["id"] = Random().nextInt(999999999);
    if (body["createdAt"] != null) {
      Timestamp.fromDate(body["createdAt"]);
    }
    if (body["updatedAt"] != null) {
      Timestamp.fromDate(body["updatedAt"]);
    }
    if (body["deletedAt"] != null) {
      Timestamp.fromDate(body["deletedAt"]);
    }
    await _collectionRef.add(body);
    return SuccessDataResult(body, "");
  }

  @override
  Future<IResult> createMany(List<Map<String, dynamic>> bodies) async {
    for (var element in bodies) {
      element["id"] = Random().nextInt(999999999);
      await create(element);
    }
    return Future.value(
        SuccessResult(CoreMessages.customerDefaultSuccessMessage));
  }

  @override
  Future<IDataResult<List<Map<String, dynamic>>>> getAll() async {
    QuerySnapshot<Map<String, dynamic>> querySnapshot =
        await _collectionRef.get();
    return SuccessDataResult(
        querySnapshot.docs.map((doc) {
          var data = doc.data();
          if (data["createdAt"] != null) {
            data["createdAt"] = (data['createdAt'] as Timestamp).toDate();
          }

          if (data["updatedAt"] != null) {
            data["updatedAt"] = (data['updatedAt'] as Timestamp).toDate();
          }

          if (data["deletedAt"] != null) {
            data["deletedAt"] = (data['deletedAt'] as Timestamp).toDate();
          }
          return data;
        }).toList(),
        "");
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> getById(int id) async {
    QuerySnapshot<Map<String, dynamic>> querySnapshot =
        await _collectionRef.get();
    Map<String, dynamic> data = {};
    var datas = querySnapshot.docs.map((doc) => doc.data()).toList();
    for (var i = 0; i < datas.length; i++) {
      if (datas[i]["id"].toString() == id.toString()) {
        data = datas[i];
      }
    }
    if (data["createdAt"] != null) {
      data["createdAt"] = (data['createdAt'] as Timestamp).toDate();
    }

    if (data["updatedAt"] != null) {
      data["updatedAt"] = (data['updatedAt'] as Timestamp).toDate();
    }

    if (data["deletedAt"] != null) {
      data["deletedAt"] = (data['deletedAt'] as Timestamp).toDate();
    }
    return SuccessDataResult(data, "");
  }

  @override
  Future<IDataResult<Map<String, dynamic>>> getByName(String name) async {
    QuerySnapshot<Map<String, dynamic>> querySnapshot =
        await _collectionRef.get();
    Map<String, dynamic> data = {};
    var datas = querySnapshot.docs.map((doc) => doc.data()).toList();
    for (var i = 0; i < datas.length; i++) {
      if (datas[i]["name"].toString() == name.toString()) {
        data = datas[i];
      }
    }
    if (data["createdAt"] != null) {
      data["createdAt"] = (data['createdAt'] as Timestamp).toDate();
    }

    if (data["updatedAt"] != null) {
      data["updatedAt"] = (data['updatedAt'] as Timestamp).toDate();
    }

    if (data["deletedAt"] != null) {
      data["deletedAt"] = (data['deletedAt'] as Timestamp).toDate();
    }
    return SuccessDataResult(data, "");
  }

  @override
  Future<IResult> update(Map<String, dynamic> body) async {
    var documentID = "";
    QuerySnapshot<Map<String, dynamic>> querySnapshot =
        await _collectionRef.get();
    var ids = querySnapshot.docs.map((doc) => doc.id).toList();
    var datas = querySnapshot.docs.map((doc) => doc.data()).toList();
    for (var i = 0; i < ids.length; i++) {
      if (datas[i]["id"] == body["id"]) {
        documentID = ids[i];
      }
    }
    await _collectionRef.doc(documentID).update(body);
    return Future.value(
        SuccessResult(CoreMessages.customerDefaultSuccessMessage));
  }

  @override
  Future<IResult> delete(int id) async {
    var documentID = "";
    QuerySnapshot<Map<String, dynamic>> querySnapshot =
        await _collectionRef.get();
    var ids = querySnapshot.docs.map((doc) => doc.id).toList();
    var datas = querySnapshot.docs.map((doc) => doc.data()).toList();
    for (var i = 0; i < ids.length; i++) {
      if (datas[i]["id"] == id) {
        documentID = ids[i];
      }
    }
    await _collectionRef.doc(documentID).delete();
    return Future.value(
        SuccessResult(CoreMessages.customerDefaultSuccessMessage));
  }
}
