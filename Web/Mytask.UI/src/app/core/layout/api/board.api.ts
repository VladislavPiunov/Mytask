import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Board } from "../models/board.model";
import { MyTaskApiPaths, environment } from "src/app/environments/environments";

@Injectable({
  providedIn: "root"
})
export class BoardApi {
 readonly API : string;

 constructor(private http: HttpClient) {
  this.API = environment.mytaskUrl + MyTaskApiPaths.Board;
 }

 getBoards(): Observable<Board[]> {
   return this.http.get<Board[]>(this.API);
 }

}