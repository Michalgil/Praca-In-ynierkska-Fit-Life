import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { Dimensions } from '../../Models/dimensions';

@Injectable()
export class ProgressService {

  constructor(private http: HttpClient) { }

  getCurrentDimensions() : Observable<any>{
    return this.http.get("https://localhost:44348/api/Diet/getDimensions");
  }

}
