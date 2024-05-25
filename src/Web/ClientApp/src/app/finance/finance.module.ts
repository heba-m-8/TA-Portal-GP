import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkRecordComponent } from './components/work-record/work-record.component';
import { RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../autherization.guard';
import { SharedModule } from '../shared.module';
import { FinanceNavMenuComponent } from './components/finance-nav-menu/finance-nav-menu.component';
import { WorkRecordDetailsComponent } from './components/work-record/work-record-details/work-record-details.component';
import { ConfirmRejectWorkRecordComponent } from './components/work-record/confirm-reject-work-record/confirm-reject-work-record.component';
import { NewWorkRecordComponent } from './components/work-record/new-work-record/new-work-record.component';
import { ApprovedWorkRecordComponent } from './components/work-record/approved-work-record/approved-work-record.component';
import { UserProfileComponent } from './user-profile/user-profile.component';



@NgModule({
  declarations: [
    WorkRecordComponent,
    FinanceNavMenuComponent,
    WorkRecordDetailsComponent,
    ConfirmRejectWorkRecordComponent,
    NewWorkRecordComponent,
    ApprovedWorkRecordComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forRoot([
      { path: 'finance', component: WorkRecordComponent, pathMatch: 'full' , canActivate: [AuthorizationGuard]},
      { path: 'finance/details/:id', component: WorkRecordDetailsComponent, pathMatch: 'full' , canActivate: [AuthorizationGuard]},
      { path: 'finance/profile', component: UserProfileComponent, pathMatch: 'full', canActivate: [AuthorizationGuard] },

    ]),
  ]
})
export class FinanceModule { }
