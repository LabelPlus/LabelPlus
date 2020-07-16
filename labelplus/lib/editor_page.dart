import 'package:flutter/material.dart';
import 'package:quill_delta/quill_delta.dart';
import 'package:zefyr/zefyr.dart';

class EditorPage extends StatefulWidget {
  @override
  EditorPageState createState() => EditorPageState();
}

class EditorPageState extends State<EditorPage> {
  /// Allows to control the editor and the document.
  ZefyrController _controller;

  /// Zefyr editor like any other input field requires a focus node.
  FocusNode _focusNode;

  @override
  void initState() {
    super.initState();
    // Here we must load the document and pass it to Zefyr controller.
    final document = _loadDocument();
    _controller = ZefyrController(document);
    _focusNode = FocusNode();
  }

  @override
  Widget build(BuildContext context) {
    // Note that the editor requires special `ZefyrScaffold` widget to be
    // one of its parents.
    return Scaffold(
      appBar: AppBar(title: Text("Editor page")),
      body: ZefyrScaffold(
        child: ZefyrEditor(
          padding: EdgeInsets.all(16),
          controller: _controller,
          focusNode: _focusNode,
        ),
      ),
    );
  }

  /// Loads the document to be edited in Zefyr.
  NotusDocument _loadDocument() {
    // For simplicity we hardcode a simple document with one line of text
    // saying "Zefyr Quick Start".
    // (Note that delta must always end with newline.)
    final Delta delta = Delta()..insert("Zefyr Quick Start\n");
    return NotusDocument.fromDelta(delta);
  }
}


// import 'package:flutter_treeview/tree_view.dart';
    // TreeViewTheme _treeViewTheme = TreeViewTheme(
    //   expanderTheme: ExpanderThemeData(
    //     type: ExpanderType.caret,
    //     modifier: ExpanderModifier.none,
    //     position: ExpanderPosition.start,
    //     color: Colors.red.shade800,
    //     size: 20,
    //   ),
    //   labelStyle: TextStyle(
    //     fontSize: 16,
    //     letterSpacing: 0.3,
    //   ),
    //   parentLabelStyle: TextStyle(
    //     fontSize: 16,
    //     letterSpacing: 0.1,
    //     fontWeight: FontWeight.w800,
    //     color: Colors.red.shade600,
    //   ),
    //   iconTheme: IconThemeData(
    //     size: 18,
    //     color: Colors.grey.shade800,
    //   ),
    //   colorScheme: ColorScheme.light(),
    // );

    // List<Node> nodes = [
    // Node(
    //   label: 'Documents',
    //   key: 'docs',
    //   expanded: true,
    //   icon: NodeIcon(
    //     codePoint:
    //         (true) ? Icons.folder_open.codePoint : Icons.folder.codePoint,
    //     color: "blue",
    //   ),
    //   children: [
    //     Node(
    //         label: 'Job Search',
    //         key: 'd3',
    //         icon: NodeIcon.fromIconData(Icons.input),
    //         children: [
    //           Node(
    //               label: 'Resume.docx',
    //               key: 'pd1',
    //               icon: NodeIcon.fromIconData(Icons.insert_drive_file)),
    //           Node(
    //               label: 'Cover Letter.docx',
    //               key: 'pd2',
    //               icon: NodeIcon.fromIconData(Icons.insert_drive_file)),
    //         ]),
    //     Node(
    //       label: 'Inspection.docx',
    //       key: 'd1',
    //     ),
    //     Node(
    //         label: 'Invoice.docx',
    //         key: 'd2',
    //         icon: NodeIcon.fromIconData(Icons.insert_drive_file)),
    //   ],
    // ),
    // Node(
    //     label: 'MeetingReport.xls',
    //     key: 'mrxls',
    //     icon: NodeIcon.fromIconData(Icons.insert_drive_file)),
    // Node(
    //     label: 'MeetingReport.pdf',
    //     key: 'mrpdf',
    //     icon: NodeIcon.fromIconData(Icons.insert_drive_file)),
    // Node(
    //     label: 'Demo.zip',
    //     key: 'demo',
    //     icon: NodeIcon.fromIconData(Icons.archive)),
    // ];
    // TreeViewController _treeViewController = TreeViewController(children: nodes);

            // TreeView(
            //   controller: _treeViewController,
            //   allowParentSelect: false,
            //   supportParentDoubleTap: false,
            //   // onExpansionChanged: _expandNodeHandler,
            //   onNodeTap: (key) {
            //     setState(() {
            //       _treeViewController = _treeViewController.copyWith(selectedKey: key);
            //     });
            //   },
            //   theme: _treeViewTheme
            // ),


    // Stack(
    //           children: <Widget>[
    //             Container(
    //               width: 100,
    //               height: 100,
    //               color: Colors.red,
    //             ),
    //             Container(
    //               width: 90,
    //               height: 90,
    //               color: Colors.green,
    //             ),
    //             Container(
    //               width: 80,
    //               height: 80,
    //               color: Colors.blue,
    //             ),
    //           ],
    //         ),
