import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import * as moment from "moment";
import { Observable } from "rxjs";
import { shareReplay, tap } from "rxjs/operators";

@Injectable( {
    providedIn: "root"
})
export class AuthService {

    constructor(private http: HttpClient) { }

    login(username:string, password:string ): Observable<any> {
        const body = new URLSearchParams();
        body.set("client_id", "mytask-client");
        body.set("client_secret", "CvrgW62xy9BvrfWI1SRfyvlUi8sVgHpQ");
        body.set("grant_type", "password");
        body.set("scope", "openid");
        body.set("username", username);
        body.set("password", password);

        const options = {
            headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
        };

        return this.http.post("http://localhost:8484/auth/realms/my_realm/protocol/openid-connect/token", body, options)
            .pipe(
                tap(res => this.setSession(res)),
                shareReplay()
                ); 
    }
    
    refresh() : Observable<any> {
        const body = new URLSearchParams();
        body.set("client_id", "mytask-client");
        body.set("client_secret", "JUImQpTmItjl6gtL0kimr6TbDrYHt0DW");
        body.set("grant_type", "refresh_token");
        body.set("refresh_token", localStorage.getItem("refresh_token") ?? "");

        const options = {
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
        const refreshExpiresIn = moment().add(authResult.refresh_expires_in,'second');

        localStorage.setItem("id_token", authResult.id_token);
        localStorage.setItem("expires_in", JSON.stringify(expiresIn.valueOf()));
        localStorage.setItem("refresh_token", authResult.refresh_token);
        localStorage.setItem("refresh_expires_in", JSON.stringify(refreshExpiresIn.valueOf()));
    }          

    logout() {
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_in");
        localStorage.removeItem("refresh_token");
        localStorage.removeItem("refresh_expires_in");
    }

    isLoggedIn() : boolean {
        const expiration = localStorage.getItem("expires_in");
        const refreshExpiration = localStorage.getItem("refresh_expires_in");
        if(moment().isBefore(this.getExpiration(expiration)))
        return true;
        else if (moment().isBefore(this.getExpiration(refreshExpiration))) 
        {
            this.refresh().subscribe({
                next: () => { return true; },
                error: (err) => {
                    console.log(err);
                    return false;
                } 
            });
        }
        return false;
    }

    getExpiration(expiration: string | null) {
        const expiresIn = JSON.parse(expiration as string);
        return moment(expiresIn);
    }    
}