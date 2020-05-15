// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'messageModels.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Message _$MessageFromJson(Map<String, dynamic> json) {
  return Message(
    json['successful'] as bool,
    json['messageType'] as String,
    json['authType'] as String,
    json['message'] as String,
    json['userId'] as int,
    json['targetId'] as int,
  );
}

Map<String, dynamic> _$MessageToJson(Message instance) => <String, dynamic>{
      'successful': instance.successful,
      'messageType': instance.messageType,
      'authType': instance.authType,
      'message': instance.message,
      'userId': instance.userId,
      'targetId': instance.targetId,
    };
