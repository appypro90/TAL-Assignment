import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { TimeSlot } from 'src/app/shared/models/timeSlot.model';
import { BookTimeSlotsResult, GetTimeSlotsResult } from 'src/app/store/timeSlots/actions/timeSlot.actions';

@Component({
  selector: 'app-slots-list',
  templateUrl: './slots-list.component.html',
  styleUrls: ['./slots-list.component.scss']
})
export class SlotsListComponent implements OnInit {

  public timeSlots: Observable<TimeSlot[]>;
  public isLoading: Observable<boolean>;

  constructor(public store: Store) {
    this.timeSlots = this.store.select(state => state.timeSlot.items);

    this.isLoading = this.store.select(state => state.temperature.isLoading);
  }

  ngOnInit(): void {
    this.store.dispatch(new GetTimeSlotsResult());
  }

  bookTIme = (timeSlot: TimeSlot) => {
    this.store.dispatch(new BookTimeSlotsResult(timeSlot.startTime.toString()));
  }
}
