import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FinanceClient, WorkRecordDto } from 'src/app/web-api-client';
import { WorkRecordDetailsComponent } from './work-record-details/work-record-details.component';
import { NavigationExtras, Router } from '@angular/router';
import { Observable, Observer } from 'rxjs';
import { ExampleTab } from 'src/app/ta/components/tasks/tasks.component';

@Component({
  selector: 'app-work-record',
  templateUrl: './work-record.component.html',
  styleUrls: ['./work-record.component.css']
})

export class WorkRecordComponent  implements OnInit {
  userId: any;
  activeTab: number = 0;
  asyncTabs: Observable<ExampleTab[]>;
  selectedTabIndex: number = 0;
  constructor() {
    this.userId = localStorage.getItem('UserId');
    this.asyncTabs = new Observable((observer: Observer<ExampleTab[]>) => {
      observer.next([
        {label: 'New Work Records', content: ''},
        {label: 'Approved Work Records', content: ''},
      ]);
    });
  }

  ngOnInit(): void {
  }


  trackByFn(item) {
    return item.id;
  }

  tabChanged(index: number) {
    this.selectedTabIndex = index;
  }

}


