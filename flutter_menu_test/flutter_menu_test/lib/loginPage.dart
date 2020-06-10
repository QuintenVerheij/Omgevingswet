import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_menu_test/models/Misc/messageModels.dart';
import 'package:http/http.dart' as http;
import 'main.dart';
import 'models/Auth/authModels.dart';
import 'registerPage.dart';

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key}) : super(key: key);
  final String title = "Login";
  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  final _formkey = GlobalKey<FormState>();

  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();

  Future<void> _performLogin() async {
    String username = _usernameController.text.trim();
    String password = _passwordController.text.trim();

    print('login attempt: $username with $password');

    var url = "http://10.0.2.2:8080/auth/login";
    var body = jsonEncode(AuthorizationTokenRequest(username, password));

    http
        .post(url, headers: {"Content-Type": "application/json"}, body: body)
        .then((http.Response res) {
      print("response code" + res.statusCode.toString());
      print("Response body" + res.body);
      var m = Message.fromJson(jsonDecode(res.body));
      if (m.successful) {
        Navigator.push(
            context,
            MaterialPageRoute(
                builder: (context) => (MyStatefulWidget(userId: m.userId))));
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(widget.title)),
      body: Center(
          child: Column(children: <Widget>[
        Center(
          child: Column(
            children: <Widget>[
              Form(
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
                      controller: _passwordController,
                      obscureText: true,
                      validator: (value) {
                        if (value.isEmpty) {
                          return 'Please enter some text';
                        }
                        return null;
                      },
                      decoration: InputDecoration(labelText: "Password"),
                    ),
                  ],
                ),
              ),
              RaisedButton(
                  onPressed: () {
                    // Validate returns true if the form is valid, otherwise false.
                    if (_formkey.currentState.validate()) {
                      // If the form is valid, display a snackbar. In the real world,
                      // you'd often call a server or save the information in a database
                      _performLogin();
                    }
                  },
                  child: Text("Submit"))
            ],
          ),
        ),
        RaisedButton(
            onPressed: () {
              Navigator.push(context,
                  MaterialPageRoute(builder: (context) => RegisterPage()));
            },
            child: Text("Registreer"))
      ])),
    );
  }
}