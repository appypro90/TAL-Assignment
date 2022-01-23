import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReadService {

  constructor(private http: HttpClient) { }

  readAll<T>(queryObject: any, url: string): Observable<T> {
    let params = new HttpParams();
    if (!!queryObject) {
      const paramKeys = Object.getOwnPropertyNames(queryObject);
      paramKeys.forEach(paramKey => {
        params = params.set(paramKey, queryObject[paramKey]);
      });
    }

    return this.http
      .get(url, { params })
      .pipe(map((res: any) => {
          return res as T;
      }));
  }
}
