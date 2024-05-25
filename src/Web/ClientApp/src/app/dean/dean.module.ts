import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkRecordsComponent } from './components/work-records/work-records.component';
import { RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../autherization.guard';
import { SharedModule } from '../shared.module';
import { DeanNavMenuComponent } from './components/dean-nav-menu/dean-nav-menu.component';
import { WorkRecordDetailsComponent } from './components/work-records/work-record-details/work-record-details.component';
import { NewWorkRecordComponent } from './components/work-records/new-work-record/new-work-record.component';
import { RejectedWorkRecordComponent } from './components/work-records/rejected-work-record/rejected-work-record.component';
import { ConfirmRejectWorkRecordComponent } from './components/work-records/confirm-reject-work-record/confirm-reject-work-record.component';
import { UserProfileComponent } from './user-profile/user-profile.component';



@NgModule({
  declarations: [
    WorkRecordsComponent,
    DeanNavMenuComponent,
    WorkRecordDetailsComponent,
    NewWorkRecordComponent,
    RejectedWorkRecordComponent,
    ConfirmRejectWorkRecordComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forRoot([
      { path: 'dean', component: WorkRecordsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'dean/details/:id', component: WorkRecordDetailsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'dean/profile', component: UserProfileComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },

    ]),
  ]
})
export class DeanModule { }
