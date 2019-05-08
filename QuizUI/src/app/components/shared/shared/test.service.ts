import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { PATCH_CONTENT_TYPE } from 'src/app/http-config';
import { HOST_URL } from 'src/app/config';

import { IPatchEntity } from 'src/app/components/shared/entities/patch-entity.interface';
import { CreateTestModel } from '../../home/entities/create-test.model';
import { TestModel } from '../entities/test.model';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  private testApiUrl = HOST_URL + 'api/test/';

  constructor(private http: HttpClient) { }

  create(createdTestModel: CreateTestModel): Observable<TestModel> {
    return this.http.post<TestModel>(this.testApiUrl, createdTestModel);
  }

  patch(patchedData: IPatchEntity[], id: number) {
    return this.http.patch(this.testApiUrl + id, patchedData, { headers: PATCH_CONTENT_TYPE });
  }

  delete(id: number) {
    return this.http.delete(this.testApiUrl + id);
  }

  getOwnWithPagination(paginatedUrl: string): Observable<TestModel[]> {
    const url = this.testApiUrl + `admin/own${paginatedUrl}`;
    return this.http.get<TestModel[]>(url);
  }

  get(id: number): Observable<TestModel> {
    return this.http.get<TestModel>(this.testApiUrl + id);
  }

  getClean(id: number): Observable<TestModel> {
    return this.http.get<TestModel>(this.testApiUrl + `clean/${id}`);
  }

  getOwnTestCount(): Observable<number> {
    const url = this.testApiUrl + 'admin/own/count';
    return this.http.get<number>(url);
  }
}
