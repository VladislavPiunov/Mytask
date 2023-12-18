import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Stage } from "../models/stage.model";

@Injectable({
  providedIn: "root"
})
export class StageApi {
 readonly API = 'http://localhost:8081/api/stage';

 constructor(private http: HttpClient) {}

 getStages(boardId: string): Observable<Stage[]> {
   return this.http.get<Stage[]>(this.API + "/" + boardId);
 }

}