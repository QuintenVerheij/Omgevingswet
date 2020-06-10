import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_menu_test/models/Misc/messageModels.dart';
import 'package:flutter_menu_test/models/User/userModels.dart';
import 'package:http/http.dart' as http;

class RegisterPage extends StatefulWidget {
  RegisterPage({Key key}) : super(key: key);
  final String title = "register";
  @override
  _RegisterPageState createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  final _formkey = GlobalKey<FormState>();

  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();
  final _emailController = TextEditingController();
  final _cityController = TextEditingController();
  final _streetController = TextEditingController();
  final _numberController = TextEditingController();
  final _postalCodeController = TextEditingController();
  final _numberExtController = TextEditingController();

  Future<void> _performRegister() async {
    String username = _usernameController.text.trim();
    String password = _passwordController.text.trim();
    String email = _emailController.text.trim();
    String city = _cityController.text.trim();
    String street = _streetController.text.trim();
    int number = int.parse(_numberController.text.trim());
    String postalCode = _postalCodeController.text.trim();
    String numExt = _numberExtController.text.trim();

    print('login attempt: $username with $password');

    var url = "http://10.0.2.2:8080/user/create";
    var body = jsonEncode(UserCreateInput(username, password, email,
        AddressCreateInput(city, street, number, numExt, postalCode)));

    http
        .post(url, headers: {"Content-Type": "application/json"}, body: body)
        .then((http.Response res) {
      print("response code" + res.statusCode.toString());
      print("Response body" + res.body);
      var m = Message.fromJson(jsonDecode(res.body));
      if (m.successful) {
        print("jaa gelukt ofzo lol");
        // Navigator.push(
        //     context,
        //     MaterialPageRoute(
        //         builder: (context) => (MyStatefulWidget(userId: m.userId))));
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Registreer"),
        ),
        body: Center(
            child: Form(
                key: _formkey,
                child: Column(
                  children: <Widget>[
                    TextFormField(
                      controller: _usernameController,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "Username"),
                    ),
                    TextFormField(
                      controller: _emailController,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "Email Address"),
                    ),
                    TextFormField(
                      obscureText: true,
                      controller: _passwordController,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "Password"),
                    ),
                    TextFormField(
                      controller: _cityController,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "City"),
                    ),
                    TextFormField(
                      controller: _streetController,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "Street"),
                    ),
                    TextFormField(
                      controller: _numberController,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "House Number"),
                    ),
                    TextFormField(
                      controller: _numberExtController,
                      decoration:
                          InputDecoration(labelText: "House Number Addition"),
                    ),
                    TextFormField(
                      controller: _postalCodeController,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "Postal Code"),
                    ),
                    RaisedButton(
                        onPressed: () {
                          print("Validating...");
                          if (_formkey.currentState.validate()) {
                            print("Validation successful.");
                            _performRegister();
                          } else {
                            print("Validation failed.");
                          }
                        },
                        child: Text("Registreer"))
                  ],
                ))));
  }
}
