import { FileNode } from "../file-module/file-node";

export class ExerciseNode {
    id: number;
    name: string;
    author: string;
    description:string;
    files?: FileNode[];

    constructor(id:number,name:string, author:string, description:string, files?:FileNode[]){
        this.id=id;
        this.name=name;
        this.author=author;
        this.description=description;
        this.files=files;
    }
}
