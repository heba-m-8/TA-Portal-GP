import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhdNavMenuComponent } from './components/phd-nav-menu/phd-nav-menu.component';
import { PhdCoursesComponent } from './components/phd-courses/phd-courses.component';
import { SharedModule } from '../shared.module';
import { RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../autherization.guard';
import { WorkRecordComponent } from './components/work-record/work-record.component';
import { ManageTasksComponent } from './components/phd-courses/manage-tasks/manage-tasks.component';
import { NewWorkRecordComponent } from './components/work-record/new-work-record/new-work-record.component';
import { RejectedWorkRecordComponent } from './components/work-record/rejected-work-record/rejected-work-record.component';
import { WorkRecordDetailsComponent } from './components/work-record/work-record-details/work-record-details.component';
import { UserProfileComponent } from './user-profile/user-profile.component';



@NgModule({
  declarations: [
    PhdNavMenuComponent,
    PhdCoursesComponent,
    WorkRecordComponent,
    ManageTasksComponent,
    NewWorkRecordComponent,
    RejectedWorkRecordComponent,
    WorkRecordDetailsComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forRoot([
      { path: 'taphd', component: PhdCoursesComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'taphd/workrecord', component: WorkRecordComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'taphd/workrecord/details/:id', component: WorkRecordDetailsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'taphd/profile', component: UserProfileComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },


    ]),
  ]
})
export class PhdModule { }
