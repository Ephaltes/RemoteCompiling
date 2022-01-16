import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { share, shareReplay } from 'rxjs/operators';
import * as globalVar from '../../../globals'
import { ExerciseNode } from '../exercise-module/exercise-node';
import { HandInNode } from '../exercise-module/handin-node';
import { FileNode, FileNodeType } from '../file-module/file-node';
import { convertFileTypeToNumber } from './help.function.service';
import { DataIdBody } from './userproject.service';
export interface GradeBody {
    data: {
        grade: number;
        feedback: string;
    }
}
export interface ResponseBody {
    data: ExerciseNode[];
};
export interface ResponseBodySingle {
    data: ExerciseNode;
};
@Injectable()
export class ExerciseService {

    constructor(private http: HttpClient) { }
    public getExercises() {
        return this.http.get<ResponseBody>(globalVar.apiURL + "/api/exercises/Exercises");
    }
    public getExercisesById(projectId: number) {
        return this.http.get<ResponseBodySingle>(globalVar.apiURL + "/api/exercises/Exercises/" + projectId);
    }
    public getExercisesWithHandIn() {
        return this.http.get<ResponseBody>(globalVar.apiURL + "/api/exercises/ExercisesWithHandIn");
    }
    public getExercisesWithHandInById(projectId: number) {
        return this.http.get<ResponseBodySingle>(globalVar.apiURL + "/api/exercises/ExercisesWithHandIn/" + projectId);
    }
    public postExercises(name: string, description: string, projectType: FileNodeType) {
        return this.http.post(globalVar.apiURL + "/api/exercises", { name: name, description: description, taskDefinition: "", templateProjectType: convertFileTypeToNumber(projectType) })
            .pipe(shareReplay(1));
    }
    public deleteExercises(id: number) {
        return this.http.delete(globalVar.apiURL + "/api/exercises", { body: { id: id } })
            .pipe(shareReplay(1));
    }
    public putExercises(exercise: ExerciseNode) {
        return this.http.put(globalVar.apiURL + "/api/exercises", exercise)
            .pipe(shareReplay(1));
    }
    public putExerciseHandIn(projectId: number, exerciseId: number) {
        return this.http.put(globalVar.apiURL + "/api/exercises/handin", { projectId: projectId, exerciseId: exerciseId })
            .pipe(shareReplay(1));
    }
    public gradeExercise(exercise: ExerciseNode, handIn: HandInNode, feedback: string, grade: number) {
        return this.http.put(globalVar.apiURL + "/api/grade/gradeExercise", { exerciseId: exercise.id, studentId: handIn.userToGrade.ldapUid, status: 2, grading: grade, feedback: feedback }).pipe(shareReplay(1));
    }
    public getExerciseGrade(studentId: string, exerciseId: number) {
        return this.http.get<GradeBody>(globalVar.apiURL + "/api/grade/student/" + studentId + "/Exercise/" + exerciseId);
    }
    public getExerciseGradeStatus(studentId: string, exerciseId: number) {
        return this.http.get<DataIdBody>(globalVar.apiURL + "/api/grade/student/" + studentId + "/Exercise/" + exerciseId + "/status");
    }
}
