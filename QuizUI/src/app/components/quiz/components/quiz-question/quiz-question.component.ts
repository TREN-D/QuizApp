import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { flatMap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import { QuizService } from '../../shared/quiz.service';

import { QuestionTypes } from 'src/app/components/shared/entities/question-type.enum';
import { OptionModel } from 'src/app/components/shared/entities/option.model';
import { QuestionModel } from 'src/app/components/shared/entities/question.model';
import { UpsertAnswerModel } from '../../entities/answer/upsert-answer.model';

@Component({
  selector: 'app-quiz-question',
  templateUrl: 'quiz-question.component.html',
  styleUrls: ['quiz-question.component.scss']
})

export class QuizQuestionComponent implements OnInit, OnChanges {
  @Input() currentQuestionIndex: number;
  @Input() totalQuestionsAmount: number;
  @Input() testResultId: number;
  @Input() currentQuestion: QuestionModel;
  @Input() currentQuestionPassedAnswers: UpsertAnswerModel[];

  @Output() nextQuestionEmit = new EventEmitter<UpsertAnswerModel[]>();
  @Output() previousQuestionEmit = new EventEmitter<UpsertAnswerModel[]>();

  textTypeAnswer = '';
  questionTypes = QuestionTypes;
  constructor(private quizService: QuizService,
              private notification: ToastrService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnChanges() {
    this.textTypeAnswer = '';

    if (this.currentQuestionPassedAnswers && this.currentQuestionPassedAnswers.length > 0) {
      if (this.currentQuestion.questionType === QuestionTypes.text) {
        this.textTypeAnswer = this.currentQuestionPassedAnswers[0].userAnswer;
      } else {
        this.currentQuestionPassedAnswers.forEach(existedAnswer => {
          this.currentQuestion.options.find(o => o.id === existedAnswer.optionId && o.questionId === existedAnswer.questionId)
            .isCorrect = true;
        });
      }
    }
    this.currentQuestionPassedAnswers = new Array<UpsertAnswerModel>();
  }

  ngOnInit() {

  }

  onNext() {
    this.setAlreadyPassedAnswers();
    this.quizService.upsertAnswers(this.currentQuestionPassedAnswers).subscribe(
      () => this.nextQuestionEmit.emit(this.currentQuestionPassedAnswers),
      (error: string) => this.notification.error(error)
    );
  }

  onPrevious() {
    this.setAlreadyPassedAnswers();
    this.quizService.upsertAnswers(this.currentQuestionPassedAnswers).subscribe(
      () => this.previousQuestionEmit.emit(this.currentQuestionPassedAnswers),
      (error: string) => this.notification.error(error)
    );
  }

  onFinish() {
    this.setAlreadyPassedAnswers();
    this.quizService.upsertAnswers(this.currentQuestionPassedAnswers).pipe(
      flatMap(() => this.quizService.finishTestResult(this.testResultId))
    ).subscribe(
      testResult => {
        this.router.navigate(['../result', {testResult: JSON.stringify(testResult)}], {relativeTo: this.route});
      },
      (error: string) => this.notification.error(error)
    );
  }

  onRadioChange(optionModel: OptionModel) {
    OptionModel.clearAllCorrectOptions(this.currentQuestion.options);
    this.currentQuestion.options.find(o => o.id === optionModel.id).isCorrect = true;
  }

  private setAlreadyPassedAnswers() {
    if (this.currentQuestion.questionType === QuestionTypes.text) {
      this.currentQuestionPassedAnswers.push(new UpsertAnswerModel(this.textTypeAnswer, this.testResultId, this.currentQuestion.id));
    } else {
      if (this.currentQuestion.options.some(o => o.isCorrect)) {
        this.currentQuestion.options.forEach(o => {
          if (o.isCorrect) {
            this.currentQuestionPassedAnswers.push(new UpsertAnswerModel(o.optionText, this.testResultId, this.currentQuestion.id, o.id));
          }
        });
      } else {
        this.currentQuestionPassedAnswers.push(new UpsertAnswerModel('', this.testResultId, this.currentQuestion.id));
      }
    }
  }
}
