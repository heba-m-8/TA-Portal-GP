import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UpdateTaskDto ,TAClient} from 'src/app/web-api-client';

@Component({
  selector: 'app-task-total-hours',
  templateUrl: './task-total-hours.component.html',
  styleUrls: ['./task-total-hours.component.css']
})
export class TaskTotalHoursComponent implements OnInit{
  displayedColumns: string[] = ['description','status'];
  userId:any;
  @Output() taskUpdated: EventEmitter<void> = new EventEmitter<void>();

  
  tasksList:any[]=[];
  totalHours:any;

   numbers: number[] = [1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6];

  constructor( @Inject(MAT_DIALOG_DATA) private data: any,private TAClient: TAClient) { }

  ngOnInit(): void {

  }

  updateTask(){    
    var dto= new UpdateTaskDto(); 
    dto.id=this.data.taskId;
    dto.status=true; 
    dto.totalHours=this.totalHours; 
    this.TAClient.updateTask(dto).subscribe((responce) => {
      this.taskUpdated.emit();
    })
  }
}
