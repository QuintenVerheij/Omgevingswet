import 'package:json_annotation/json_annotation.dart';
import 'package:json_serializable/builder.dart';

part 'userModels.g.dart';

@JsonSerializable(nullable: false)
class UserCreateInput {
  UserCreateInput(this.username, this.email, this.password, this.address);

  String username;
  String email;
  String password;
  AddressCreateInput address;

  factory UserCreateInput.fromJson(Map<String, dynamic> json) => _$UserCreateInputFromJson(json);

  Map<String, dynamic> toJson() => _$UserCreateInputToJson(this);
}

@JsonSerializable(nullable: false)
class AddressCreateInput {
  AddressCreateInput(this.city, this.street, this.houseNumber, this.houseNumberAddition, this.postalCode);

  String city;
  String street;
  int houseNumber;

  @JsonKey(nullable: true)
  String houseNumberAddition;

  String postalCode;

  factory AddressCreateInput.fromJson(Map<String, dynamic> json) => _$AddressCreateInputFromJson(json);

  Map<String, dynamic> toJson() => _$AddressCreateInputToJson(this);

}