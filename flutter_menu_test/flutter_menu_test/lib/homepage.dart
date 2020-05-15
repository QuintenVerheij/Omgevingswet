
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
          children: [Widget1(), Widget1(), Widget3()],
        ),
        bottomNavigationBar: TabBar(
          tabs: [
            Tab(icon: Icon(Icons.map, color: Colors.green)),
            Tab(icon: Icon(Icons.camera_alt, color: Colors.green)),
            Tab(icon: Icon(Icons.settings, color: Colors.green)),
          ],
        ),
      ),
    );
  }
}

class Widget1 extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Image(image: AssetImage("assets/google_maps_2.jpg"));
  }
}

class Widget3 extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(body: Center(child: Icon(Icons.directions_bike)));
  }
}
