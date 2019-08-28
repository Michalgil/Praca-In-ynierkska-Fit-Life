import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DietData } from '../../Models/diet.data.';

@Injectable()
export class DietService {

  constructor( private http: HttpClient) { }

  createDiet(dietData: DietData) : Observable<any>{
    return this.http.post<any>("https://localhost:44348/api/Diet/createDiet",dietData)
  }
  getCurrentDiet() : Observable<any>{
    return this.http.get("https://localhost:44348/api/Diet/getCurrentDiet");
  }

}
