import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { LoadingService } from '../../shared/loading.service';

import { ILoadingState } from '../../entities/loading-state.interface';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit, OnDestroy {
  visible = false;
  private subscription: Subscription;

  constructor(private loadingService: LoadingService) {
   }

  ngOnInit() {
    this.subscription = this.loadingService.loaderStateChanged
    .subscribe((state: ILoadingState) => {
      this.visible = state.visible;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
