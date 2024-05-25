import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { InstructorClient, TaskDto } from 'src/app/web-api-client';

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
  
  tasksList:any[]=[];
  taskDescription:string='';
  myControl = new FormControl<string>('');



  options: any[] = [
    "Mark assignment",
    "Mark quiz",
    "Mark project",
    "Monitor exam",
    "Help students"
    ];

  constructor( @Inject(MAT_DIALOG_DATA) private data: any,private instructorClint: InstructorClient) { }

  ngOnInit(): void {

    this.getSectionTasks(this.data.sectionId);
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
    let manageTaskDto = new TaskDto();
    manageTaskDto.sectionId = this.data.sectionId;
    manageTaskDto.assignedTAId = this.data.taId;
    manageTaskDto.description= this.taskDescription;
    this.instructorClint.assignTask(manageTaskDto).subscribe((responce) => {
      this.getSectionTasks(this.data.sectionId);
    });
  }
}


interface Task {
  id: number;
  text: string;
}