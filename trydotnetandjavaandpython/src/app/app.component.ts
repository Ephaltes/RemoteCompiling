import { FlatTreeControl } from '@angular/cdk/tree';
import { Component } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { CodeModel } from '@ngstack/code-editor';
interface CodeFile {
  name: string;
  code: string
}
interface FileNode {
  name: string;
  code?: string;
  parent?:FileNode;
  children?: FileNode[];
}
interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
}
const TREE_DATA: FileNode[] = [
  {
    name: 'Files',
    children: [
      {
        name: 'HelloWorld.cs',
        code: "xd"
      },
      {
        name: 'HelloWorld.java',
        code: ""
      },
      {
        name: 'HelloWorld.py',
        code: ""
      },
    ]
  }
];

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  title = 'trydotnetandjavaandpython';
  theme = 'vs-dark';

  codeModel: CodeModel = {
    language: 'csharp',
    uri: 'Program.cs',
    value: '',
  };

  options = {
    contextmenu: true,
    minimap: {
      enabled: true,
    },
  };
  constructor() {
    this.dataSource.data = TREE_DATA;
  }

  onCodeChanged(value: any) {
    console.log('CODE', value);
  }

  private _transformer = (node: FileNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };
  }
  
  treeControl = new FlatTreeControl<FlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
  hasChild = (_: number, node: FlatNode) => node.expandable;
}
