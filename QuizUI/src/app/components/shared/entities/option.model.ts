export class OptionModel {
  id: number;
  questionId: number;
  optionText: string;
  isCorrect: boolean;

  static clearAllCorrectOptions(options: OptionModel[]) {
    options.forEach(o => o.isCorrect = false);
  }
}
