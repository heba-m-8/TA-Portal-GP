import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TasksComponent } from './components/tasks/tasks.component';
import { WorkRecordComponent } from './components/work-record/work-record.component';
import { RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../autherization.guard';
import { TaNavMenuComponent } from './components/ta-nav-menu/ta-nav-menu.component';
import { MyCoursesComponent } from './components/my-courses/my-courses.component';
import { SharedModule } from '../shared.module';
import { TaskTotalHoursComponent } from './components/tasks/task-total-hours/task-total-hours.component';
import { RestrictHoursDirective } from './components/tasks/task-total-hours/restrict-hours.directive';
import { WorkRecordDetailsComponent } from './components/work-record/work-record-details/work-record-details.component';
import { NewWorkRecordComponent } from './components/work-record/new-work-record/new-work-record.component';
import { SubmittedWorkRecordComponent } from './components/work-record/submitted-work-record/submitted-work-record.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';



@NgModule({
  declarations: [
    TasksComponent,
    WorkRecordComponent,
    TaNavMenuComponent,
    MyCoursesComponent,
    TaskTotalHoursComponent,
    RestrictHoursDirective,
    WorkRecordDetailsComponent,
    NewWorkRecordComponent,
    SubmittedWorkRecordComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,

    RouterModule.forRoot([
      { path: 'ta', component: TasksComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'ta/workrecord', component: WorkRecordComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'ta/mycourses', component: MyCoursesComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'ta/profile', component: UserProfileComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },
      { path: 'ta/workrecord/details/:id', component: WorkRecordDetailsComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },

    ]),
  ]
})
export class TAModule { }
