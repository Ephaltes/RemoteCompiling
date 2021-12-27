import { FileEntity } from './file-entity';

export enum FileNodeType {
    csharp = 'csharp',
    java = 'java',
    python = 'python',
    cpp = 'cpp',
    c = 'c',
    folder = 'folder'
}

export class FileNode {
    id: number;
    projectName: string;
    stdIn?: string;
    files?: FileEntity[];
    projectType: FileNodeType;
}
