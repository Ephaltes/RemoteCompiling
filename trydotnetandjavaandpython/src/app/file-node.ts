import { CodeModel } from '@ngstack/code-editor/public_api';

export enum FileNodeType {
    csharp = 'csharp',
    java = 'java',
    python = 'python',
    cpp = 'cpp',
    c = 'c',
    folder = 'folder'
}

export class FileNode {
    children?: FileNode[];
    name: string;
    type: FileNodeType;

    code?: CodeModel;

    constructor(name:string, type:FileNodeType, code:string,){
        this.name=name;
        this.type=type;
        this.code={language:type,value:code,uri:name};
    }
}
