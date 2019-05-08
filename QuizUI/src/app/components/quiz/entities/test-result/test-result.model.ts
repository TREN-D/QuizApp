import { AnswerModel } from '../answer/answer.model';

export class TestResultModel {
  diffScore: number;
  timeTestPassed: number;
  userName: string;

  answers: AnswerModel[];
}
