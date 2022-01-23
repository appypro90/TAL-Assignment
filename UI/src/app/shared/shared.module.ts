import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MatChipsModule } from '@angular/material/chips';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MatChipsModule
  ],
  exports: [
    FlexLayoutModule,
    MatChipsModule
  ]
})
export class SharedModule { }
