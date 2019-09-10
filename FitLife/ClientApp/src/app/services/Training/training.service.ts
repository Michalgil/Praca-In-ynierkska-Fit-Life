import { TrainingData } from './../../Models/training.data';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AnonymousSubscription } from 'rxjs/Subscription';

@Injectable()
export class TrainingService {

  constructor(private http: HttpClient) { }

  createTraining(trainingData: TrainingData): Observable<any>{
    return this.http.post<any>("https://localhost:44348/api/Training/createTraining",trainingData);
  }

  getTraining(): Observable<any>{
    return this.http.get<any>("https://localhost:44348/api/Training/getTraining");
  }
  getQunatityoFTrainings(): Observable<any>{
    return this.http.get<any>("https://localhost:44348/api/Training/quantitytOfTrainings");
  }
  updateTraining(): Observable<any>{
    return this.http.get<any>("https://localhost:44348/api/Training/updateTraining");
  }

  getExercises(): Observable<any>{
    return this.http.get<any>("https://localhost:44348/api/Training/getExercises");
  }
}
