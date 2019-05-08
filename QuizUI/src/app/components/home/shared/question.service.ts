import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { HOST_URL } from 'src/app/config';
import { PATCH_CONTENT_TYPE } from 'src/app/http-config';

import { CreateQuestionModel } from '../entities/create-question.model';
import { QuestionModel } from '../../shared/entities/question.model';
import { IPatchEntity } from '../../shared/entities/patch-entity.interface';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  private questionApiUrl = HOST_URL + 'api/question/';

  constructor(private http: HttpClient) {

  }

  create(createQuestionModel: CreateQuestionModel): Observable<QuestionModel> {
    return this.http.post<QuestionModel>(this.questionApiUrl, createQuestionModel);
  }

  patch(patchedData: IPatchEntity[], id: number): Observable<QuestionModel> {
    return this.http.patch<QuestionModel>(this.questionApiUrl + id, patchedData, { headers: PATCH_CONTENT_TYPE });
  }

  delete(id: number) {
    return this.http.delete(this.questionApiUrl + id);
  }
}
