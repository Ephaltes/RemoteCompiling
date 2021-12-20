import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import * as moment from "moment";
import { shareReplay, tap } from "rxjs/operators";
import * as globalVar from '../../../globals'
export interface User {
    data: "";
};
@Injectable()
export class AuthService {

    constructor(private http: HttpClient) {
    }

    login(username: string, password: string) {
        return this.http.post<User>(globalVar.apiURL + '/api/ldap/login', { "username": username, "password": password })
            // this is just the HTTP call, 
            // we still need to handle the reception of the token
            .pipe(tap(res => this.setSession(res)), shareReplay(1));
    }
    private setSession(authResult: any) {
        console.log(authResult)
        const expiresAt = moment().add(authResult.expiresIn, 'second');

        localStorage.setItem('id_token', authResult.data);
        localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
    }

    logout() {
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_at");
    }

    public isLoggedIn() {
        return moment().isBefore(this.getExpiration());
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }

    getExpiration() {
        const expiration = localStorage.getItem("expires_at");
        const expiresAt = JSON.parse(expiration);
        return moment(expiresAt);
    }
}
