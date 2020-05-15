import 'package:json_annotation/json_annotation.dart';

part 'authModels.g.dart';

@JsonSerializable()
class AuthorizationToken {
  AuthorizationToken(this.token);

  String token;

  factory AuthorizationToken.fromJson(Map<String, dynamic> json) => _$AuthorizationTokenFromJson(json);

  Map<String, dynamic> toJson() => _$AuthorizationTokenToJson(this);
}
@JsonSerializable()
class AuthorizationTokenRequest {
  AuthorizationTokenRequest(this.mail, this.password);

  String mail;
  String password;

  factory AuthorizationTokenRequest.fromJson(Map<String, dynamic> json) => _$AuthorizationTokenRequestFromJson(json);

  Map<String, dynamic> toJson() => _$AuthorizationTokenRequestToJson(this);

}
@JsonSerializable()
class AuthorizationTokenReturn {
  AuthorizationTokenReturn(this.successful, this.messageType, this.message, this.userId, this.role, this.expireTime, this.token);

  bool successful;
  String messageType;
  @JsonKey(nullable: true)
  String message;
  @JsonKey(nullable: true)
  int userId;
  @JsonKey(nullable: true)
  String role;
  @JsonKey(nullable: true)
  int expireTime;
  @JsonKey(nullable: true)
  String token;


  factory AuthorizationTokenReturn.fromJson(Map<String, dynamic> json) => _$AuthorizationTokenReturnFromJson(json);

  Map<String, dynamic> toJson() => _$AuthorizationTokenReturnToJson(this);
  
}