import { HttpErrorResponse } from '@angular/common/http';
import { IHttpError } from '../entities/http-error.interface';

export const handleRequestError = (error: any): string => {
  let errorMessage = error;
  if (error instanceof HttpErrorResponse) {
    if (typeof error.error === 'string') {
      errorMessage = error.error;
    } else {
      const httpError: IHttpError = error.error;
      errorMessage = httpError.message || httpError.error_description;
    }
  }
  return errorMessage;
};
