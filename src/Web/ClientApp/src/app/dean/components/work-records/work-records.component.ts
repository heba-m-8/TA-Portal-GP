import {  Component, OnInit } from '@angular/core';

import { Observable, Observer } from 'rxjs';
import { ExampleTab } from 'src/app/ta/components/tasks/tasks.component';


@Component({
  selector: 'app-work-records',
  templateUrl: './work-records.component.html',
  styleUrls: ['./work-records.component.css']
})
export class WorkRecordsComponent implements OnInit {
  userId: any;
  activeTab: number = 0;
  asyncTabs: Observable<ExampleTab[]>;
  selectedTabIndex: number = 0;
  constructor() {
    this.userId = localStorage.getItem('UserId');
    this.asyncTabs = new Observable((observer: Observer<ExampleTab[]>) => {
      observer.next([
        {label: 'New Work Records', content: ''},
        {label: 'Dean Of Graduates  Rejected Work Records', content: ''},
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

