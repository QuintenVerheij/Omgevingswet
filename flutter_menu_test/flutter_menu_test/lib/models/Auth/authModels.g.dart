// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'authModels.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AuthorizationToken _$AuthorizationTokenFromJson(Map<String, dynamic> json) {
  return AuthorizationToken(
    json['token'] as String,
  );
}

Map<String, dynamic> _$AuthorizationTokenToJson(AuthorizationToken instance) =>
    <String, dynamic>{
      'token': instance.token,
    };

AuthorizationTokenRequest _$AuthorizationTokenRequestFromJson(
    Map<String, dynamic> json) {
  return AuthorizationTokenRequest(
    json['mail'] as String,
    json['password'] as String,
  );
}

Map<String, dynamic> _$AuthorizationTokenRequestToJson(
        AuthorizationTokenRequest instance) =>
    <String, dynamic>{
      'mail': instance.mail,
      'password': instance.password,
    };

AuthorizationTokenReturn _$AuthorizationTokenReturnFromJson(
    Map<String, dynamic> json) {
  return AuthorizationTokenReturn(
    json['successful'] as bool,
    json['messageType'] as String,
    json['message'] as String,
    json['userId'] as int,
    json['role'] as String,
    json['expireTime'] as int,
    json['token'] as String,
  );
}

Map<String, dynamic> _$AuthorizationTokenReturnToJson(
        AuthorizationTokenReturn instance) =>
    <String, dynamic>{
      'successful': instance.successful,
      'messageType': instance.messageType,
      'message': instance.message,
      'userId': instance.userId,
      'role': instance.role,
      'expireTime': instance.expireTime,
      'token': instance.token,
    };
