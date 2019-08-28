import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient) { }

  getProducts(): Observable<any>{
    return this.http.get<any>("https://localhost:44348/api/Diet/getAllProducts");
  }

}
