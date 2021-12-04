import { FileNode } from "../file-module/file-node";

export class StudentNode {
    id: string;
    name: string;
    grading: number;
    files?: FileNode[];

    constructor(id: string, name: string, grading: number, files?: FileNode[]) {
        this.id = id;
        this.name = name;
        this.grading = grading;
        this.files = files;
    }
}
