import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { shareReplay } from 'rxjs/operators';
import * as globalVar from '../../../globals'
import { ExerciseNode } from '../exercise-module/exercise-node';
import { FileNodeType } from '../file-module/file-node';
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
        return this.http.get<ResponseBody>(globalVar.apiURL + "/api/exercises");
    }
    public getExercisesById(projectId: number) {
        return this.http.get<ResponseBodySingle>(globalVar.apiURL + "/api/exercises/" + projectId);
    }
    public postExercises(name: string, description: string, projectType: FileNodeType) {
        return this.http.post(globalVar.apiURL + "/api/exercises", { name: name, description: description, taskDefinition: "", projectType: this.convertFileTypeToNumber(projectType) })
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
    private convertFileTypeToNumber(fileType: FileNodeType): number {
        switch (fileType) {
            case FileNodeType.csharp:
                return 0;
            case FileNodeType.cpp:
                return 1;
            case FileNodeType.java:
                return 2;
            case FileNodeType.folder:
                return 3;
            case FileNodeType.folder:
                return 4;
            case FileNodeType.python:
                return 5;
            default:
                return 0;
        }
    }
}
