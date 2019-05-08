export class UpsertAnswerModel {
  testResultId: number;
  userAnswer: string;
  questionId: number;
  optionId: number;

  constructor(userAnswer: string, testResultId: number, questionId: number, optionId?: number) {
    this.userAnswer = userAnswer;
    this.optionId = optionId;
    this.questionId = questionId;
    this.testResultId = testResultId;
  }
}
