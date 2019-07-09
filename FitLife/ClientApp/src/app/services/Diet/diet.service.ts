import { Diet } from './../../Models/diet';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DietData } from '../../Models/diet.data.';

@Injectable()
export class DietService {

  constructor( private http: HttpClient) { }

  createDiet(dietData: DietData) : Observable<Diet>{
    return this.http.post<Diet>("https://localhost:44348/api/Account/login",dietData)
  }

}
