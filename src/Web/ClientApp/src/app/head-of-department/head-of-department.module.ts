import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HODCoursesComponent } from './components/hod-courses/hod-courses.component';
import { RouterModule } from '@angular/router';
import { AllStudentsComponent } from './components/all-students/all-students.component';
import { AuthorizationGuard } from '../autherization.guard';
import { SharedModule } from '../shared.module';
import { HodNavMenuComponent } from './components/hod-nav-menu/hod-nav-menu.component';
import { WorkRecordComponent } from './components/work-record/work-record.component';
import { ManageTAComponent } from './components/manage-ta/manage-ta.component';
import { WorkRecordDetailsComponent } from './components/work-record/work-record-details/work-record-details.component';
import { RejectedWorkRecordComponent } from './components/work-record/rejected-work-record/rejected-work-record.component';
import { NewWorkRecordComponent } from './components/work-record/new-work-record/new-work-record.component';
import { ConfirmRejectWorkRecordComponent } from './components/work-record/confirm-reject-work-record/confirm-reject-work-record.component';
import { UserProfileComponent } from './user-profile/user-profile.component';



@NgModule({
  declarations: [
    HODCoursesComponent,
    AllStudentsComponent,
    HodNavMenuComponent,
    WorkRecordComponent,
    ManageTAComponent,
    WorkRecordDetailsComponent,
    RejectedWorkRecordComponent,
    NewWorkRecordComponent,
    ConfirmRejectWorkRecordComponent,
    UserProfileComponent, 
   ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forRoot([
      { path: 'hod', component: AllStudentsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'hod/courses', component: HODCoursesComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'hod/workrecord', component: WorkRecordComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'hod/workrecord/details/:id', component: WorkRecordDetailsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'hod/profile', component: UserProfileComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },

      
    ]),
  ]
})
export class HeadOfDepartmentModule { }
