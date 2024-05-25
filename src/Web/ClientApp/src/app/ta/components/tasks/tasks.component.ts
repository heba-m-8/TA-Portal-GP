import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, Observer } from 'rxjs';
import { TAClient, TaskDto, UpdateTaskDto } from 'src/app/web-api-client';
import { TaskTotalHoursComponent } from './task-total-hours/task-total-hours.component';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit,AfterViewInit  {
  displayedColumns: string[] = ['courseRef','sectionName','description','assigner','status'];
  dataSource: MatTableDataSource<TaskDto>;
  userId:any;
  isCompleted:boolean = false 
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  activeTab: number = 0;
  asyncTabs: Observable<ExampleTab[]>;
  selectedTabIndex: number = 0;

  taskList:any[]=[];
  constructor(private TAClient: TAClient, private dialog: MatDialog ) {
    this.userId = localStorage.getItem('UserId');
    this.asyncTabs = new Observable((observer: Observer<ExampleTab[]>) => {
      observer.next([
        {label: 'New Tasks', content: ''},
        {label: 'Completed Tasks', content: ''},
      ]);
    });
  }

  tabChanged(index: number) {
    this.selectedTabIndex = index;

    if (index === 0) {
      this.getTATasks(this.userId, false);
    } else if (index === 1) {
      this.getTATasks(this.userId, true);
    }
  }

  ngOnInit(): void {
    this.getTATasks(this.userId, false);


  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  


  getTATasks(userId , status){
    this.TAClient.getTATasks(userId,status).subscribe((responce) => {
      this.taskList = responce;
      if(status)
      this.taskList=this.taskList.reverse();

      this.dataSource = new MatTableDataSource(this.taskList);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });

  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  updateTask(id){

    const dialogRef = this.dialog.open(TaskTotalHoursComponent, {
      maxHeight: '530px',
      width: '800px',
      data: { userId:this.userId ,taskId:id}
  })

  dialogRef.afterClosed().subscribe(result => {
    this.getTATasks(this.userId, false);
  });

  dialogRef.componentInstance.taskUpdated.subscribe(() => {
    this.getTATasks(this.userId, false);
  });
  }

  trackByFn(index, item) {
    return item.id; // Use a unique identifier property of the tab object
  }

  isCompletedTabSelected(): boolean {
    return this.selectedTabIndex === 1;
  }
}



export interface ExampleTab {
  label: string;
  content: string;
}