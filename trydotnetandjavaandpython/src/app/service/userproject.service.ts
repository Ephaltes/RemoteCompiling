import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { forkJoin } from 'rxjs';
import { delay } from "rxjs/operators";
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
    exerciseID: number;
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
    public postProject(exerciseId: number, projectName: string, projectType: number, files: FileEntity[]) {
        return this.http.post(globalVar.apiURL + "/api/project/add", { project: { projectName: projectName, files: files, projectType: projectType, exerciseID: exerciseId } })
    }
    public deleteProject(projectId: number) {
        return this.http.delete(globalVar.apiURL + "/api/project/delete", { body: { projectId: projectId } })
    }
    public postFileToProject(projectId: number, fileName: string) {
        return this.http.post<DataIdBody>(globalVar.apiURL + "/api/file/add", { file: { fileName: fileName, checkpoints: [{ code: "" }] }, projectId: projectId })
    }
    public postFileToProjectWithCode(projectId: number, fileName: string, code: string) {
        return this.http.post<DataIdBody>(globalVar.apiURL + "/api/file/add", { file: { fileName: fileName, checkpoints: [{ code: code }] }, projectId: projectId })
    }
    public deleteFileFromProject(projectId: number, fileId: number) {
        return this.http.delete(globalVar.apiURL + "/api/file/remove", { body: { projectId: projectId, fileId: fileId } })
    }
    public save(files: FileNode[]) {
        const calls: Observable<Object>[] = [];
        files.forEach(project => {
            for (const file of project.children) {
                this.http.post(globalVar.apiURL + "/api/file/addCheckPoint", { fileId: file.fileId, checkpoint: { code: file.code.value } }).subscribe();
            }
        });
        forkJoin(calls).subscribe();
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
                        fileType = FileNodeType.c;
                        break;
                    case 2:
                        fileType = FileNodeType.cpp;
                        break;
                    case 3:
                        fileType = FileNodeType.java;
                        break;
                    case 4:
                        fileType = FileNodeType.python;
                        break;
                    default:
                        fileType = FileNodeType.csharp;
                        break;
                }
                if (pj.files.length > 0) {
                    pj.files.forEach(pjFile => {
                        var checkpoint = pjFile.checkpoints.reduce((r, o) => r.created > o.created ? r : o);
                        var childFile = new FileNode(pjFile.fileName, fileType, checkpoint.code);
                        childFile.exerciseId = pj.exerciseID;
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
