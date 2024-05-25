import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { InstructorClient, PhdTAClient, PhdTaskDto, TaskDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-manage-tasks',
  templateUrl: './manage-tasks.component.html',
  styleUrls: ['./manage-tasks.component.css']
})
export class ManageTasksComponent implements OnInit{
  displayedColumns: string[] = ['description','status'];
  dataSource: MatTableDataSource<TaskDto> = new MatTableDataSource<TaskDto>();
  userId:any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  numbers: number[] = [1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6];
  tasksList:any[]=[];
  taskDescription:string='';
  myControl = new FormControl<string>('');
  taskHours:any;


  options: any[] = [
    "Give lab lecture",
    "Write lab questions",
    "Mark lab",
    "Mark quiz",
    "Mark project",
    "Monitor exam",
    "Help students"
    ];

  constructor( @Inject(MAT_DIALOG_DATA) private data: any,private instructorClint: InstructorClient,private phdTAClient: PhdTAClient) { }

  ngOnInit(): void {
    this.getSectionTasks(this.data.section.id);
  }

  displayFn(task: any): string {
    return task?? '';
  }

  getSectionTasks(sectionId){
    this.instructorClint.getSectionTasks(sectionId).subscribe((responce) => {
      this.tasksList = responce.reverse();
      this.dataSource = new MatTableDataSource(this.tasksList);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.taskDescription=null;
    });
  }



  addNewTask(){
    let manageTaskDto = new PhdTaskDto();
    manageTaskDto.sectionId = this.data.section.id;
    manageTaskDto.assignedTAId = this.data.userId;
    manageTaskDto.description= this.taskDescription;
    manageTaskDto.hours = this.taskHours;
    manageTaskDto.status = true;
    this.phdTAClient.createTask(manageTaskDto).subscribe((responce) => {
      this.getSectionTasks(this.data.section.id);
      this.taskHours = null;
    });
  }
}


interface Task {
  id: number;
  text: string;
}
