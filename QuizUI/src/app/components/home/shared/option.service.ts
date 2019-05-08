import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { HOST_URL } from 'src/app/config';
import { PATCH_CONTENT_TYPE } from 'src/app/http-config';

import { CreateOptionModel } from '../entities/create-option.model';
import { OptionModel } from '../../shared/entities/option.model';
import { IPatchEntity } from '../../shared/entities/patch-entity.interface';

@Injectable({
  providedIn: 'root'
})
export class OptionService {
  private optionApiUrl = HOST_URL + 'api/option/';

  constructor(private http: HttpClient) {

  }

  create(createOptionModel: CreateOptionModel): Observable<OptionModel> {
    return this.http.post<OptionModel>(this.optionApiUrl, createOptionModel);
  }

  delete(id: number) {
    return this.http.delete(this.optionApiUrl + id);
  }

  patch(patchedData: IPatchEntity[], id: number): Observable<OptionModel> {
    return this.http.patch<OptionModel>(this.optionApiUrl + id, patchedData, { headers: PATCH_CONTENT_TYPE });
  }
}
