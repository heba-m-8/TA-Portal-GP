import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkRecordComponent } from './components/work-record/work-record.component';
import { RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../autherization.guard';
import { SharedModule } from '../shared.module';
import { DeanOFGraduateNavMenuComponent } from './components/dean-ofgraduate-nav-menu/dean-ofgraduate-nav-menu.component';
import { WorkRecordDetailsComponent } from './components/work-record/work-record-details/work-record-details.component';
import { RejectedWorkRecordComponent } from './components/work-record/rejected-work-record/rejected-work-record.component';
import { NewWorkRecordComponent } from './components/work-record/new-work-record/new-work-record.component';
import { ConfirmRejectWorkRecordComponent } from './components/work-record/confirm-reject-work-record/confirm-reject-work-record.component';
import { UserProfileComponent } from './user-profile/user-profile.component';



@NgModule({
  declarations: [
    WorkRecordComponent,
    DeanOFGraduateNavMenuComponent,
    WorkRecordDetailsComponent,
    RejectedWorkRecordComponent,
    NewWorkRecordComponent,
    ConfirmRejectWorkRecordComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    
    RouterModule.forRoot([
      { path: 'deanofgraduatestudies', component: WorkRecordComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'deanofgraduatestudies/details/:id', component: WorkRecordDetailsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'deanofgraduatestudies/profile', component: UserProfileComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },

    ]),
  ]
})
export class DeanOfGraduateStudiesModule { }
