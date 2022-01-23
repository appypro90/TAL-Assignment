import { TimeSlot } from 'src/app/shared/models/timeSlot.model';

export class GetTimeSlotsResult {
    static readonly type = '[TimeSlots] Get';

    constructor() { }
}

export class PopulateTimeSlotsResult {
    static readonly type = '[TimeSlots] Populate';

    constructor(public payload: TimeSlot[]) {
    }
}

export class BookTimeSlotsResult {
  static readonly type = '[TimeSlots] Book';

  constructor(public payload: string) {
  }
}
