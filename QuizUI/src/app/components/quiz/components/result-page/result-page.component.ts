import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { flatMap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import { TestService } from 'src/app/components/shared/shared/test.service';
import { UrlService } from 'src/app/components/shared/shared/url.service';

import { QuestionTypes } from 'src/app/components/shared/entities/question-type.enum';
import { TestResultModel } from '../../entities/test-result/test-result.model';
import { TestModel } from 'src/app/components/shared/entities/test.model';
import { QuestionModel } from 'src/app/components/shared/entities/question.model';

@Component({
  selector: 'app-result-page',
  templateUrl: 'result-page.component.html',
  styleUrls: ['result-page.component.scss']
})

export class ResultPageComponent implements OnInit {
  testResult: TestResultModel;
  testModel: TestModel;
  questionTypes = QuestionTypes;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private urlService: UrlService,
              private testService: TestService,
              private notification: ToastrService) {
  }

  ngOnInit() {
    this.testResult = JSON.parse(this.route.snapshot.paramMap.get('testResult'));

    this.route.parent.params.pipe(
      flatMap(
        params => this.urlService.get(params.urlIdentifier)
      ),
      flatMap(
        urlModel => this.testService.getClean(urlModel.testId)
      )
    ).subscribe(
      testModel => {
        this.testModel = testModel;
        this.setCorrectOptions();
      },
      (error: string) => {
        this.notification.error(error);
        this.router.navigateByUrl('404');
      }
    );
  }

  private setCorrectOptions() {
    this.testModel.questions.forEach(q => {
      const answers = this.testResult.answers.filter(a => a.questionId === q.id);
      answers.forEach(a => {
        const option = q.options.find(o => o.id === a.optionId);
        if (option) {
          option.isCorrect = true;
        }
      });
      if (answers.every(a => a.isCorrect)) {
        q.isCorrect = true;
      }
    });
  }

  getTextAnswerValue(question: QuestionModel) {
    return this.testResult.answers.find(a => a.questionId === question.id).userAnswer;
  }
}
