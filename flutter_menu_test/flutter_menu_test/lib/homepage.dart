import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';

class HomePage extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    return new HomePageState();
  }
}

class HomePageState extends State<HomePage> {
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 3,
      child: Scaffold(
        appBar: AppBar(
          title: Text("Homepage"),
          actions: <Widget>[
            FlatButton(onPressed: null, child: Text("Logout"))
          ],
        ),
        body: TabBarView(
          children: [Widget1(), Widget2(), Widget3()],
        ),
        bottomNavigationBar: TabBar(
          tabs: [
            Tab(icon: Icon(Icons.directions_car, color: Colors.green)),
            Tab(icon: Icon(Icons.directions_transit, color: Colors.green)),
            Tab(icon: Icon(Icons.directions_bike, color: Colors.green)),
          ],
        ),
      ),
    );
  }
}

class Widget1 extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Icon(Icons.directions_car);
  }
}

class Widget2 extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Icon(Icons.directions_transit);
  }
}

class Widget3 extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(body: Center(child: Icon(Icons.directions_bike)));
  }
}
