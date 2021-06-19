import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FileNode, FileNodeType } from './file-node';

const FILES_DATA: FileNode[] = [
  {
    name: 'HelloWorldCSharp.cs',
    type: FileNodeType.csharp,
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
    name: 'HelloAngular.cs',
    type: FileNodeType.csharp,
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
  },
  {
    name: 'HelloWorldJava.java',
    type: FileNodeType.java,
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
  },
  {
    name: 'HelloWorldPython.py',
    type: FileNodeType.python,
    code: {
      language: 'python',
      uri: 'HelloWorldPython.py',
      value: [
        `print('Hello, world from Python!')`
      ].join('\r\n')
    }
  },
  {
    name: 'HelloWorldCpp.cpp',
    type: FileNodeType.cpp,
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
    name: 'HelloWorldC.c',
    type: FileNodeType.c,
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
  add(name: string, language: FileNodeType, code: string) {
    this.dataChange.next(this.dataChange.getValue().concat({ name: name, type: language, code: { language: language, uri: name, value: code } }));
  }
  remove(file: FileNode) {
    const dataRemoveArray: FileNode[] = this.dataChange.getValue();
    dataRemoveArray.forEach((item, index) => {
      if (item === file)
        dataRemoveArray.splice(index, 1);
    });
    this.dataChange.next(dataRemoveArray);
  }
  fileNames(): string[] {
    var list = new Array();
    this.data.forEach(element => {
      list.push(this.fileEndingRemover(element.name));
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
