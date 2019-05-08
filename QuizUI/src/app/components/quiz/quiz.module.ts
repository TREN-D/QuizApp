import { NgModule } from '@angular/core';
import { QuizRoutingModule } from './quiz-routing.module';

import { MatDividerModule } from '@angular/material/divider';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatTooltipModule } from '@angular/material/tooltip';

import { SharedModule } from '../shared/shared.module';

import { QuizService } from './shared/quiz.service';

import { QuizComponent } from './quiz.component';
import { TestBeginComponent } from './components/test-begin/test-begin.component';
import { StartPageComponent } from './components/start-page/start-page.component';
import { QuizQuestionComponent } from './components/quiz-question/quiz-question.component';
import { ResultPageComponent } from './components/result-page/result-page.component';

@NgModule({
  declarations: [
    QuizComponent,
    TestBeginComponent,
    StartPageComponent,
    QuizQuestionComponent,
    ResultPageComponent
  ],
  imports: [
    QuizRoutingModule,
    SharedModule,
    MatCardModule,
    MatDividerModule,
    MatProgressBarModule,
    MatCheckboxModule,
    MatRadioModule,
    MatTooltipModule
  ],
  providers: [QuizService]
})
export class QuizModule {
  constructor() {

  }
}
