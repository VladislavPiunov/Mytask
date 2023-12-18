import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Board } from "../models/board.model";

@Injectable({
  providedIn: "root"
})
export class BoardApi {
 readonly API = 'http://localhost:8081/api/board';

 constructor(private http: HttpClient) {}

 getBoards(): Observable<Board[]> {
   return this.http.get<Board[]>(this.API);
 }

}