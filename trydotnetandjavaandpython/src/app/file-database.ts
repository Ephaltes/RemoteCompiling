import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FileNode, FileNodeType } from './file-node';

const FILES_DATA: FileNode[] = [
    {
      name: 'HelloWorld.cs',
      type: FileNodeType.csharp,
      code: {
        language: 'csharp',
        uri: 'HelloWorld.cs',
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
      name: 'HelloWorld.java',
      type: FileNodeType.java,
      code: {
        language: 'java',
        uri: 'HelloWorld.java',
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
      name: 'HelloWorld.py',
      type: FileNodeType.python,
      code: {
        language: 'python',
        uri: 'main.py',
        value: [
          `print('Hello, world from Python!')`
        ].join('\r\n')
      }
    },
  {
    name: 'HelloWorld.cpp',
    type: FileNodeType.cpp,
    code: {
      language: 'cpp',
      uri: 'HelloWorld.cpp',
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
    name: 'HelloWorld.c',
    type: FileNodeType.c,
    code: {
      language: 'c',
      uri: 'HelloWorld.c',
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
  add(name:string,language:FileNodeType,code:string){
    this.dataChange.next(this.dataChange.getValue().concat({name:name,type:language,code:{language:language,uri:name,value:code}}));
  }
  initialize() {
    const data = FILES_DATA;

    // Notify the change.
    this.dataChange.next(data);
  }
}
