import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import * as globalVar from '../../../globals'
import { FileNode, FileNodeType } from "../file-module/file-node";
export interface CheckPoint {
    id?: number;
    code: string;
    created?: Date;
}
export interface FileEntity {
    id?: number;
    lastModified?: Date;
    fileName: string;
    checkpoints: CheckPoint[]
}
export interface UserProject {
    id: number;
    projectName: string;
    stdIn?: string;
    files?: FileEntity[];
    projectType: number;
}
export interface User {
    data: {
        name: string;
        email: string;
        projects: UserProject[];
    }
}
export interface DataIdBody {
    data: number;
}
@Injectable()
export class UserProjectService {
    constructor(private http: HttpClient) { }
    public getProjects() {
        return this.http.get<User>(globalVar.apiURL + "/api/user/getMySelf");
    }
    public postProject(projectName: string, projectType: number, files: FileEntity[]) {
        return this.http.post(globalVar.apiURL + "/api/project/add", { project: { projectName: projectName, files: files, projectType: projectType } })
    }
    public deleteProject(projectId: number) {
        return this.http.delete(globalVar.apiURL + "/api/project/delete", { body: { projectId: projectId } })
    }
    public postFileToProject(projectId: number, fileName: string) {
        return this.http.post<DataIdBody>(globalVar.apiURL + "/api/file/add", { file: { fileName: fileName, checkpoints: [{ code: "" }] }, projectId: projectId })
    }
    public deleteFileFromProject(projectId: number, fileId: number) {
        return this.http.delete(globalVar.apiURL + "/api/file/remove", { body: { projectId: projectId, fileId: fileId } })
    }
    
    public convertBEtoFEEntity(user: User): FileNode[] {
        var projectsConverted: FileNode[] = [];
        const projects = user.data.projects;
        if (projects != undefined) {
            projects.forEach(pj => {
                var newFileNode = new FileNode(pj.projectName, FileNodeType.folder, "")
                newFileNode.children = [];
                newFileNode.projectid = pj.id;
                newFileNode.projectType = pj.projectType;
                var fileType: FileNodeType;
                switch (pj.projectType) {
                    case 0:
                        fileType = FileNodeType.csharp;
                        break;
                    case 1:
                        fileType = FileNodeType.cpp;
                        break;
                    case 2:
                        fileType = FileNodeType.java;
                        break;
                    case 3:
                        fileType = FileNodeType.folder;
                        break;
                    case 4:
                        fileType = FileNodeType.folder;
                        break;
                    case 5:
                        fileType = FileNodeType.python;
                        break;
                    default:
                        fileType = FileNodeType.csharp;
                        break;
                }
                if (pj.files.length > 0) {
                    pj.files.forEach(pjFile => {
                        var checkpoint = pjFile.checkpoints.reduce((r, o) => r.created < o.created ? r : o);
                        var childFile = new FileNode(pjFile.fileName, fileType, checkpoint.code);
                        childFile.fileId = pjFile.id;
                        childFile.projectid = pj.id;
                        newFileNode.children.push(childFile);
                    });
                }
                projectsConverted.push(newFileNode);
            });
        }
        return projectsConverted;
    }
}
