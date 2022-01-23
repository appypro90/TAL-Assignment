import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UpdateService {

  constructor(private http: HttpClient) { }

  Update = <T1, T2>(requestBody: T2, url: string, id?: string): Observable<T1> =>
  id && id.length > 0 && id !== '0' ?
  this.http
      .put(url + '/' + id, requestBody)
      .pipe(map((res: any) => {
        return res as T1;
      })) :
      this.http
      .post(url, requestBody)
      .pipe(map((res: any) => {
        return res as T1;
      }))
}
