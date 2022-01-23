import { Observable } from 'rxjs';
import { ReadService } from './read.service';
import { UpdateService } from './update.service';

export class DataService {

  constructor(
    public readService: ReadService,
    public updateService: UpdateService,
    public url: string) {}

  getAll = <T1>(queryObject: any): Observable<T1> => {
    return this.readService.readAll(queryObject, this.url);
  }

  Update = <T1, T2>(requestBody: T2): Observable<T1> => {
    return this.updateService.Update(requestBody, this.url);
  }

}
