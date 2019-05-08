import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HOST_URL } from 'src/app/config';
import { CreateUrlModel } from '../../home/entities/create-url.model';
import { Observable } from 'rxjs';
import { UrlModel } from '../entities/url.model';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  private urlApiUrl = HOST_URL + 'api/url/';

  constructor(private http: HttpClient) {

  }

  create(createdUrlModel: CreateUrlModel): Observable<UrlModel> {
    return this.http.post<UrlModel>(this.urlApiUrl, createdUrlModel);
  }

  get(identifier: string): Observable<UrlModel> {
    return this.http.get<UrlModel>(this.urlApiUrl + identifier);
  }
}
