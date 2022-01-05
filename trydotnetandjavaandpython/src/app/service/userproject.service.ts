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
    checkpoint?:CheckPoint
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
        ldapUid?: string;
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
        files.forEach(async project => {
            for (const file of project.children) {
                if (file.modified)
                    await this.http.post(globalVar.apiURL + "/api/file/addCheckPoint", { fileId: file.fileId, checkpoint: { code: file.code.value } }).toPromise();
            }
        });
    }

    
}
