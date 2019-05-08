import { Component, OnInit, ViewChild } from '@angular/core';

import { tap, flatMap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

import { TestService } from 'src/app/components/shared/shared/test.service';

import { getPaginatedUrl } from 'src/app/components/shared/helpers/pagination-helper';

import { CreateTestModel } from '../../entities/create-test.model';
import { TestModel } from 'src/app/components/shared/entities/test.model';

@Component({
  selector: 'app-own-tests',
  templateUrl: './own-tests.component.html',
  styleUrls: ['./own-tests.component.scss']
})
export class OwnTestsComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;

  defaultPageSize = 5;
  testsCount = 0;
  exist = true;
  dataSource = new MatTableDataSource<TestModel>();
  displayedColumns = ['Index', 'TestDescription', 'TestTime', 'Questions', 'TestEdit', 'CreateUrl', 'TestRemove'];

  constructor(private testService: TestService,
              private notification: ToastrService) {
  }

  ngOnInit() {
    this.configurePaginator();
  }

  createTest() {
    const index = this.testsCount + 1;
    const testDescription = `Untitled test #${index}`;
    const createdTestModel = new CreateTestModel(testDescription);
    this.testService.create(createdTestModel).subscribe(
      () => {
        this.paginator.page.emit();
      },
      (error: string) => this.notification.error(error)
    );
  }

  onDelete(element: TestModel) {
    this.testService.delete(element.id).subscribe(
      () => {
        this.paginator.page.emit();
      },
      (err: string) => {
        this.notification.error(err);
      }
    );
  }

  private configurePaginator() {
    this.paginator.pageSize = this.defaultPageSize;

    this.paginator.page.pipe(
      flatMap(
        () => this.testService.getOwnWithPagination(getPaginatedUrl(this.paginator))
      ),
      tap(
        (testModels) => {
          if (testModels && (testModels.length === 0)) {
            this.exist = false;
            this.dataSource.data = null;
          } else {
            this.dataSource.data = testModels;
            this.exist = true;
          }
        }
      ),
      flatMap(
        () => this.testService.getOwnTestCount()
      )
    ).subscribe(
      (amount) => {
        this.testsCount = amount;
      },
      (err: string) => {
        this.notification.error(err);
      }
    );
    this.paginator.page.emit();
  }
}
