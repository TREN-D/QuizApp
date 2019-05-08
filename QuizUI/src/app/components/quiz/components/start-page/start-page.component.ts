import { OnInit, Component } from '@angular/core';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';

import { QuizService } from '../../shared/quiz.service';

import { CreateTestResultModel } from '../../entities/test-result/create-test-result.model';
import { TestModel } from 'src/app/components/shared/entities/test.model';
import { UrlModel } from 'src/app/components/shared/entities/url.model';

@Component({
  selector: 'app-start-page',
  templateUrl: './start-page.component.html',
  styleUrls: ['./start-page.component.scss']
})
export class StartPageComponent implements OnInit {
  testModel: TestModel;
  urlModel: UrlModel;
  userName: string;
  constructor(private quizService: QuizService,
              private router: Router,
              private notification: ToastrService) {

  }
  ngOnInit() {
    this.userName = this.urlModel.userName;
  }

  createTestResult() {
    const createTestResultModel = new CreateTestResultModel(this.userName, this.urlModel.id);
    this.quizService.createTestResult(createTestResultModel).subscribe(
      () => {
        this.router.navigateByUrl(`${this.router.url}/test-begin`);
      },
      (error: string) => {
        this.notification.error(error);
      }
    );
  }
}
