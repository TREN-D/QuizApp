export class CreateQuestionModel {
  testId: number;
  questionText: string;

  constructor(testId: number, questionText: string) {
    this.questionText = questionText;
    this.testId = testId;
  }
}
