import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SlotsListComponent } from './components/slots-list/slots-list.component';

const routes: Routes = [
  {
    path: '',
    component: SlotsListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SlotsRoutingModule { }
