import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { ILoadingState } from '../entities/loading-state.interface';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private loaderSubject = new BehaviorSubject<ILoadingState>({ visible: false, activeRequestsCount: 0 });
  loaderStateChanged = this.loaderSubject.asObservable();

  constructor() { }

  requestStart(): void {
    let currentRequests = this.loaderSubject.value.activeRequestsCount;
    this.loaderSubject.next({visible: true, activeRequestsCount: ++currentRequests});
  }

  requestEnd(): void {
    let currentRequests = this.loaderSubject.value.activeRequestsCount;
    let visible = true;
    if (--currentRequests === 0) {
      visible = false;
    }
    this.loaderSubject.next({visible, activeRequestsCount: currentRequests});
  }
}
