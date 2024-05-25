import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoursesComponent } from './components/courses/courses.component';
import { AuthorizationGuard } from '../autherization.guard';
import { SharedModule } from '../shared.module';
import { InstructorNavMenuComponent } from './components/instructor-nav-menu/instructor-nav-menu.component';
import { WorkRecordComponent } from './components/work-record/work-record.component';
import { ManageTasksComponent } from './components/manage-tasks/manage-tasks.component';
import { WorkRecordDetailsComponent } from './components/work-record/work-record-details/work-record-details.component';
import { RejectedWorkRecordComponent } from './components/work-record/rejected-work-record/rejected-work-record.component';
import { NewWorkRecordComponent } from './components/work-record/new-work-record/new-work-record.component';
import { ConfirmRejectWorkRecordComponent } from './components/work-record/confirm-reject-work-record/confirm-reject-work-record.component';
import { UserProfileComponent } from './user-profile/user-profile.component';



@NgModule({
  declarations: [CoursesComponent, InstructorNavMenuComponent, WorkRecordComponent, ManageTasksComponent, WorkRecordDetailsComponent, RejectedWorkRecordComponent, NewWorkRecordComponent,ConfirmRejectWorkRecordComponent, UserProfileComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forRoot([
      { path: 'instructor', component: CoursesComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'instructor/workrecord', component: WorkRecordComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'instructor/workrecord/details/:id', component: WorkRecordDetailsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'instructor/profile', component: UserProfileComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },

    ]),
  ]
})
export class InstructorModule { }
