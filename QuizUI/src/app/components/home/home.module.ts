import { NgModule } from '@angular/core';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faTrashAlt, faPencilAlt, faCaretDown, faInfinity,
         faQuestionCircle, faPlusCircle, faArrowCircleLeft,
         faUpload, faMinusCircle, faLink, faExternalLinkAlt } from '@fortawesome/free-solid-svg-icons';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { MatDatepickerModule } from '@angular/material/datepicker';

import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';

import { QuestionService } from './shared/question.service';
import { OptionService } from './shared/option.service';

import { HomeComponent } from './home.component';
import { HeaderComponent } from './components/header/header.component';
import { EditTestComponent } from './components/edit-test/edit-test.component';
import { QuestionComponent } from './components/question/question.component';
import { OwnTestsComponent } from './components/own-tests/own-tests.component';
import { OptionComponent } from './components/option/option.component';
import { CreateUrlComponent } from './components/create-url/create-url.component';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  declarations: [
    HomeComponent,
    HeaderComponent,
    OwnTestsComponent,
    EditTestComponent,
    QuestionComponent,
    OptionComponent,
    CreateUrlComponent
  ],
  imports: [
    SharedModule,
    MatToolbarModule,
    HomeRoutingModule,
    MatMenuModule,
    MatTableModule,
    MatCardModule,
    MatPaginatorModule,
    MatTooltipModule,
    MatExpansionModule,
    MatSelectModule,
    MatDividerModule,
    MatCheckboxModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  providers: [QuestionService, OptionService]
})
export class HomeModule {
  constructor() {
    library.add(faTrashAlt, faPencilAlt, faCaretDown, faInfinity,
                faQuestionCircle, faPlusCircle, faArrowCircleLeft, faUpload,
                faMinusCircle, faLink, faExternalLinkAlt);
  }
}
