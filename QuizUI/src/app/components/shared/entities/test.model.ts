import { QuestionModel } from './question.model';

export class TestModel {
  id: number;
  createdBy: number;
  testTime: number;
  testDescription: string;
  questions: QuestionModel[];

  constructor() {
    this.questions = new Array<QuestionModel>();
  }
}
