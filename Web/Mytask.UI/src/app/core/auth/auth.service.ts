import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import * as moment from "moment";
import { Observable } from "rxjs";
import { shareReplay, tap } from "rxjs/operators";

@Injectable( {
    providedIn: "root"
})
export class AuthService {

    constructor(private http: HttpClient) {

    }

    login(username:string, password:string ): Observable<any> {
        let body = new URLSearchParams();
        body.set("client_id", "mytask-client");
        body.set("client_secret", "JUImQpTmItjl6gtL0kimr6TbDrYHt0DW");
        body.set("grant_type", "password");
        body.set("scope", "openid");
        body.set("username", username);
        body.set("password", password);

        let options = {
            headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
        };

        return this.http.post("http://localhost:8484/auth/realms/my_realm/protocol/openid-connect/token", body, options)
            .pipe(
                tap(res => this.setSession(res)),
                shareReplay()
                ); 
    }
          
    private setSession(authResult: any) {
        const expiresIn = moment().add(authResult.expires_in,'second');

        localStorage.setItem('id_token', authResult.id_token);
        localStorage.setItem("expires_in", JSON.stringify(expiresIn.valueOf()) );
    }          

    logout() {
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_in");
    }

    public isLoggedIn() {
        return moment().isBefore(this.getExpiration());
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }

    getExpiration() {
        const expiration = localStorage.getItem("expires_in");
        const expiresIn = JSON.parse(expiration as string);
        return moment(expiresIn);
    }    
}