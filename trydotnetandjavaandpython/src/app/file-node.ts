import { CodeModel } from '@ngstack/code-editor/public_api';

export enum FileNodeType {
    csharp = 'csharp',
    java = 'java',
    python = 'python',
    folder = 'folder'
}

export class FileNode {
    children?: FileNode[];
    name: string;
    type: FileNodeType;

    code?: CodeModel;
}