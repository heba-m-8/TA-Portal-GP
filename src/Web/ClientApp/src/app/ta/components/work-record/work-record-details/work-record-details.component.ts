import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TaskDto, UpdateWorkRecordDto,TAClient, WorkRecordClient, UpdateTaskDto } from 'src/app/web-api-client';


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
  totalHours:any;
  constructor(private router: Router,private TAClient: TAClient, private workRecordClient: WorkRecordClient) { 
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
    const totalHoursSum = this.tasksList.reduce((sum, task) => sum + task.totalHours, 0);
    this.totalHours = totalHoursSum;
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

  submit(){
    var updateWorkRecordDto = new UpdateWorkRecordDto();
    updateWorkRecordDto.id = this.data.id;
    updateWorkRecordDto.assignedTAId = this.user.id;

    if(this.workRecordDetails.isSubmitted){
      this.TAClient.reSubmitWorkRecord2(updateWorkRecordDto).subscribe((responce) => {
        this.router.navigate(['ta/workrecord']);

      });
    }
    else{
      this.TAClient.submitWorkRecord2(updateWorkRecordDto).subscribe((responce) => {
        this.router.navigate(['ta/workrecord']);

      });
    }
    
  }


  updateTask(totalHours: number,taskId,oldTotalHours) {
    const dto = new UpdateTaskDto();
    dto.id = taskId;
    dto.totalHours = totalHours;
    dto.oldTotalHours =oldTotalHours;

    this.TAClient.updateTaskHours(dto).subscribe((response) => {
      this.getWorkRecordById();
    });
  }

  onSelectionChange(event: Event , taskId,oldTotalHours) {
    const selectElement = event.target as HTMLSelectElement;
    const totalHours = Number(selectElement.value);
    this.totalHours = this.totalHours - oldTotalHours +totalHours ;
    this.updateTask(totalHours,taskId,oldTotalHours);
  }

}


