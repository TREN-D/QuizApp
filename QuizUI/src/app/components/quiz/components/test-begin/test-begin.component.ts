import { Component, OnInit } from '@angular/core';

import { QuestionModel } from 'src/app/components/shared/entities/question.model';
import { TestModel } from 'src/app/components/shared/entities/test.model';
import { UrlModel } from 'src/app/components/shared/entities/url.model';
import { UpsertAnswerModel } from '../../entities/answer/upsert-answer.model';
import { QuestionTypes } from 'src/app/components/shared/entities/question-type.enum';

@Component({
  selector: 'app-test-begin',
  templateUrl: './test-begin.component.html'
})
export class TestBeginComponent implements OnInit {
  testModel: TestModel;
  urlModel: UrlModel;
  currentQuestion: QuestionModel;
  currentQuestionIndex = 0;
  allUserAnswers = new Array<UpsertAnswerModel>();
  currentQuestionPassedAnswers: UpsertAnswerModel[];
  constructor() {

  }

  ngOnInit() {
    this.currentQuestion = this.testModel.questions[this.currentQuestionIndex];
  }

  onNext(answerModels: UpsertAnswerModel[]) {
    this.fillAllUserAnswers(answerModels);
    if (this.currentQuestionIndex < this.testModel.questions.length - 1) {
      this.currentQuestionIndex++;
      this.currentQuestion = this.testModel.questions[this.currentQuestionIndex];

      this.fillCurrentQuestionPassedAnswers();
    }
  }

  onPrevious(answerModels: UpsertAnswerModel[]) {
    this.fillAllUserAnswers(answerModels);
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
      this.currentQuestion = this.testModel.questions[this.currentQuestionIndex];

      this.fillCurrentQuestionPassedAnswers();
    }
  }

  private fillCurrentQuestionPassedAnswers() {
    this.currentQuestionPassedAnswers = new Array<UpsertAnswerModel>();
    this.allUserAnswers.forEach(a => {
      if (a.questionId === this.currentQuestion.id) {
        this.currentQuestionPassedAnswers.push(a);
      }
    });
  }

  private fillAllUserAnswers(answerModels: UpsertAnswerModel[]) {
    this.allUserAnswers = this.allUserAnswers.filter(a => a.questionId !== this.currentQuestion.id);
    answerModels.forEach(upsertedAnswer => {
      if ((this.currentQuestion.questionType !== QuestionTypes.text) && !upsertedAnswer.optionId) {

      } else {
        this.allUserAnswers.push(upsertedAnswer);
      }
    });
  }
}
