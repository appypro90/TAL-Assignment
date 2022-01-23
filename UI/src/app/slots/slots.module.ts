import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SlotsListComponent } from './components/slots-list/slots-list.component';
import { HttpClientModule } from '@angular/common/http';
import { SlotsRoutingModule } from './slots.routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    SlotsListComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    SlotsRoutingModule,
    SharedModule
  ]
})
export class SlotsModule { }
