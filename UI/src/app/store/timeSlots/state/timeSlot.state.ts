import { Injectable } from '@angular/core';
import { State, Selector, Action, StateContext } from '@ngxs/store';
import { tap } from 'rxjs/operators';
import { Store } from '@ngxs/store';
import { TimeSlot } from 'src/app/shared/models/timeSlot.model';
import { TimeSlotService } from 'src/app/slots/services/timeSlot.service';
import { BookTimeSlotsResult, GetTimeSlotsResult, PopulateTimeSlotsResult } from '../actions/timeSlot.actions';

export class TimeSlotStateModel {
    items: TimeSlot[] = [];
    isLoading = false;
}

@State<TimeSlotStateModel>({
    name: 'timeSlot',
    defaults: {
        items: [],
        isLoading: false
    }
})

@Injectable()
export class TimeSlotState {

    constructor(private timeSlotService: TimeSlotService, private store: Store) {
    }

    @Selector()
    static getTimeSlots(state: TimeSlotStateModel): TimeSlot[] {
        return state.items;
    }


    @Action(GetTimeSlotsResult)
    GetTimeSlotsResult({ getState, setState }: StateContext<TimeSlotStateModel>): void {
        const state = getState();
        setState({
            ...state,
            items: [],
            isLoading: true,
        });
        this.timeSlotService.getTimeSlots().pipe(tap((result: TimeSlot[]) => {
            this.store.dispatch(new PopulateTimeSlotsResult(result));
        })).subscribe();
    }

    @Action(PopulateTimeSlotsResult)
    PopulateTimeSlotsResult({ getState, patchState }: StateContext<TimeSlotStateModel>,
                            { payload }: PopulateTimeSlotsResult): void {
        const state = getState();
        patchState({
            ...state,
            items: [...state.items, ...payload],
            isLoading: false,
        });
    }

    @Action(BookTimeSlotsResult)
    BookTimeSlotsResult({ getState, setState }: StateContext<TimeSlotStateModel>,  { payload }: BookTimeSlotsResult): void {
        const state = getState();
        setState({
            ...state,
            isLoading: true,
        });
        this.timeSlotService.bookTimeSlot(payload).pipe(tap((result: string) => {
            this.store.dispatch(new GetTimeSlotsResult());
        })).subscribe();
    }
}
