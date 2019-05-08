import { QuestionTypes } from 'src/app/components/shared/entities/question-type.enum';
import { OptionModel } from './option.model';

export class QuestionModel {
  id: number;
  testId: number;
  questionText: string;
  scoreForAnswer: number;
  questionType: QuestionTypes;
  options: OptionModel[];
  isCorrect: boolean;

  constructor() {
    this.isCorrect = false;
  }
}
