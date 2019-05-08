import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { flatMap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import { UrlService } from '../shared/shared/url.service';
import { TestService } from '../shared/shared/test.service';

import { UrlModel } from '../shared/entities/url.model';
import { TestModel } from '../shared/entities/test.model';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss']
})
export class QuizComponent implements OnInit {

  urlModel: UrlModel;
  testModel: TestModel;
  constructor(private route: ActivatedRoute,
              private urlService: UrlService,
              private testService: TestService,
              private notification: ToastrService,
              private router: Router) { }

  ngOnInit() {
    this.route.params.pipe(
      flatMap(
        params => this.urlService.get(params.urlIdentifier)
      ),
      flatMap(
        urlModel => {
          this.urlModel = urlModel;
          return this.testService.getClean(urlModel.testId);
        }
      )
    ).subscribe(
      testModel => {
        this.testModel = testModel;
      },
      (error: string) => {
        this.notification.error(error);
        this.router.navigateByUrl('404');
      }
    );
  }

  onActivate(loadedChild: any) {
    loadedChild.urlModel = this.urlModel;
    loadedChild.testModel = this.testModel;
  }
}
