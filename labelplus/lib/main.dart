import 'package:flutter/material.dart';

import 'editor_page.dart';
// import 'setting_page.dart';
// import 'about_page.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'LabelPlus',
      theme: ThemeData(
        primarySwatch: Colors.pink,
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: StartPage(),
      routes: {
        "/editor": (context) => EditorPage(),
      },
    );
  }
}

class StartPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final navigator = Navigator.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text("Start Page"),
      ),
      body: Center(
        child: FlatButton(
          child: Text("Open editor"),
          onPressed: () => navigator.pushNamed("/editor"),
        ),
      )
    );
  }
}
