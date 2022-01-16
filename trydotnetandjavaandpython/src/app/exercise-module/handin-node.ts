import { FileNode } from "../file-module/file-node";
import { User, UserProject } from "../service/userproject.service";
export interface UserWithoutData {
    ldapUid?: string;
    name: string;
    email: string;
    projects: UserProject[];
}
export class HandInNode {
    id?: number;
    userToGrade?: UserWithoutData;
    grade?: number;
    status?: number;
    feedback?: string
    project?: UserProject;
    files?: FileNode[];
}