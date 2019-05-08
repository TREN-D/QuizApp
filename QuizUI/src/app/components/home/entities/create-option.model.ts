export class CreateOptionModel {
  questionId: number;
  optionText: string;
  isCorrect: boolean;

  constructor(questionId: number, optionText: string, isCorrect: boolean) {
    this.optionText = optionText;
    this.questionId = questionId;
    this.isCorrect = isCorrect;
  }
}
