import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import * as moment from "moment";
import { shareReplay, tap } from "rxjs/operators";
import * as globalVar from '../../../globals';
import jwt_decode from "jwt-decode";
export interface User {
    data: "";
};
@Injectable()
export class AuthService {

    constructor(private http: HttpClient) {
    }

    login(username: string, password: string, isUserLecturer: boolean) {
        return this.http.post<User>(globalVar.apiURL + '/api/ldap/login', { "username": username, "password": password })
            .pipe(tap(res => this.setSession(res, isUserLecturer)), shareReplay(1));
    }
    private setSession(authResult: any, isUserLecturer: boolean) {
        const decodedToken = this.getDecodedAccessToken(authResult.data);

        localStorage.setItem('id_token', authResult.data);
        localStorage.setItem("expires_at", decodedToken.exp);
        if (isUserLecturer)
            localStorage.setItem("isuserlecturer", "1");
    }

    logout() {
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_at");
        localStorage.removeItem("isuserlecturer");
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
        return moment.unix(expiresAt);
    }
    getUserName(): string {
        const token = localStorage.getItem("id_token");
        const decoded = this.getDecodedAccessToken(token);
        if (decoded != undefined)
            return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        return "";
    }
    getDecodedAccessToken(token: string): any {
        try {
            return jwt_decode(token);
        }
        catch (Error) {
            return null;
        }
    }
}
