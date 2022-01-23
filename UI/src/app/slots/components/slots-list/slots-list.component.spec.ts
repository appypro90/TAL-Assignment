import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { TimeSlotState } from 'src/app/store/timeSlots/state/timeSlot.state';

import { SlotsListComponent } from './slots-list.component';

describe('SlotsListComponent', () => {
  let component: SlotsListComponent;
  let fixture: ComponentFixture<SlotsListComponent>;
  let store: Store;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientModule,
        NgxsModule.forRoot([TimeSlotState]),
      ],
      declarations: [SlotsListComponent]
    })
      .compileComponents();
    store = TestBed.inject(Store);
    store.reset({
      ...store.snapshot(),
      timeSlot: {
        items: [],
        isLoading: false
      }
    });
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SlotsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should update store on init', () => {
    component.ngOnInit();
    expect(store.selectSnapshot(state => state.timeSlot.isLoading)).toEqual(true);
  });
});
