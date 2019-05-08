import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { flatMap } from 'rxjs/operators';

import { PageEvent } from '@angular/material/paginator';

import { ToastrService } from 'ngx-toastr';

import { QuestionService } from '../../shared/question.service';
import { TestService } from 'src/app/components/shared/shared/test.service';

import { CreateQuestionModel } from '../../entities/create-question.model';
import { QuestionModel } from '../../../shared/entities/question.model';
import { IPatchEntity } from 'src/app/components/shared/entities/patch-entity.interface';
import { TestModel } from 'src/app/components/shared/entities/test.model';

import { getPatchedData } from 'src/app/components/shared/helpers/json-patch-helper';

@Component({
  selector: 'app-edit-test',
  templateUrl: './edit-test.component.html',
  styleUrls: ['./edit-test.component.scss']
})
export class EditTestComponent implements OnInit {
  defaultPageSize = 5;
  startIndex = 0;
  endIndex = this.defaultPageSize;
  maxTestTimeValue = 120;
  testModel = new TestModel();

  constructor(private testService: TestService,
              private questionService: QuestionService,
              private notification: ToastrService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.pipe(
      flatMap(
        params => this.testService.get(params.id)
      )
    ).subscribe(testModel => {
      this.testModel = testModel;
    });
  }

  pageChange(event?: PageEvent) {
    if (event) {
      this.startIndex = event.pageIndex * event.pageSize;
      this.endIndex = event.pageIndex * event.pageSize + event.pageSize;
    }
  }

  onTestTimeKeyDown(event: any) {
    // 95, < 106 corresponds to Numpad 0 through 9;
    // 47, < 58 corresponds to 0 through 9 on the Number Row;
    // 8 corresponds to Backspace.
    if (!((event.keyCode > 95 && event.keyCode < 106)
      || (event.keyCode > 47 && event.keyCode < 58)
      || event.keyCode === 8)) {
      return false;
    }
    const newValue = this.testModel.testTime + event.key;
    if (Number(newValue) > this.maxTestTimeValue) {
      return false;
    }
  }

  updateTest() {
    const patchedTest = new Array<IPatchEntity>();
    const patchedTestDescription = this.getPatchedTestDescription();
    const patchedTestTime = this.getPatchedTestTime();

    patchedTest.push(patchedTestDescription, patchedTestTime);
    this.testService.patch(patchedTest, this.testModel.id).subscribe(
      (data: TestModel) => this.testModel = { ...data },
      (error: string) => this.notification.error(error)
    );
  }

  getIndex(index: number): number {
    return this.startIndex + index + 1;
  }

  createQuestion() {
    const index = this.testModel.questions.length + 1;
    const questionText = `Untitled question #${index}`;

    const createQustionModel = new CreateQuestionModel(this.testModel.id, questionText);
    this.questionService.create(createQustionModel).subscribe(
      (data: QuestionModel) => {
        this.testModel.questions.push(data);
      },
      (error: string) => this.notification.error(error)
    );
  }

  deleteQuestion(questionId: number) {
    this.testModel.questions = this.testModel.questions.filter(q => q.id !== questionId);
  }

  private getPatchedTestDescription(): IPatchEntity {
    const propertyName = 'TestDescription';
    const patchedValue = this.testModel.testDescription || 'Invalid text description';
    return getPatchedData(patchedValue, propertyName);
  }

  private getPatchedTestTime(): IPatchEntity {
    const propertyName = 'TestTime';
    const patchedValue = this.testModel.testTime > 0 ? this.testModel.testTime.toString() : null;
    return getPatchedData(patchedValue, propertyName);
  }
}
