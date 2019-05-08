import { OnInit, Component, Input, Output, EventEmitter } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

import { OptionService } from '../../shared/option.service';

import { CreateOptionModel } from '../../entities/create-option.model';
import { OptionModel } from '../../../shared/entities/option.model';
import { IPatchEntity } from 'src/app/components/shared/entities/patch-entity.interface';
import { QuestionTypes } from 'src/app/components/shared/entities/question-type.enum';

import { getPatchedData } from 'src/app/components/shared/helpers/json-patch-helper';
import { MatRadioChange } from '@angular/material/radio';

@Component({
  selector: 'app-option',
  templateUrl: './option.component.html',
  styleUrls: ['./option.component.scss']
})

export class OptionComponent implements OnInit {
  @Input() questionType: QuestionTypes;
  @Input() questionId: number;
  @Input() options: OptionModel[];

  @Output() delete = new EventEmitter<number>();
  @Output() add = new EventEmitter<OptionModel>();
  optionsToUpdate = new Array<OptionModel>();
  questionTypes = QuestionTypes;

  constructor(private optionService: OptionService,
              private notification: ToastrService) {

  }

  ngOnInit() {

  }

  createOption() {
    const index = this.options.length + 1;
    const optionText = `Untitled option #${index}`;
    const isCorrect = (this.questionType === QuestionTypes.text) || !(this.options.some(o => o.isCorrect));

    const createOptionModel = new CreateOptionModel(this.questionId, optionText, isCorrect);
    this.optionService.create(createOptionModel).subscribe(
      (optionModel) => {
        this.add.emit(optionModel);
      },
      (error: string) => {
        this.notification.error(error);
      }
    );
  }

  onOptionChange(option: OptionModel) {
    const optionIndex = this.optionsToUpdate.findIndex(o => o.id === option.id);
    if (optionIndex >= 0) {
      this.optionsToUpdate[optionIndex] = option;
    } else {
      this.optionsToUpdate.push(option);
    }
  }

  updateOptions() {
    for (const option of this.optionsToUpdate) {
      const data = new Array<IPatchEntity>();
      const patchedOptionText = this.getPatchedOptionText(option);
      const patchedIsCorrect = this.getPatchedIsCorrect(option);
      data.push(patchedOptionText, patchedIsCorrect);

      this.optionService.patch(data, option.id).subscribe(
        (patchedOption) => {
          const optionIndex = this.options.findIndex(o => o.id === patchedOption.id);
          this.options[optionIndex] = patchedOption;
          this.optionsToUpdate = [];
        },
        (error: string) => this.notification.error(error)
      );
    }
  }

  deleteOption(option: OptionModel) {
    this.optionService.delete(option.id).subscribe(
      () => {
        this.options = this.options.filter(o => o.id !== option.id);
        this.optionsToUpdate = this.optionsToUpdate.filter(o => o.id !== option.id);

        // If we remove isCorrect radiobutton option, first option automatically sets correct
        if (this.questionType === QuestionTypes.radio && option.isCorrect && this.options.length) {
          this.options[0].isCorrect = true;
          this.onOptionChange(this.options[0]);
          this.updateOptions();
        }
        this.delete.emit(option.id);
      },
      (error: string) => this.notification.error(error)
    );
  }

  onRadioChange(option: OptionModel, event: Event) {
    if (event instanceof MatRadioChange) {
      const checkedOption = this.options.find(o => o.isCorrect);
      if (checkedOption) {
        checkedOption.isCorrect = false;
        this.onOptionChange(checkedOption);
      }

      const newCheckedOption = this.options.find(o => o.id === option.id);
      newCheckedOption.isCorrect = true;
      this.onOptionChange(newCheckedOption);
    }
  }

  private getPatchedOptionText(option: OptionModel): IPatchEntity {
    const propertyName = 'OptionText';
    const patchedValue = option.optionText || 'Invalid option text';
    const data = getPatchedData(patchedValue, propertyName);
    return data;
  }

  private getPatchedIsCorrect(option: OptionModel): IPatchEntity {
    const propertyName = 'IsCorrect';
    const patchedValue = option.isCorrect.toString();
    const data = getPatchedData(patchedValue, propertyName);
    return data;
  }
}
