import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DeanClient, TaskDto, WorkRecordClient } from 'src/app/web-api-client';
import { ConfirmRejectWorkRecordComponent } from '../confirm-reject-work-record/confirm-reject-work-record.component';

@Component({
  selector: 'app-work-record-details',
  templateUrl: './work-record-details.component.html',
  styleUrls: ['./work-record-details.component.css']
})

export class WorkRecordDetailsComponent implements OnInit{

  displayedColumns: string[] = ['courseRef','sectionName','insructorName','description','totalHours'];
  dataSource: MatTableDataSource<TaskDto> = new MatTableDataSource<TaskDto>();
  userId:any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  tasksList:any[]=[];
  taskDescription:any;
  user:any;
  items:any;
  data: any;
  workRecordDetails:any;
  constructor(private router: Router,private deanClient: DeanClient, private dialog: MatDialog , private workRecordClient: WorkRecordClient) { 
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras?.state as { userId: string, data: any };
    
    if (state) {
      this.userId = state.userId;
      this.data = state.data;
    }
  }

  ngOnInit(): void {
    this.getTaskList();
    this.getWorkRecordById();
  }

  getTaskList(){
    this.items = this.data;
    this.user = this.data.assignedTA;
    this.tasksList = this.data.tasks;
    this.dataSource = new MatTableDataSource(this.tasksList);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.taskDescription=null;
  }

  getWorkRecordById(){
    this.workRecordClient.getWorkRecordById(this.items.id,this.userId ).subscribe((responce) => {
      this.workRecordDetails = responce;
    });
  }

  approve(){
    this.deanClient.approveWorkRecord(this.items.id).subscribe((responce) => {
      this.router.navigate(['/dean']);
    });
  }
  
  reject(){

    const dialogRef = this.dialog.open(ConfirmRejectWorkRecordComponent, {
      maxHeight: '830px',
      width: '1000px',
      data: { userId: this.userId, workRecordId: this.items.id}
    })

    dialogRef.afterClosed().subscribe(result => {
    });
  }

}
