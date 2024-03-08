import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Stage } from "../models/stage.model";
import { MyTaskApiPaths, environment } from "src/app/environments/environments";

@Injectable({
  providedIn: "root"
})
export class StageApi {
 readonly API : string;

 constructor(private http: HttpClient) {
  this.API = environment.mytaskUrl + MyTaskApiPaths.Stage;
 }

 getStages(boardId: string): Observable<Stage[]> {
   return this.http.get<Stage[]>(this.API + "/" + boardId);
 }

}