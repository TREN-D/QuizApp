import { OnInit, Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { flatMap } from 'rxjs/operators';

import { ClipboardService } from 'ngx-clipboard';
import { ToastrService } from 'ngx-toastr';

import { UrlService } from 'src/app/components/shared/shared/url.service';
import { TestService } from 'src/app/components/shared/shared/test.service';

import { CreateUrlModel } from 'src/app/components/home/entities/create-url.model';
import { TestModel } from 'src/app/components/shared/entities/test.model';

@Component({
  selector: 'app-create-url',
  templateUrl: './create-url.component.html',
  styleUrls: ['./create-url.component.scss']
})
export class CreateUrlComponent implements OnInit {
  @ViewChild('form') form: NgForm;
  minDate = new Date();
  testModel = new TestModel();
  createdUrlModel = new CreateUrlModel();
  createdUrlIdentifier = '';
  constructor(private testService: TestService,
              private urlService: UrlService,
              private route: ActivatedRoute,
              private router: Router,
              private notification: ToastrService,
              private clipboardService: ClipboardService) {

  }

  ngOnInit() {
    this.route.params.pipe(
      flatMap(
        params => this.testService.get(params.testId)
      )
    ).subscribe(
      testModel => this.testModel = testModel,
      (error: string) => {
        this.notification.error(error);
        this.router.navigateByUrl('/home');
      }
    );
  }

  createUrl() {
    if (this.form.valid) {
      this.createdUrlModel.testId = this.testModel.id;
      this.urlService.create(this.createdUrlModel).subscribe(
        (urlModel) => {
          this.createdUrlIdentifier = `${window.location.origin}/quiz/${urlModel.identifier}`;
          this.clipboardService.copyFromContent(this.createdUrlIdentifier);
          this.notification.success('Url copied to the clipboard.');
        },
        (err: string) => this.notification.error(err));
    } else {
      this.notification.error('Invalid form!');
    }
  }

  onNumberOfRunsKeyDown(event: any) {
    // 95, < 106 corresponds to Numpad 0 through 9;
    // 47, < 58 corresponds to 0 through 9 on the Number Row;
    // 8 corresponds to Backspace.
    if (!((event.keyCode > 95 && event.keyCode < 106)
      || (event.keyCode > 47 && event.keyCode < 58)
      || event.keyCode === 8)) {
      return false;
    }
  }

}
