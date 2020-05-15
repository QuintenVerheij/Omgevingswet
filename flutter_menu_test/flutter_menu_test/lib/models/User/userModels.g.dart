// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'userModels.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserCreateInput _$UserCreateInputFromJson(Map<String, dynamic> json) {
  return UserCreateInput(
    json['username'] as String,
    json['email'] as String,
    json['password'] as String,
    AddressCreateInput.fromJson(json['address'] as Map<String, dynamic>),
  );
}

Map<String, dynamic> _$UserCreateInputToJson(UserCreateInput instance) =>
    <String, dynamic>{
      'username': instance.username,
      'email': instance.email,
      'password': instance.password,
      'address': instance.address,
    };

AddressCreateInput _$AddressCreateInputFromJson(Map<String, dynamic> json) {
  return AddressCreateInput(
    json['city'] as String,
    json['street'] as String,
    json['houseNumber'] as int,
    json['houseNumberAddition'] as String,
    json['postalCode'] as String,
  );
}

Map<String, dynamic> _$AddressCreateInputToJson(AddressCreateInput instance) =>
    <String, dynamic>{
      'city': instance.city,
      'street': instance.street,
      'houseNumber': instance.houseNumber,
      'houseNumberAddition': instance.houseNumberAddition,
      'postalCode': instance.postalCode,
    };
