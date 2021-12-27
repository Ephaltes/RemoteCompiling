import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { shareReplay } from 'rxjs/operators';
import * as globalVar from '../../../globals'
import { ExerciseNode } from '../exercise-module/exercise-node';
export interface ResponseBody {
    data: ExerciseNode[];
};
@Injectable()
export class ExerciseService {

    constructor(private http: HttpClient) { }
    public getExercises() {
        return this.http.get<ResponseBody>(globalVar.apiURL + "/api/exercises");
    }
    public postExercises(name: string, description: string) {
        return this.http.post(globalVar.apiURL + "/api/exercises", { name: name, description: description, taskDefinition: "" })
            .pipe(shareReplay(1));
    }
    public deleteExercises(id: number) {
        return this.http.delete(globalVar.apiURL + "/api/exercises", { body: { id: id } })
            .pipe(shareReplay(1));
    }
    public putExercises(exercise: ExerciseNode) {
        return this.http.put(globalVar.apiURL + "/api/exercises",  exercise )
            .pipe(shareReplay(1));
    }
}
