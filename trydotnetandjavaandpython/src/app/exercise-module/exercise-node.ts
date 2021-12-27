import { FileNode } from "../file-module/file-node";
import { StudentNode } from "./student-node";

export class ExerciseNode {
    id: number;
    name: string;
    author: string;
    description: string;
    taskDefiniton?: string;
    dueData?: Date;
    template?: FileNode[];
    files?: FileNode[];
    students?: StudentNode[];

    constructor(id: number, name: string, author: string, description: string, files?: FileNode[], students?: StudentNode[]) {
        this.id = id;
        this.name = name;
        this.author = author;
        this.description = description;
        this.files = files;
        this.students = students;
    }
}
