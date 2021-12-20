import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as globalVar from '../../../globals'
import { ExerciseNode } from '../exercise-module/exercise-node';
export interface ResponseBody {
    data: ExerciseNode[];
};
@Injectable()
export class ExerciseService {

    constructor(private http: HttpClient) { }
    public getExercises() {
        return this.http.get<ResponseBody>(globalVar.apiURL + `/api/exercises`);
    }
}
