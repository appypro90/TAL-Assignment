import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TimeSlot } from 'src/app/shared/models/timeSlot.model';
import { DataService } from 'src/app/shared/services/data.service';
import { ReadService } from 'src/app/shared/services/read.service';
import { UpdateService } from 'src/app/shared/services/update.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TimeSlotService extends DataService {

  constructor(public readService: ReadService,
    public updateService: UpdateService,
    public http: HttpClient) {
    super(readService, updateService, `${environment.apiBaseUri}/TimeSlots`);
  }

  getTimeSlots = (): Observable<TimeSlot[]> =>
    this.readService.readAll<TimeSlot[]>(null, this.url)

  bookTimeSlot = (timeSlot: string): Observable<string> =>
    this.updateService.Update<string, { startDate: string }>({ startDate: timeSlot }, this.url)
}
