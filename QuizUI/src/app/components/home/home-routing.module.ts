import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { OwnTestsComponent } from './components/own-tests/own-tests.component';
import { EditTestComponent } from './components/edit-test/edit-test.component';
import { HomeComponent } from './home.component';
import { CreateUrlComponent } from './components/create-url/create-url.component';

const routes: Routes =  [
  {path: '', component: HomeComponent,
    children: [
      {path: '', component: OwnTestsComponent},
      {path: 'edit-test/:id', component: EditTestComponent},
      {path: 'create-url/:testId', component: CreateUrlComponent}
    ]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
