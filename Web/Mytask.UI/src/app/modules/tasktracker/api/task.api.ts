import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MyTaskApiPaths, environment } from "src/app/environments/environments";
import { Task } from "../models/task.model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root"
  })
  export class TaskApi {
   readonly API : string;
  
   constructor(private http: HttpClient) {
    this.API = environment.mytaskUrl + MyTaskApiPaths.Task;
   }
  
   getTasks(boardId: string): Observable<Task[]> {
     return this.http.get<Task[]>(this.API + "/" + boardId);
   }
  
  }