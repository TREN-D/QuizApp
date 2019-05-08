import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './components/auth/shared/auth.guard';

import { SignInComponent } from './components/auth/components/sign-in/sign-in.component';
import { SignUpComponent } from './components/auth/components/sign-up/sign-up.component';
import { NotFoundComponent } from './components/shared/components/not-found/not-found.component';
import { AuthComponent } from './components/auth/auth.component';

const routes: Routes = [
  {path: '', redirectTo: '/auth/sign-in', pathMatch: 'full'},
  {path: 'auth', component: AuthComponent,
    children: [
      {path: '', redirectTo: 'sign-in', pathMatch: 'full'},
      {path: 'sign-in', component: SignInComponent},
      {path: 'sign-up', component: SignUpComponent},
      {path: '**', redirectTo: 'sign-in'}
  ]},
  {
    path: 'quiz', loadChildren: './components/quiz/quiz.module#QuizModule'
  },
  {
    path: 'home', loadChildren: './components/home/home.module#HomeModule', canLoad: [AuthGuard]
  },
  {path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes/*, {enableTracing: true}*/)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
