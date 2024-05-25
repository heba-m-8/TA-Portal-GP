import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InstructorModule } from './instructor/instructor.module';
import { HeadOfDepartmentModule } from './head-of-department/head-of-department.module';
import { DeanModule } from './dean/dean.module';
import { TAModule } from './ta/ta.module';
import { FinanceModule } from './finance/finance.module';
import { DeanOfGraduateStudiesModule } from './dean-of-graduate-studies/dean-of-graduate-studies.module';
import { AuthModule } from './auth/auth.module';
import { AuthorizationGuard } from './autherization.guard';
import { Erorr404Component } from './erorr404/erorr404.component';
import { FooterComponent } from './footer/footer.component';
import { SharedModule } from './shared.module';
import { PhdModule } from './phd/phd.module';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AuthModule,
    InstructorModule,
    HeadOfDepartmentModule,
    DeanModule,
    TAModule,
    FinanceModule,
    DeanOfGraduateStudiesModule,
    PhdModule,
    SharedModule,

 RouterModule.forRoot([
  { path: '', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule), pathMatch: 'full' },
  { path: 'instructor', loadChildren: () => import('./instructor/instructor.module').then(m => m.InstructorModule), pathMatch: 'full', canActivate: [AuthorizationGuard] },
  { path: 'hod', loadChildren: () => import('./head-of-department/head-of-department.module').then(m => m.HeadOfDepartmentModule), pathMatch: 'full', canActivate: [AuthorizationGuard] },
  { path: 'dean', loadChildren: () => import('./dean/dean.module').then(m => m.DeanModule), pathMatch: 'full', canActivate: [AuthorizationGuard] },
  { path: 'ta', loadChildren: () => import('./ta/ta.module').then(m => m.TAModule), pathMatch: 'full', canActivate: [AuthorizationGuard] },
  { path: 'finance', loadChildren: () => import('./finance/finance.module').then(m => m.FinanceModule), pathMatch: 'full', canActivate: [AuthorizationGuard] },
  { path: 'taphd', loadChildren: () => import('./phd/phd.module').then(m => m.PhdModule), pathMatch: 'full', canActivate: [AuthorizationGuard] },
  { path: 'deanofgraduatestudies', loadChildren: () => import('./dean-of-graduate-studies/dean-of-graduate-studies.module').then(m => m.DeanOfGraduateStudiesModule), pathMatch: 'full', canActivate: [AuthorizationGuard] },
  { path: '**', component: Erorr404Component }
]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
