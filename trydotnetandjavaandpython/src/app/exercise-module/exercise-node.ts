import { FileNode } from "../file-module/file-node";
import { UserProject } from "../service/userproject.service";
import { HandInNode } from "./handin-node";
import { StudentNode } from "./student-node";

export class ExerciseNode {
    id: number;
    name: string;
    author: string;
    description: string;
    taskDefinition?: string;
    dueData?: Date;
    template?: UserProject;
    files?: FileNode[];
    handIns?: HandInNode[];

    constructor(id: number, name: string, author: string, description: string, files?: FileNode[]) {
        this.id = id;
        this.name = name;
        this.author = author;
        this.description = description;
        this.files = files;
    }
}
