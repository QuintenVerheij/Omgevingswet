import 'package:json_annotation/json_annotation.dart';

part 'messageModels.g.dart';

@JsonSerializable(nullable: true)
class Message {
  Message(this.successful, this.messageType, this.authType, this.message, this.userId, this.targetId);

  bool successful;
  String messageType;
  String authType;
  String message;
  int userId;
  int targetId;

  factory Message.fromJson(Map<String, dynamic> json) => _$MessageFromJson(json);

  Map<String, dynamic> toJson() => _$MessageToJson(this);
}

