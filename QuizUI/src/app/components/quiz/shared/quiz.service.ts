import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { HOST_URL } from 'src/app/config';

import { CreateTestResultModel } from '../entities/test-result/create-test-result.model';
import { UpsertAnswerModel } from '../entities/answer/upsert-answer.model';
import { TestResultModel } from '../entities/test-result/test-result.model';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private quizApiUrl = HOST_URL + 'api/testresult';

  constructor(private http: HttpClient) {

  }

  createTestResult(createTestResultModel: CreateTestResultModel): Observable<void> {
    return this.http.post<void>(this.quizApiUrl, createTestResultModel);
  }

  upsertAnswers(answerModels: UpsertAnswerModel[]): Observable<void> {
    return this.http.post<void>(this.quizApiUrl + '/upsertanswers', answerModels);
  }

  finishTestResult(testResultId: number): Observable<TestResultModel> {
    return this.http.post<TestResultModel>(this.quizApiUrl + '/finishtest', testResultId);
  }
}
