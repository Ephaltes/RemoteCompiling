import { FileNode } from "../file-module/file-node";
import { UserProject } from "../service/userproject.service";
import { HandInNode } from "./handin-node";

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
    ldapUid?: string;
    constructor(id: number, name: string, author: string, description: string, files?: FileNode[]) {
        this.id = id;
        this.name = name;
        this.author = author;
        this.description = description;
        this.files = files;
    }
}
