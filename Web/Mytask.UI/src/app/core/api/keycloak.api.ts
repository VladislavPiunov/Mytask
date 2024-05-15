import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { MyTaskApiPaths, environment } from "src/app/environments/environments";
import { User } from "../model/user.model";

@Injectable({
  providedIn: "root"
})
export class KeycloakApi {
 readonly API : string;

 constructor(private http: HttpClient) {
  this.API = environment.mytaskUrl;
 }

 getUsers(): Observable<User[]> {
   return this.http.get<User[]>(this.API + MyTaskApiPaths.Keycloak);
 }

}