import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FileNode, FileNodeType } from './file-node';
/*
const FILES_DATA: FileNode[] = [
  {
    projectName: 'HelloWorldAngular',
    projectType: FileNodeType.folder,
    children: [{
      projectName: 'HelloWorldCSharp.cs',
      projectType: FileNodeType.csharp,
      code: {
        language: 'csharp',
        uri: 'HelloWorldCSharp.cs',
        value: [
          'using System;',
          `namespace HelloWorld`,
          '{',
          '    class Program',
          '    {',
          `        static void Main(string[] args)`,
          `        {`,
          `            Console.WriteLine($"Hello, world from .NET and {Angular.name}!");`,
          '        }',
          '    }',
          '}'
        ].join('\r\n')
      }
    },
    {
      projectName: 'HelloAngular.cs',
      projectType: FileNodeType.csharp,
      code: {
        language: 'csharp',
        uri: 'HelloAngular.cs',
        value: [
          'using System;',
          `namespace HelloWorld`,
          '{',
          '    public class Angular',
          '    {',
          `        public static string name="Angular";`,
          '    }',
          '}'
        ].join('\r\n')
      }
    }]
  },
  {
    projectName: 'HelloWorldJava',
    projectType: FileNodeType.folder,
    children: [{
      projectName: 'HelloWorldJava.java',
      projectType: FileNodeType.java,
      code: {
        language: 'java',
        uri: 'HelloWorldJava.java',
        value: [
          'public class HelloWorld',
          `{`,
          `       public static void main (String[] args)`,
          `       {`,
          `            System.out.println("Hello, world from Java!");`,
          `       }`,
          `}`
        ].join('\r\n')
      }
    }]
  },
  {
    projectName: 'HelloWorldPython',
    projectType: FileNodeType.folder,
    children: [{
      projectName: 'HelloWorldPython.py',
      projectType: FileNodeType.python,
      code: {
        language: 'python',
        uri: 'HelloWorldPython.py',
        value: [
          `print('Hello, world from Python!')`
        ].join('\r\n')
      }
    }]
  },
  {
    projectName: 'HelloWorldC',
    projectType: FileNodeType.folder,
    children: [{
      projectName: 'HelloWorldCpp.cpp',
      projectType: FileNodeType.cpp,
      code: {
        language: 'cpp',
        uri: 'HelloWorldCpp.cpp',
        value: [
          '#include <iostream>',
          `    int main() {`,
          `        std::cout << "Hello World!";`,
          `        return 0;`,
          '    }',
        ].join('\r\n')
      }
    },
    {
      projectName: 'HelloWorldC.c',
      projectType: FileNodeType.c,
      code: {
        language: 'c',
        uri: 'HelloWorldC.c',
        value: [
          '#include <stdio.h>',
          `    int main() {`,
          `        printf("Hello World!");`,
          `        return 0;`,
          '    }',
        ].join('\r\n')
      }
    }]
  }
];

@Injectable()
export class FileDatabase {
  dataChange = new BehaviorSubject<FileNode[]>([]);

  get data(): FileNode[] {
    return this.dataChange.value;
  }

  constructor() {
    this.initialize();
  }
  getAllFolders(): FileNode[] {
    var list: FileNode[] = [];
    this.data.forEach(element => {
      if (element.projectType === FileNodeType.folder)
        list.push(element)
    });
    return list;
  }
  addFolder(name: string) {
    this.dataChange.next(this.dataChange.getValue().concat({ projectName: name, projectType: FileNodeType.folder, children:[] }));
  }
  addFile(folderName: string, name: string, language: FileNodeType, code: string) {
    this.dataChange.getValue().find(folder => folder.projectName == folderName).children.push({ projectName: name, projectType: language, code: { language: language, uri: name, value: code } })
    this.dataChange.next(this.dataChange.getValue());
    console.log(this.dataChange.getValue());
  }
  remove(file: FileNode) {
    const dataRemoveArray: FileNode[] = this.dataChange.getValue();
    var found = false;
    for (let index = 0; index < dataRemoveArray.length; index++) {
      let item = dataRemoveArray[index];
      if (found) {
        break;
      }
      if (item === file) {
        dataRemoveArray.splice(index, 1);
        break;
      }
      if (item.projectType === FileNodeType.folder) {
        for (let indexchild = 0; indexchild < item.children.length; indexchild++) {
          let child = item.children[indexchild];
          if (child === file) {
            dataRemoveArray[index].children.splice(indexchild, 1);
            found = true;
            break;
          }
        }
      }
    }
    this.dataChange.next(dataRemoveArray);
  }
  fileNames(): string[] {
    var list = new Array();
    this.data.forEach(element => {
      list.push(element.projectName.toLowerCase());
    });
    console.log(list);
    return list;
  }
  fileNamesForFolders(folderName: string): string[] {
    var list = new Array();
    this.data.forEach((element) => {
      if (element.projectName == folderName) {
        element.children.forEach(file => {
          list.push(this.fileEndingRemover(file.projectName.toLowerCase()));
        });
      }
    });

    return list;
  }
  fileNamesForFoldersWithExt(folderName: string): string[] {
    var list = new Array();
    this.data.forEach((element) => {
      if (element.projectName == folderName) {
        element.children.forEach(file => {
          list.push(file.projectName.toLowerCase());
        });
      }
    });

    return list;
  }
  fileEndingRemover(fileName: string): string {
    return fileName.slice(0, fileName.lastIndexOf(".")).toLowerCase();
  }
  save() {
    localStorage.setItem("dataSource", JSON.stringify(this.data));
  }
  initialize() {
    var data;
    if (localStorage.getItem("dataSource") === null) {
      data = FILES_DATA;
    } else if (JSON.parse(localStorage.getItem("dataSource")).length == 0) {
      data = FILES_DATA;
    } else {
      data = JSON.parse(localStorage.getItem("dataSource"));
    }
    this.dataChange.next(data);
  }
}
*/