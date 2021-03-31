import { FlatTreeControl } from '@angular/cdk/tree';
import { Component } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { CodeModel } from '@ngstack/code-editor';
interface FileNode {
  name: string;
  children?: FileNode[];
}
interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
}
const TREE_DATA: FileNode[] = [
  {
    name: 'Fruit',
    children: [
      {name: 'Apple'},
      {name: 'Banana'},
      {name: 'Fruit loops'},
    ]
  }, {
    name: 'Vegetables',
    children: [
      {
        name: 'Green',
        children: [
          {name: 'Broccoli'},
          {name: 'Brussels sprouts'},
        ]
      }, {
        name: 'Orange',
        children: [
          {name: 'Pumpkins'},
          {name: 'Carrots'},
        ]
      },
    ]
  },
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
