import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FileNode, FileNodeType } from './file-node';

const FILES_DATA: FileNode[] = [
  {
    name: 'Files',
    type: FileNodeType.folder,
    children: [

      {
        name: 'HelloWorld.cs',
        type: FileNodeType.csharp,
        code: {
          language: 'csharp',
          uri: 'Program.cs',
          value: [
            'using System;',
            `namespace HelloWorld`,
            '{',
            '    class Program',
            '    {',
            `        static void Main(string[] args)`,
            `        {`,
            `            Console.WriteLine("Hello World!");`,
            '        }',
            '    }',
            '}'
          ].join('\n')
        }
      },
      {
        name: 'HelloWorld.java',
        type: FileNodeType.java,
        code: {
          language: 'java',
          uri: 'main.java',
          value: [
            'public class HelloWorld',
            `{`,
            `       public static void main (String[] args)`,
            `       {`,
            `             System.out.println("Hello World!");`,
            `       }`,
            `}`
          ].join('\n')
        }
      },
      {
        name: 'HelloWorld.py',
        type: FileNodeType.python,
        code: {
          language: 'python',
          uri: 'main.py',
          value:
            `print('Hello, world!')`,
        }
      }
    ]
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

  initialize() {
    const data = FILES_DATA;

    // Notify the change.
    this.dataChange.next(data);
  }
}
