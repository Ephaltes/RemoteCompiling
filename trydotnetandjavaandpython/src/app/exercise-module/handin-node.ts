import { User, UserProject } from "../service/userproject.service";

export class HandInNode {
    id?: number;
    userToGrade?: User;
    grade?: number;
    status?: number;
    feedback?: string
    project?: UserProject;
}