import {Component, OnInit } from '@angular/core';

import { ExampleTab } from 'src/app/ta/components/tasks/tasks.component';
import { Observable, Observer } from 'rxjs';

@Component({
  selector: 'app-work-record',
  templateUrl: './work-record.component.html',
  styleUrls: ['./work-record.component.css']
})

export class WorkRecordComponent implements OnInit {
  userId: any;
  activeTab: number = 0;
  asyncTabs: Observable<ExampleTab[]>;
  selectedTabIndex: number = 0;
  constructor() {
    this.userId = localStorage.getItem('UserId');
    this.asyncTabs = new Observable((observer: Observer<ExampleTab[]>) => {
      observer.next([
        {label: 'New Work Records', content: ''},
        {label: 'Finance Rejected Work Records', content: ''},
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
