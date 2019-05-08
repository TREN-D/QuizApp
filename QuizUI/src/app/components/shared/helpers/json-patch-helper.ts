import { IPatchEntity } from '../entities/patch-entity.interface';
import { JsonPatchAction } from '../entities/json-patch.enum';

const jsonPatchObject = (action: JsonPatchAction, targetProperty: string, value: string): IPatchEntity => {
  switch (action) {
    case JsonPatchAction.Replace: {
      const patchData = { op: 'replace', path: `/${targetProperty}`, value };
      return patchData;
    }
    default: {
      // We use patch only for replace right now
      throw new Error('Not implemented logic yet');
    }
  }
};

export const getPatchedData = (patchedValue: string, patchedProperty: string): IPatchEntity => {
  const patchedData = jsonPatchObject(JsonPatchAction.Replace, patchedProperty, patchedValue);
  return patchedData;
};
