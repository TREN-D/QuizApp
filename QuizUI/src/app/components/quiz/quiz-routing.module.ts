import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { QuizComponent } from './quiz.component';
import { TestBeginComponent } from './components/test-begin/test-begin.component';
import { StartPageComponent } from './components/start-page/start-page.component';
import { ResultPageComponent } from './components/result-page/result-page.component';

const routes: Routes = [
  {
    path: ':urlIdentifier', component: QuizComponent,
    children: [
      {path: '', component: StartPageComponent},
      {path: 'test-begin', component: TestBeginComponent},
      {path: 'result', component: ResultPageComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuizRoutingModule { }
