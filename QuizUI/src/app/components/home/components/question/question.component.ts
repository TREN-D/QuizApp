import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

import { QuestionService } from '../../shared/question.service';

import { IPatchEntity } from 'src/app/components/shared/entities/patch-entity.interface';
import { QuestionModel } from '../../../shared/entities/question.model';
import { QuestionTypes } from 'src/app/components/shared/entities/question-type.enum';

import { getPatchedData } from 'src/app/components/shared/helpers/json-patch-helper';
import { OptionModel } from '../../../shared/entities/option.model';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})

export class QuestionComponent implements OnInit {

  @Input() question: QuestionModel;
  @Input() index: number;

  @Output() delete = new EventEmitter<number>();

  questionToUpdate: QuestionModel;
  questionTypes = QuestionTypes;
  questionTypeKeys: string[];

  constructor(private questionService: QuestionService,
              private notification: ToastrService) {

  }

  ngOnInit() {
    this.questionToUpdate = { ...this.question };
    this.setQuestionTypeKeys();
  }

  onQuestionScoreKeyDown(event: any) {
    // 95, < 106 corresponds to Numpad 0 through 9;
    // 47, < 58 corresponds to 0 through 9 on the Number Row;
    // 8 corresponds to Backspace.
    if (!((event.keyCode > 95 && event.keyCode < 106)
      || (event.keyCode > 47 && event.keyCode < 58)
      || event.keyCode === 8)) {
      return false;
    }
  }

  updateQuestion() {
    let patchedQuestion = new Array<IPatchEntity>();
    const patchedQuestionText = this.getPatchedQuestionText();
    const patchedQuestionScore = this.getPatchedQuestionScore();
    patchedQuestion.push(patchedQuestionScore, patchedQuestionText);

    patchedQuestion = patchedQuestion.filter(q => !!q);
    this.questionService.patch(patchedQuestion, this.question.id).subscribe(
      (data: QuestionModel) => {
        this.question = data;
        this.questionToUpdate = { ...this.question };
      },
      (error: string) => this.notification.error(error)
    );
  }

  updateQuestionType() {
    const patchedQuestionType = this.getPatchedQuestionType();
    if (!!patchedQuestionType) {
      this.questionService.patch([patchedQuestionType], this.question.id).subscribe(
        (data: QuestionModel) => {
          this.question = data;
          this.questionToUpdate = { ...this.question };
        },
        (error: string) => this.notification.error(error)
      );
    }
  }

  deleteQuestion() {
    this.questionService.delete(this.questionToUpdate.id).subscribe(
      () => {
        this.delete.emit(this.questionToUpdate.id);
      },
      (error: string) => {
        this.notification.error(error);
      }
    );
  }

  addOption(option: OptionModel) {
    this.question.options.push(option);
    this.questionToUpdate.options = this.question.options;
  }

  deleteOption(optionId: number) {
    this.question.options = this.question.options.filter(o => o.id !== optionId);
    this.questionToUpdate.options = [...this.question.options];
  }

  private getPatchedQuestionText(): IPatchEntity {
    const propertyName = 'QuestionText';
    if (this.question.questionText === this.questionToUpdate.questionText) {
      return null;
    }
    const patchedValue = this.questionToUpdate.questionText || 'Invalid text description';
    const patchedData = getPatchedData(patchedValue, propertyName);
    return patchedData;
  }

  private getPatchedQuestionScore(): IPatchEntity {
    const propertyName = 'ScoreForAnswer';
    if (this.question.scoreForAnswer === this.questionToUpdate.scoreForAnswer) {
      return null;
    }
    const patchedValue = this.questionToUpdate.scoreForAnswer || 0;
    const patchedData = getPatchedData(patchedValue.toString(), propertyName);
    return patchedData;
  }

  private getPatchedQuestionType(): IPatchEntity {
    const propertyName = 'QuestionType';
    if (this.question.questionType === this.questionToUpdate.questionType) {
      return null;
    }
    const patchedValue = this.questionToUpdate.questionType.toString();
    const patchedData = getPatchedData(patchedValue, propertyName);
    return patchedData;
  }

  private setQuestionTypeKeys(): void {
    const keys = Object.keys(this.questionTypes);
    this.questionTypeKeys = keys.slice(keys.length / 2, keys.length);
  }
}
